using checkpoint.Helpers;
using checkpoint.Views.Catalogs.Products.Models;
using checkpoint.Views.Catalogs.Products.Presenters;
using checkpoint.Views.Catalogs.Products.Services;
using Microsoft.Win32;
using Google.Apis.Auth;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Interop;

namespace checkpoint.Views.Catalogs.Products.Views
{
    /// <summary>
    /// Interaction logic for CreateProduct.xaml
    /// </summary>
    public partial class CreateProduct : UserControl
    {
        private ProductsPresenter _productsPresenter;
        Productos productos = new Productos();
        BindingList<ProductosProveedor> supplierProductList;
        BindingList<ImpuestoProducto> impuestosList;
        BindingList<PLUProductos> plusList;
        public CreateProduct()
        {
            InitializeComponent();
            _productsPresenter = new ProductsPresenter(new ProductsServices());
            supplierProductList = new BindingList<ProductosProveedor>();
            plusList = new BindingList<PLUProductos>();
            impuestosList = new BindingList<ImpuestoProducto>();
            productCodeTxtBox.PreviewTextInput += productCodeTxtBox.OnlyNumbersValidationTextBox;
            descriptionTextBox.PreviewTextInput += descriptionTextBox.LettersAndNumbersValidationTextBox;
            existTextBox.PreviewTextInput += existTextBox.OnlyNumbersValidationTextBox;
            minTextBox.PreviewTextInput += minTextBox.OnlyNumbersValidationTextBox;
            maxTextBox.PreviewTextInput += maxTextBox.OnlyNumbersValidationTextBox;
            minBuyTextBox.PreviewTextInput += minBuyTextBox.OnlyNumbersValidationTextBox;
            priceTextBox.PreviewTextInput += minBuyTextBox.NumberValidationTextBox;
            sellPriceTextBox.PreviewTextInput += minBuyTextBox.NumberValidationTextBox;
            gainTextBox.PreviewTextInput += minBuyTextBox.OnlyNumbersValidationTextBox;
            impuestosList.ListChanged += ImpuestosList_ListChanged;
            suppliersProduct.ItemsSource = supplierProductList;
            listTax.ItemsSource = impuestosList;
            listPlus.ItemsSource = plusList;
            GetAllUnidades();
            GetAllDepartamentos();
            GetAllMarcas();
            GetAllCatalogoSat();
            GetAllUnidadesSat();
            GetAllSuppliers();
            GetAllImpuestos();
            HomeProductsGrid.ItemsSource = _productsPresenter.GetAllProductos();
        }

        private void ImpuestosList_ListChanged(object sender, ListChangedEventArgs e)
        {
            CalculatePercentageGain();
        }

        public void GetAllUnidades()
        {
            List<Unidades> unidadesList = new List<Unidades>();
            unidadesList = _productsPresenter.GetAllUnidades();
            unitComboBox.ItemsSource = unidadesList;
            unitComboBox.SelectedValuePath = "idUnidad";
            unitComboBox.DisplayMemberPath = "Descripcion";
        }
        public void GetAllDepartamentos()
        {
            List<Departamentos> departamentosList = new List<Departamentos>();
            departamentosList = _productsPresenter.GetAllDepartamentos();
            departmentComboBox.ItemsSource = departamentosList;
            departmentComboBox.SelectedValuePath = "idDepartamento";
            departmentComboBox.DisplayMemberPath = "Descripcion";
        }
        public void GetAllMarcas()
        {
            List<Marca> marcasList = new List<Marca>();
            marcasList = _productsPresenter.GetAllMarcas();
            brandComboBox.ItemsSource = marcasList;
            brandComboBox.SelectedValuePath = "idMarca";
            brandComboBox.DisplayMemberPath = "Descripcion";
        }

