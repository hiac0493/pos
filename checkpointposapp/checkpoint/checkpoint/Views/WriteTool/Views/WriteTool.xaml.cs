using checkpoint.Helpers;
using checkpoint.Views.Catalogs.CatalogSat.Models;
using checkpoint.Views.Catalogs.CatalogSat.Presenters;
using checkpoint.Views.Catalogs.CatalogSat.Services;
using checkpoint.Views.Catalogs.Products.Models;
using checkpoint.Views.Catalogs.Products.Presenters;
using checkpoint.Views.Catalogs.Products.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace checkpoint.Views.WriteTool.Views
{
    /// <summary>
    /// Lógica de interacción para WriteTool.xaml
    /// </summary>
    public partial class WriteTool : UserControl
    {
        BindingList<Productos> productsList = new BindingList<Productos>();
        List<Productos> products = new List<Productos>();
        ProductsPresenter _productsPresenter;
        CatalogSatPresenter _catalogSatPresenter;
        int? idcatalogo = null;

        public WriteTool()
        {
            InitializeComponent();
            _productsPresenter = new ProductsPresenter(new ProductsServices());
            _catalogSatPresenter = new CatalogSatPresenter(new CatalogSatServices());
            producsGrid.ItemsSource = productsList;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            DataGrid dataGrid = producsGrid;
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                if(e.Key == Key.V)
                {
                    pegar_portapapeles(dataGrid);
                }
            }
        }

        public void pegar_portapapeles(DataGrid datagrid)
        {
            string texto_copiado = Clipboard.GetText();
            string[] lineas = texto_copiado.Split('\n');
            foreach(string linea in lineas)
            {
                string[] celdas = linea.Split('\t');
                if (celdas.Length == 8)
                {
                    List<PLUProductos> PLUs = new List<PLUProductos>();
                    List<ImpuestoProducto> ImpuestosList = new List<ImpuestoProducto>();
                    ImpuestosList.Add(new ImpuestoProducto
                    {
                        idImpuesto = 1,
                    });
                    PLUs.Add(new PLUProductos
                    {
                        PLU = celdas[0].Trim(),
                    });

                    if(celdas[5].Trim() != "" & celdas[5].Trim() != "#N/D")
                    {
                        PLUs.Add(new PLUProductos
                        {
                            PLU = celdas[5].Trim(),
                        });
                    }

                    if (celdas[4].Trim() != "" & celdas[4].Trim() != "#N/D")
                    {
                        catalogoSat catalogo = new catalogoSat();
                        catalogo = _catalogSatPresenter.GetCatalogByClave(celdas[4].Trim());

                        if (catalogo != null)
                        {
                            idcatalogo = catalogo.idCatalogoSat;
                        }
                        else
                        {
                            catalogo = new catalogoSat();
                            catalogo.Clave = celdas[4].Trim();
                            catalogo.Descripcion = "por definir";
                            catalogo.Activo = true;
                            catalogo.idUnidadSat = 1;
                            catalogo = _catalogSatPresenter.SaveCatalogSat(catalogo);
                            idcatalogo = catalogo.idCatalogoSat;
                        }
                    }


                    Productos producto = new Productos
                    {
                        NombreProducto = celdas[1].Trim(),
                        PLUs = PLUs,
                        PrecioCosto = float.Parse(celdas[2]),
                        PrecioVenta = float.Parse(celdas[3]),
                        PrecioVentaSinImpuestos = 0,
                        idMarca = 1,
                        idDepartamento = int.Parse(celdas[7]),
                        Ganancia = 1,
                        idUnidad = 1,
                        Minimo = 0,
                        Maximo = 0,
                        Impuestos = ImpuestosList,
                        idCatalogoSat = idcatalogo,
                        idUnidadSat = 1,
                        idSubDepartamento = int.Parse(celdas[6]),
                        MinimoCompra = 0,
                        ImagenId = "imagen"
                    };
                    products.Add(producto);
                }
            }
            productsList.AddRange(products);
            _productsPresenter.SaveProducts(products);
        }
    }
}
