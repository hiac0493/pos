using checkpoint.CheckPrices.Presenters;
using checkpoint.CheckPrices.Services;
using checkpoint.Helpers;
using checkpoint.Sales.Models;
using checkpoint.Sales.Presenters;
using checkpoint.Sales.Services;
using checkpoint.Sales.Views;
using checkpoint.Views.Dialogs.WithdrawCash.Views;
using checkpoint.Views.Sales.Models;
using CrearTicketVenta;
using ESC_POS_USB_NET.Printer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Views.Sales.Views
{
    /// <summary>
    /// Interaction logic for StartSale.xaml
    /// </summary>
    public partial class StartSale : UserControl
    {
        private SalesPresenter _salesPresenter;
        private CheckPresenter _checkPresenter;
        BindingList<ProductsGridSales> products = new BindingList<ProductsGridSales>();
        List<Impuestos> impuestosVenta = new List<Impuestos>();
        List<Impuestos> impuestosResumen = new List<Impuestos>();
        Ventas auxVenta = new Ventas();
        public StartSale()
        {
            InitializeComponent();
            skuText.Focus();
            clienteText.Text = "Publico en General";
            products.ListChanged += Products_ListChanged;
            _checkPresenter = new CheckPresenter(new ProductsCheckServices());
            _salesPresenter = new SalesPresenter(new SalesServices());
            Product.ItemsSource = products;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            
            /*Withdraw retiros = new Withdraw();
            retiros.ShowDialog();*/
        }

        private void Products_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(products.Count > 0)
            {
                App.OnUseChanged(false);
            }
            else
            {
                App.OnUseChanged(true);
            }
            totalLabel.Content = products.Sum(x => x.Price * x.Quantity).ToString("N2");
            impuestosVenta = new List<Impuestos>();
            foreach (var producto in products)
            {
                double sumPercentage = producto.impuestosList.Sum(x => x.porcentaje) / 100 + 1;
                producto.impuestosList = producto.impuestosList.Select(a => { a.cantidad = (float)(producto.Price / sumPercentage * (a.porcentaje / 100) * producto.Quantity); return a; }).ToList();
                impuestosVenta.AddRange(producto.impuestosList);
                impuestosResumen = impuestosVenta
                    .GroupBy(x => x.idImpuesto)
                    .Select(a => new Impuestos
                    {
                        idImpuesto = a.Key,
                        cantidad = a.Sum(x => x.cantidad)
                    }).ToList();
            }
        }

        private void addProductToSale(ProductSale productExtractToDB, float quantity)
        {
            ProductsGridSales productTypeForme = new ProductsGridSales { idProducto = productExtractToDB.idProducto, PLU = productExtractToDB.pluProducto, Description = productExtractToDB.nombre, Price = productExtractToDB.precioVenta, Quantity = (int)quantity, Total = productExtractToDB.precioVenta * quantity, impuestosList = productExtractToDB.impuestos };
            ProductsGridSales productExist = products.Where(x => x.idProducto.Equals(productExtractToDB.idProducto)).FirstOrDefault();
            if (productExist != null)
            {
                productExist.Quantity += productTypeForme.Quantity;
                productExist.Total = productExist.Quantity * productExist.Price;
                products.ResetBindings();
            }
            else
            {
                //imageProduct.GetGoogleImageById(productExtractToDB.imagenId);
                products.Add(productTypeForme);
            }
            skuText.Text = string.Empty;
        }

        private void updateProductToBuy(object sender)
        {
            ProductsGridSales productSelected = Product.SelectedValue as ProductsGridSales;
            productSelected.Quantity = float.Parse((((TextBox)(sender)).Text));
            productSelected.Total = productSelected.Quantity * productSelected.Price;
            products.ResetBindings();
            skuText.Text = string.Empty;
            skuText.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F10)
            {
                MessageBox.Show("Test");
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                float quantity = 1;
                if (skuText.Text.Contains('*'))
                {
                    string[] cantidad = skuText.Text.Split('*');
                    float.TryParse(cantidad[0], out quantity);
                    skuText.Text = cantidad[1];
                }
                ProductSale productExtractToDB = quantity > 0 ? _salesPresenter.GetProductByPLU(skuText.Text) : null;
                if (productExtractToDB != null)
                {
                    addProductToSale(productExtractToDB, quantity);
                }
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                CheckPrices checkPrice = new CheckPrices();
                checkPrice.ShowDialog();
                if ((bool)checkPrice.DialogResult)
                {
                    ProductSale productExtractToDB = _salesPresenter.GetProductByPLU(checkPrice.productoPLU);
                    addProductToSale(productExtractToDB, 1);
                }
            }
            else if (e.Key == Key.F12)
            {
                Ventas venta = new Ventas
                {
                    cambio = 0,
                    estatus = 'A',
                    fecha = DateTime.Now,
                    folioVenta = 0,
                    idUsuario = App._userApplication.idUsuario,
                    impuestos = 0,
                    pagado = products.Sum(x => x.Price * x.Quantity),
                    productos = (from productos in products select new AddProductSale { cantidad = productos.Quantity, idProducto = productos.idProducto, monto = productos.Total }).ToList(),
                    total = products.Sum(x => x.Price * x.Quantity),
                    pagos = new List<VentaPagos> { new VentaPagos { cantidad = products.Sum(x => x.Price * x.Quantity), idTipoPago = 1 } },
                    subtotal = products.Sum(x => x.Price * x.Quantity),
                    utilidad = 0,
                    impuesto = impuestosResumen
                };

                EndSale endsale = new EndSale(venta, products);
                endsale.ShowDialog();
                if ((bool)endsale.DialogResult)
                {
                    venta.cambio = endsale.cambio;
                    venta.pagado = endsale.pagado;
                    venta = endsale.ventaFin;
                    venta.impuestos = (float)Math.Round(venta.impuesto.Sum(x => x.cantidad), 2);
                    Ventas addVenta = _salesPresenter.AddVenta(venta).Result;
                    auxVenta = addVenta;
                    products.Clear();
                    //PrintMethod();
                }
            }
            else if (e.Key == Key.F11)
            {
                PrintTest();
            }
        }

        private void PrintTest()
        {
            Printer printer = new Printer("EPSON TM-T20II Receipt5");
            printer.TestPrinter();
            printer.FullPaperCut();
            printer.PrintDocument();
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                e.Handled = true;
                skuText.Focus();
            }
        }

        //Region Funciones de impresión de ticket
        private void PrintMethod()
        {
            CreateTicket ticket = new CreateTicket();
            Printer printer = new Printer("EPSON TM-T20II Receipt5");
            printer.AlignCenter();
            //IMAGEN
            /*Bitmap image = new Bitmap(Bitmap.FromFile("Icon.bmp"));
            printer.Image(image);*/
            printer.Append("CHECKPOINT SA DE CV");
            printer.Append("MMC110808MA8");
            printer.Append("LIB. OCEGUERA KM11 COLA.M. DE LEON");
            printer.AlignLeft();
            printer.NewLine();
            printer.Separator();
            printer.NewLine();
            string cashierName = App._userApplication.Nombres + " " + App._userApplication.ApellidoPaterno + " " + App._userApplication.ApellidoMaterno;
            printer.Append("CAJERO: " + cashierName);
            printer.Append("VENTA # " + auxVenta.folioVenta);
            printer.Append("FECHA: " + auxVenta.fecha);
            printer.NewLine();
            printer.Separator();
            printer.NewLine();
            printer.Append("DESCRIPCION             N#     PRECIO      TOTAL");
            printer.PrintDocument();
            foreach (AddProductSale product in auxVenta.productos)
            {
                ticket.AgregaArticulo(product.Productos.NombreProducto, product.cantidad, product.Productos.PrecioVenta, product.monto);
            }
            ticket.TextoIzquierda("");
            ticket.lineasGuion();
            ticket.AgregarTotalesCentrado("SUBTOTAL:", auxVenta.subtotal);
            ticket.AgregarTotalesCentrado("IVA:", auxVenta.impuestos);
            ticket.AgregarTotalesCentrado("TOTAL:", auxVenta.total);
            ticket.TextoIzquierda("");
            ticket.AgregarTotalesCentrado("SU PAGO:", auxVenta.pagado);
            ticket.AgregarTotalesCentrado("CAMBIO:", auxVenta.cambio);
            ticket.lineasGuion();
            ticket.TextoIzquierda("");
            ticket.ImprimirTicket("EPSON TM-T20II Receipt5");

            Printer printerEnd = new Printer("EPSON TM-T20II Receipt5");
            printerEnd.AlignCenter();
            printerEnd.NewLines(2);
            printerEnd.Code39(auxVenta.folioVenta.ToString());
            printerEnd.Append(auxVenta.folioVenta.ToString());
            printerEnd.AlignLeft();
            printerEnd.FullPaperCut();
            printerEnd.PrintDocument();
        }

        //endregion

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateProductToBuy(sender);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            updateProductToBuy(sender);
        }

        private void Product_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(Product.ActualWidth >= 343)
                skuText.Width = Product.ActualWidth - 343;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Product.Height = this.ActualHeight - 165;
        }
    }
}