        public void GetAllCatalogoSat()
         {
            List<CatalogoSat> catalogoSatList = new List<CatalogoSat>();
            catalogoSatList = _productsPresenter.GetAllCatalogoSat();
            catalogoSatList = catalogoSatList.Select(a => { a.Descripcion = a.Descripcion + " - " + a.Clave; return a; }).ToList();
            claveComboBox.ItemsSource = catalogoSatList;
            claveComboBox.SelectedValuePath = "idCatalogoSat";
            claveComboBox.DisplayMemberPath = "Descripcion";
        }
        public void GetAllUnidadesSat()
        {
            List<UnidadSat> unidadSatList = new List<UnidadSat>();
            unidadSatList = _productsPresenter.GetAllUnidadesSat();
            unidadSatComboBox.ItemsSource = unidadSatList;
            unidadSatComboBox.SelectedValuePath = "idUnidadSat";
            unidadSatComboBox.DisplayMemberPath = "Descripcion";
        }

        public void GetAllSuppliers()
        {
            List<Proveedores> suppliersList = new List<Proveedores>();
            suppliersList = _productsPresenter.GetAllSuppliers();
            suppliersCombo.ItemsSource = suppliersList;
            suppliersCombo.SelectedValuePath = "idProveedor";
            suppliersCombo.DisplayMemberPath = "Nombre";
        }

        private void GetAllImpuestos()
        {
            List<Impuestos> taxesList = new List<Impuestos>();
            taxesList = _productsPresenter.GetAllImpuestos();
            ComboTaxes.ItemsSource = taxesList;
            ComboTaxes.SelectedValuePath = "idImpuesto";
            ComboTaxes.DisplayMemberPath = "Descripcion";
        }

        private void initButton_Click(object sender, RoutedEventArgs e)
        {
            cleanView();
            HomeProductsGrid.ItemsSource = _productsPresenter.GetAllProductos();
            productsTabControl.SelectedIndex = 0;
        }

        private void productGeneralBtn_Click(object sender, RoutedEventArgs e)
        {
            productsTabControl.SelectedIndex = 1;
        }

        private void productPriceBtn_Click(object sender, RoutedEventArgs e)
        {
            productsTabControl.SelectedIndex = 2;
        }

        private void productDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            productsTabControl.SelectedIndex = 3;
        }

