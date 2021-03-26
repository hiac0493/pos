using checkpoint.CheckPrices.Presenters;
using checkpoint.CheckPrices.Services;
using checkpoint.Helpers;
using checkpoint.Sales.Models;
using checkpoint.Sales.Presenters;
using checkpoint.Sales.Services;
using checkpoint.Sales.Views;
using checkpoint.Views.CashClose.Models;
using checkpoint.Views.CashClose.Presenters;
using checkpoint.Views.CashClose.Services;
using checkpoint.Views.Promotions.Services;
using checkpoint.Views.Promotions.Presenters;
using checkpoint.Views.Sales.Models;
using checkpoint.Views.WithdrawCash;
using checkpoint.Views.WithdrawCash.Views;
using CrearTicketVenta;
using ESC_POS_USB_NET.Printer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
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
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private SalesPresenter _salesPresenter;
        private CheckPresenter _checkPresenter;
        private PromotionsPresenter _promotionsPresenter;
        double totalTaxe, ivaTasa, totalReturns, ivaReturns;
        private CashClosePresenter _cashClosePresenter;
        BindingList<ProductsGridSales> products = new BindingList<ProductsGridSales>();
        List<Impuestos> impuestosVenta = new List<Impuestos>();
        List<Impuestos> impuestosResumen = new List<Impuestos>();
        List<Promociones> listaPromociones = new List<Promociones>();
        List<ProductSale> listaProductos = new List<ProductSale>();
        Cortes corte = new Cortes();
        Cortes cortesToSave = new Cortes();
        Cortes cortesAux = new Cortes();
        BindingList<CortePagos> cortePagoList = new BindingList<CortePagos>();
        BindingList<VentaImpuestos> impuestoList = new BindingList<VentaImpuestos>();
        BindingList<TasaImpuesto> tasaList = new BindingList<TasaImpuesto>();
        BindingList<TasaImpuesto> returnList = new BindingList<TasaImpuesto>();
        Ventas auxVenta = new Ventas();
        #endregion

        #region Constructor

        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        double globalEfectivo = 0;
        public StartSale()
        {
            InitializeComponent();
            _cashClosePresenter = new CashClosePresenter(new CashCloseServices());
            corte = _cashClosePresenter.GetCurrentCashClose(App._userApplication.idUsuario);
            if (corte == null || corte.FolioVentaFin != null)
            {
                OpenCash openCash = new OpenCash();
                openCash.ShowDialog();
            }

            skuText.Focus();
            clienteText.Text = "Publico en General";
            products.ListChanged += Products_ListChanged;
            _checkPresenter = new CheckPresenter(new ProductsCheckServices());
            _salesPresenter = new SalesPresenter(new SalesServices());
            Product.ItemsSource = products;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            globalEfectivo = _salesPresenter.GetTotalEfectivo(App._userApplication.idUsuario);


        }
        #endregion
        #region Write data
        private void saveCashClose(Cortes cortesToSave)
        {
            cortesAux = _cashClosePresenter.SaveCashClose(cortesToSave).Result;
        }
        #endregion

        #region Read data

        #endregion
        #region Methods form
        private void endCashClose()
        {
            cortesToSave = _cashClosePresenter.GetCurrentCashClose(App._userApplication.idUsuario);
            cortesToSave.FolioVentaFin = _cashClosePresenter.GetLastFolio();
            cortesToSave.TotalVenta = _cashClosePresenter.GetTotalSale(App._userApplication.idUsuario, cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin);

            paymentsGrid.ItemsSource = cortePagoList;
            cortePagoList.AddRange(_cashClosePresenter.GetAllPagosCorte(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, App._userApplication.idUsuario));

            taxesGrid.ItemsSource = impuestoList;
            impuestoList.AddRange(_cashClosePresenter.GetAllTaxes(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, App._userApplication.idUsuario));

            totalSale.Text = cortesToSave.TotalVenta.ToString("C2");

            totalTaxe = _cashClosePresenter.GetTotalTaxes(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, App._userApplication.idUsuario);
            totalTaxes.Text = totalTaxe.ToString("C2");
            ivaGrid.ItemsSource = tasaList;
            tasaList.AddRange(_cashClosePresenter.GetTotalWithTaxes(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, App._userApplication.idUsuario));
            returnsGrid.ItemsSource = returnList;
            totalTasa.Text = cortesToSave.TotalVenta.ToString("C2");

            ivaTasa = _cashClosePresenter.CalcIvaTasa(App._userApplication.idUsuario, cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin);
            totalTasaIva.Text = ivaTasa.ToString("C2");
            returnList.AddRange(_cashClosePresenter.GetReturnsWithTaxes(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, App._userApplication.idUsuario));

            totalReturns = _cashClosePresenter.GetTotalReturns(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, App._userApplication.idUsuario);

            totalReturn.Text = totalReturns.ToString("C2");

            ivaReturns = _cashClosePresenter.CalcIvaReturn(App._userApplication.idUsuario, cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin);
            totalReturnIva.Text = ivaReturns.ToString("C2");
            saveCashClose(cortesToSave);
        }

        private void PrintCashClose()
        {
            if (cortesAux.IdCorte != 0 && cortesAux != null)
            {
                CreateTicket ticket = new CreateTicket();
                Printer printer = new Printer("EPSON TM-T20II Receipt5");
                printer.AlignCenter();
                printer.Append("CORTE Z");
                printer.Append("N° " + cortesAux.IdCorte);

                printer.AlignLeft();
                printer.Append("Fecha " + cortesAux.FechaInicio.ToString());
                printer.Append(cortesAux.Caja.Nombre + " " + cortesAux.Turno.Nombre);
                printer.Separator();
                printer.BoldMode("Total de venta: " + cortesAux.TotalVenta.ToString("C2"));
                printer.Separator();
                printer.PrintDocument();
                printer = new Printer("EPSON TM-T20II Receipt5");
                double sumTipoPago = 0;
                foreach (CortePagos tipoPago in cortePagoList)
                {
                    ticket.TextoExtremos(tipoPago.TipoPago, tipoPago.Total.ToString("C2"));
                    sumTipoPago += tipoPago.Total;
                }
                ticket.TextoCentro("");
                ticket.AgregarTotalesCentrado("Suma: ", (float)sumTipoPago);
                ticket.ImprimirTicket("EPSON TM-T20II Receipt5");

                printer.Separator();
                printer.BoldMode("Total de impuestos: " + totalTaxe.ToString("C2"));
                printer.Separator();
                printer.PrintDocument();
                printer = new Printer("EPSON TM-T20II Receipt5");
                double sumTipoPagoTax = 0;
                foreach (VentaImpuestos impuesto in impuestoList)
                {
                    ticket.TextoExtremos(impuesto.TipoImpuesto, impuesto.Total.ToString("C2"));
                    sumTipoPagoTax += impuesto.Total;
                }
                ticket.TextoCentro("");
                ticket.AgregarTotalesCentrado("Suma: ", (float)sumTipoPagoTax);
                ticket.ImprimirTicket("EPSON TM-T20II Receipt5");

                printer.Separator();
                printer.BoldMode("Total de venta: " + cortesAux.TotalVenta.ToString("C2"));
                printer.Separator();
                printer.PrintDocument();
                printer = new Printer("EPSON TM-T20II Receipt5");
                double sumTasa = 0;
                foreach (TasaImpuesto tasaTax in tasaList)
                {
                    ticket.TextoExtremos(tasaTax.Nombre, tasaTax.Total.ToString("C2"));
                    sumTasa += tasaTax.Total;
                }
                ticket.TextoCentro("");
                ticket.AgregarTotalesCentrado("Suma: ", (float)sumTasa);
                ticket.ImprimirTicket("EPSON TM-T20II Receipt5");

                printer.Separator();
                printer.BoldMode("Total de devoluciones: " + totalReturns.ToString("C2"));
                printer.Separator();
                printer.PrintDocument();
                printer = new Printer("EPSON TM-T20II Receipt5");
                double sumDevoluciones = 0;
                foreach (TasaImpuesto returns in returnList)
                {
                    ticket.TextoExtremos(returns.Nombre, returns.Total.ToString("C2"));
                    sumDevoluciones += returns.Total;
                }
                ticket.TextoCentro("");
                ticket.AgregarTotalesCentrado("Suma: ", (float)sumDevoluciones);
                ticket.CortaTicket();
                ticket.ImprimirTicket("EPSON TM-T20II Receipt5");
                cleanView();
            }
        }
        private void ActualizarPromos(List<Promociones> lista_promos)
        {
            foreach (Promociones item in lista_promos)
            {
                Promociones promoExists = listaPromociones.Where(x => x.idPromocion.Equals(item.idPromocion)).FirstOrDefault();
                if (promoExists == null)
                {
                    listaPromociones.Add(item);
                }
            }
        }
        private void CombinarPromos(List<ProductSale> listaProductos)
        {
            List<Productos> listToDelete;
            float totalProducts = 0;
            foreach (Promociones item in listaPromociones)
            {
                listToDelete = new List<Productos>();
                foreach (Productos product in item.Productos)
                {
                    if (listaProductos.Where(c => c.idProducto.Equals(product.idProducto)).Count() >= product.Cantidad)
                    {
                        listToDelete.Add(product);
                    }
                    else
                    {
                        listToDelete = null;
                        break;
                    }
                }
                if (listToDelete != null)
                {
                    totalProducts = EliminarProductos(listToDelete);
                    ProductSale productExtractToDB = new ProductSale();
                    productExtractToDB.nombre = item.NombrePromocion;
                    if (item.Porcentaje.HasValue)
                    {
                        productExtractToDB.precioVenta = -1*(((item.Porcentaje.Value) / 100) * totalProducts);
                    }
                    else
                    {
                        productExtractToDB.precioVenta = item.Monto.Value*-1;
                    }
                        //_salesPresenter.GetProductById(item.ProductoPromo.idProducto.ToString());
                    addProductToSale(productExtractToDB, 1);
                }
            }
        }
        private float EliminarProductos(List<Productos> listToDelete)
        {
            float total = 0;
            ProductSale productToDelete;
            foreach (Productos product in listToDelete)
            {
                productToDelete = listaProductos.Where(p => p.idProducto.Equals(product.idProducto)).FirstOrDefault();
                for (int i = 0; i < product.Cantidad; i++)
                {
                    listaProductos.Remove(productToDelete);
                    total +=productToDelete.precioVenta;
                }
            }
            return total;
        }
        private void Products_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (products.Count > 0)
            {
                App.OnUseChanged(true);
            }
            else
            {
                App.OnUseChanged(false);
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
            listaProductos.Add(productExtractToDB);
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
            if (productExtractToDB.promociones != null)
            {
                ActualizarPromos(productExtractToDB.promociones);
                CombinarPromos(listaProductos);
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
            if (skuText.Text != "")
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
                        //productExtractToDB.precioVenta = ModifyPricePromo(productExtractToDB, quantity);
                        addProductToSale(productExtractToDB, quantity);
                    }
                }
            }
        }

        private float ModifyPricePromo(ProductSale product, float quantity)
        {
            float total = 0;
            if (product.promociones != null && product.promociones.Count() > 0)
            {
                foreach (var promocion in product.promociones)
                {
                    if (promocion.Productos.Count > 1)
                    {

                    }
                    else
                    {
                        float totalParcial = CalcularPrecio(promocion, product.precioVenta, quantity);
                        if (total == 0)
                            total = totalParcial;
                        else if (total > totalParcial)
                            total = totalParcial;
                    }
                }
            }
            return total > 0 ? total : product.precioVenta;
        }

        private float CalcularPrecio(Promociones promocion, float precioVenta, float quantity)
        {
            float total = 0;
            if (promocion.Monto != null)
            {
                total = (float)promocion.Monto;
            }
            else
                total = precioVenta * (1 - ((float)promocion.Porcentaje / 100));
            return total;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    SalesTabControl.SelectedIndex = 0;
                    cortePagoList = new BindingList<CortePagos>();
                    impuestoList = new BindingList<VentaImpuestos>();
                    tasaList = new BindingList<TasaImpuesto>();
                    returnList = new BindingList<TasaImpuesto>();
                    break;
                case Key.F3:
                    globalEfectivo = _salesPresenter.GetTotalEfectivo(App._userApplication.idUsuario);
                    Withdraw retiroStart = new Withdraw(globalEfectivo, true);
                    retiroStart.ShowDialog();
                    break;
                case Key.F9:
                    endCashClose();
                    SalesTabControl.SelectedIndex = 1;
                    break;
                case Key.F2:
                    CheckPrices checkPrice = new CheckPrices();
                    checkPrice.ShowDialog();
                    if ((bool)checkPrice.DialogResult)
                    {
                        ProductSale productExtractToDB = _salesPresenter.GetProductByPLU(checkPrice.productoPLU);
                        addProductToSale(productExtractToDB, 1);
                    }
                    break;
                case Key.F12:
                    if (products.Count > 0)
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
                            SaleResult addVenta = _salesPresenter.AddVenta(venta).Result;
                            auxVenta = addVenta.venta;
                            //TO DO
                            if (addVenta.totalEfectivo >= 1000)
                            {
                                WithdrawAlert retiros = new WithdrawAlert(addVenta.totalEfectivo, false);
                                retiros.ShowDialog();
                            }
                            products.Clear();
                            PrintMethod();
                        }
                    }
                    break;
                case Key.F11:
                    PrintCashClose();
                    break;
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
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
                skuText.Focus();
            }
        }

        private void PrintMethod()
        {
            if (auxVenta != null)
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
                printerEnd.OpenDrawer();
            }
        }

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
            if (Product.ActualWidth >= 343)
                skuText.Width = Product.ActualWidth - 343;
        }

        private void SalesTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Product.Height = this.ActualHeight - 165;
        }

        private void OnlyNumbersTxtBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void cleanView()
        {
            cortesToSave = new Cortes();
        }
        #endregion
    }
}