        private void saveProductBtnClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(productCodeTxtBox.Text)
                && !string.IsNullOrEmpty(descriptionTextBox.Text)
                && departmentComboBox.SelectedIndex != -1
                && unitComboBox.SelectedIndex != -1
                && brandComboBox.SelectedIndex != -1
                && !string.IsNullOrEmpty(existTextBox.Text)
                && !string.IsNullOrEmpty(minTextBox.Text)
                && !string.IsNullOrEmpty(maxTextBox.Text)
                && !string.IsNullOrEmpty(minBuyTextBox.Text)
                && !string.IsNullOrEmpty(priceTextBox.Text)
                && !string.IsNullOrEmpty(gainTextBox.Text)
                && !string.IsNullOrEmpty(sellPriceTextBox.Text)
                && claveComboBox.SelectedIndex != -1
                && unidadSatComboBox.SelectedIndex != -1)
            {
                productos.NombreProducto = descriptionTextBox.Text;
                productos.Existencia = float.Parse(existTextBox.Text);
                productos.Minimo = float.Parse(minTextBox.Text);
                productos.Maximo = float.Parse(maxTextBox.Text);
                productos.MinimoCompra = float.Parse(minBuyTextBox.Text);
                productos.PrecioCosto = float.Parse(priceTextBox.Text);
                productos.PrecioVenta = float.Parse(sellPriceTextBox.Text);
                productos.idCatalogoSat = (int)claveComboBox.SelectedValue;
                productos.idDepartamento = (int)departmentComboBox.SelectedValue;
                productos.idMarca = (int)brandComboBox.SelectedValue;
                productos.idUnidad = (int)unitComboBox.SelectedValue;
                productos.Proveedores = supplierProductList.Select(a => { a.proveedor = null; return a; }).ToList();
                productos.PLUs = plusList.ToList();
                productos.PrecioVentaSinImpuestos = float.Parse(sellPriceWOTaxText.Text);
                productos.Proveedores = new List<ProductosProveedor>(supplierProductList);
                productos.Ganancia = (((productos.PrecioVenta / (((impuestosList.Sum(a => a.impuesto.Porcentaje)) / 100) + 1) - productos.PrecioCosto) / productos.PrecioVenta));
                productos.Impuestos = impuestosList.Select(a => { a.impuesto = null; return a; }).ToList();
                productos = _productsPresenter.SaveProduct(productos).Result;
                cleanView();
            }
        }

        private void cleanView()
        {
            descriptionTextBox.Text = existTextBox.Text = minTextBox.Text = maxTextBox.Text = minBuyTextBox.Text = priceTextBox.Text = sellPriceTextBox.Text = gainTextBox.Text = sellPriceWOTaxText.Text = productCodeTxtBox.Text = string.Empty;
            departmentComboBox.SelectedIndex = unitComboBox.SelectedIndex = brandComboBox.SelectedIndex = claveComboBox.SelectedIndex = unidadSatComboBox.SelectedIndex = ComboTaxes.SelectedIndex = suppliersCombo.SelectedIndex = -1;
            impuestosList.Clear(); plusList.Clear(); sellPriceTextBox.Text = string.Empty; supplierProductList.Clear(); pluAddText.Text = string.Empty;
            productos = new Productos();
            productsTabControl.SelectedIndex = 1;
            imageProduct.Source = null;
        }

        private void departmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var departamentosAux = (Departamentos)departmentComboBox.SelectedItem;
            if (departamentosAux != null)
            {
                productos.idDepartamento = departamentosAux.idDepartamento;
            }
        }

        private void unitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var unitAux = (Unidades)unitComboBox.SelectedItem;
            if (unitAux != null)
            {
                productos.idUnidad = unitAux.idUnidad;
            }
        }

        private void brandComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var brandAux = (Marca)brandComboBox.SelectedItem;
            if (brandAux != null)
            {
                productos.idMarca = brandAux.idMarca;
            }
        }

        private void claveComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var claveAux = (CatalogoSat)claveComboBox.SelectedItem;
            if (claveAux != null)
            {
                productos.idCatalogoSat = claveAux.idCatalogoSat;
                unidadSatComboBox.SelectedValue = claveAux.idUnidadSat;
            }
        }

        private void productCodeTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                productos = _productsPresenter.GetProductObjectByPLU(productCodeTxtBox.Text.Trim());
                if (productos != null)
                {
                    fillDataWithProductObject(productos);
                }
                else
                {
                    plusList.Clear();
                    PLUProductos pLUProducto = new PLUProductos();
                    pLUProducto.PLU = productCodeTxtBox.Text;
                    plusList.Add(pLUProducto);
                    descriptionTextBox.Focus();
                    productos = new Productos();
                }
            }
        }

        private void fillDataWithProductObject(Productos product)
        {
            supplierProductList.Clear();
            impuestosList.Clear();
            plusList.Clear();
            descriptionTextBox.Text = product.NombreProducto;
            existTextBox.Text = product.Existencia.ToString();
            minTextBox.Text = product.Minimo.ToString();
            maxTextBox.Text = product.Maximo.ToString();
            minBuyTextBox.Text = product.MinimoCompra.ToString();
            departmentComboBox.SelectedValue = product.idDepartamento;
            unitComboBox.SelectedValue = product.idUnidad;
            brandComboBox.SelectedValue = product.idMarca;
            priceTextBox.Text = product.PrecioCosto.ToString();
            sellPriceTextBox.Text = product.PrecioVenta.ToString();
            claveComboBox.SelectedValue = product.idCatalogoSat;
            sellPriceWOTaxText.Text = product.PrecioVentaSinImpuestos.ToString();
            supplierProductList.AddRange(product.Proveedores);
            impuestosList.AddRange(product.Impuestos);
            plusList.AddRange(product.PLUs);
            imageProduct.GetGoogleImageById(product.ImagenId);
        }

        private void deleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ProductosProveedor currentSupplier = button.DataContext as ProductosProveedor;
            if(currentSupplier.idProductoProveedor != 0)
                _productsPresenter.DeleteProductoProveedorById(currentSupplier.idProductoProveedor);
            supplierProductList.Remove(currentSupplier);
        }

        private void deleteTaxList_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ImpuestoProducto currentProductTax = button.DataContext as ImpuestoProducto;
            if (currentProductTax.idImpuestoProducto != 0)
                _productsPresenter.DeleteImpuestoProductoById(currentProductTax.idImpuestoProducto);
            impuestosList.Remove(currentProductTax);
        }

        private void deletePluList_Click(object sender, RoutedEventArgs e)
        {
            if(plusList.Count > 1)
            {
                Button button = (Button)sender;
                PLUProductos currentPlu = button.DataContext as PLUProductos;
                if (currentPlu.idPLU != 0)
                    _productsPresenter.DeletePluProductoById(currentPlu.idPLU);
                plusList.Remove(currentPlu);
            }
            else
            {
                MessageBox.Show("El producto debe tener almenos un PLU registrado.");
            }
            
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            ProductosProveedor proveedor = new ProductosProveedor
            {
                idProducto = productos.idProducto,
                idProveedor = (int)suppliersCombo.SelectedValue,
                proveedor = suppliersCombo.SelectedItem as Proveedores
            };
            ProductosProveedor supplierExist = supplierProductList.Where(x => x.idProveedor.Equals(proveedor.idProveedor)).FirstOrDefault();
            if(supplierExist is null)
                supplierProductList.Add(proveedor);
        }

        private void newTaxButton_Click(object sender, RoutedEventArgs e)
        {
            ImpuestoProducto impuesto = new ImpuestoProducto
            {
                idImpuesto = (int)ComboTaxes.SelectedValue,
                idProducto = productos.idProducto,
                impuesto = ComboTaxes.SelectedItem as Impuestos
            };
            ImpuestoProducto taxExist = impuestosList.Where(x => x.idImpuesto.Equals(impuesto.idImpuesto)).FirstOrDefault();
            if (taxExist is null)
                impuestosList.Add(impuesto);
        }

        private void sellPriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!String.IsNullOrEmpty(priceTextBox.Text) && !String.IsNullOrEmpty(sellPriceTextBox.Text))
            {
                CalculatePercentageGain();
            }
        }

        private void CalculatePercentageGain()
        {
            float totalPercentageTax = (impuestosList.Sum(x => x.impuesto.Porcentaje) / 100) + 1;
            float sellPrice = 0;
            float costo = 0;
            float.TryParse(priceTextBox.Text, out costo);
            float.TryParse(sellPriceTextBox.Text, out sellPrice);
            sellPriceWOTaxText.Text = (sellPrice / totalPercentageTax).ToString();
            gainTextBox.Text = (((sellPrice / totalPercentageTax) - costo) / sellPrice).ToString("P2");
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            cleanView();
        }

        private void newPLUButton_Click(object sender, RoutedEventArgs e)
        {
            PLUProductos currentProduct = plusList.Where(x => x.PLU.Equals(pluAddText.Text.Trim())).FirstOrDefault();
            if(currentProduct == null)
            {
                PLUProductos plu = new PLUProductos
                {
                    idProducto = productos.idProducto,
                    PLU = pluAddText.Text
                };
                plusList.Add(plu);
                pluAddText.Text = string.Empty;
            }
            
        }

        private void HomeProductsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            productos = grid.SelectedValue as Productos;
            fillDataWithProductObject(productos);
            productsTabControl.SelectedIndex = 1;
            productCodeTxtBox.Text = productos.PLUs[0].PLU;
        }

        private void ExamineImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(openFileDialog.FileName);
                string[] fileName = openFileDialog.FileName.Split('\\');
                string name = fileName[fileName.Length - 1].Split('.')[0];
                ImageProduct imageList = new ImageProduct
                {
                    Image = img,
                    idProducto = 1,
                    Description = name,
                    Id = Guid.NewGuid()
                };
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("https://drive.google.com/file/d/1uNokKACEDvZARxWnl6vRVNDeNAcBv5v_/view?usp=sharing");
                bitmap.EndInit();
                imageProduct.Source = bitmap;
                //upicImageAdd.Image = imageList.Image.Width > upicImageAdd.Width || imageList.Image.Height > upicImageAdd.Height ? PhotoExtension.ResizeImage(imageList.Image, upicImageAdd.Width, upicImageAdd.Height) : imageList.Image;
                //utxtDescription.Text = imageList.Description;
                //ulblFileName.Text = OpenFileDialog.FileName;
                //ImageRibbonList = imageList;
            }
        }
    }
}
