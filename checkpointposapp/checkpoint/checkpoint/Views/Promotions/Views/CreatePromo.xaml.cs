using checkpoint.Helpers;
using checkpoint.Views.Promotions.Models;
using checkpoint.Views.Promotions.Presenters;
using checkpoint.Views.Promotions.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Views.Promotions.Views
{
    /// <summary>
    /// Lógica de interacción para CreatePromo.xaml
    /// </summary>
    public partial class CreatePromo : UserControl
    {
        float totalCosto = 0;
        long idPromocion = 0;
        PromotionsPresenter _promotionsPresenter;
        BindingList<ProductosPromocion> producsPromotionList = new BindingList<ProductosPromocion>();
        BindingList<Promociones> promocionesList = new BindingList<Promociones>();
        public CreatePromo()
        {
            InitializeComponent();
            _promotionsPresenter = new PromotionsPresenter(new PromotionsServices());
            HoraInicio.Value = DateTime.Now;
            HoraFin.Value = DateTime.Now;
            PromotionsGrid.ItemsSource = promocionesList;
            ProductsPromotionsGrid.ItemsSource = producsPromotionList;
            ComboDepartamentos.ItemsSource = _promotionsPresenter.GetAllDepartamentos();
            ComboMarcas.ItemsSource = _promotionsPresenter.GetAllMarca();
            promocionesList.AddRange(_promotionsPresenter.GetAllPromociones());
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            TabPageControl.SelectedIndex = 1;
        }

        private void InitButton_Click(object sender, RoutedEventArgs e)
        {
            TabPageControl.SelectedIndex = 0;
            promocionesList.Clear();
            promocionesList.AddRange(_promotionsPresenter.GetAllPromociones());
        }

        private void CancelPromo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SavePromo_Click(object sender, RoutedEventArgs e)
        {
            long fin = ((DateTime)HoraFin.Value).ToFileTime();
            long inicio = ((DateTime)HoraInicio.Value).ToFileTime();
            _promotionsPresenter.SavePromotion(new Promociones
            {
                idPromocion = idPromocion,
                DiasPromocion = DiasPromocionTxt.Text.Trim(),
                Estatus = true,
                Fin = ((DateTime)HoraFin.Value).Ticks,
                Inicio = ((DateTime)HoraInicio.Value).Ticks,
                NombrePromocion = NombrePromocionTxt.Text.Trim(),
                idDepartamento = ComboDepartamentos.SelectedValue != null ? (int?)ComboDepartamentos.SelectedValue : null,
                idMarca = ComboMarcas.SelectedValue != null ? (int?)ComboMarcas.SelectedValue : null,
                Productos = producsPromotionList.ToList(),
                Monto = PrecioTxt.Text.Trim().Length > 0 ? (float?)float.Parse(PrecioTxt.Text.Trim()) : null,
                Porcentaje = !String.IsNullOrEmpty(PorcentajeTxt.Text.Trim()) ? (float?)float.Parse(PorcentajeTxt.Text.Trim()) : null
            });
        }

        private void ClearPromoView()
        {
            idPromocion = 0;
            DiasPromocionTxt.Text = NombrePromocionTxt.Text = PrecioTxt.Text = PorcentajeTxt.Text = string.Empty;
            HoraFin.Value = HoraInicio.Value = DateTime.Now;
            ComboDepartamentos.SelectedIndex = ComboMarcas.SelectedIndex = -1;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            float quantity = 1;
            if (pluProductoText.Text.Contains('*'))
            {
                string[] cantidad = pluProductoText.Text.Split('*');
                float.TryParse(cantidad[0], out quantity);
                pluProductoText.Text = cantidad[1];
            }
            if (quantity > 0)
                GetProductoByPLU(pluProductoText.Text.Trim(), quantity);
        }

        private void GetProductoByPLU(string plu, float quantity)
        {
            pluProductoText.Text = string.Empty;
            ProductosPromocion producto = _promotionsPresenter.GetProductoByPLU(plu);
            ProductosPromocion productOfList = producsPromotionList.Where(x => x.idProducto == producto.idProducto).FirstOrDefault();
            if (producto != null)
            {
                if (productOfList == null)
                {
                    producto.Cantidad = quantity;
                    producsPromotionList.Add(producto);
                }
                else
                {
                    productOfList.Cantidad += quantity;
                    producsPromotionList.ResetBindings();
                }
                totalCosto += producto.Cantidad * producto.PrecioVenta;
            }
        }

        private void AddProductButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                float quantity = 1;
                if (pluProductoText.Text.Contains('*'))
                {
                    string[] cantidad = pluProductoText.Text.Split('*');
                    float.TryParse(cantidad[0], out quantity);
                    pluProductoText.Text = cantidad[1];
                }
                if(quantity > 0)
                    GetProductoByPLU(pluProductoText.Text.Trim(),quantity);
            }
        }

        private void PromotionsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            Promociones currentPromotion = grid.SelectedItem as Promociones;
            if(currentPromotion != null)
            {
                TabPageControl.SelectedIndex = 1;
                FillPromoData(_promotionsPresenter.GetPromocionById(currentPromotion.idPromocion));
            }
        }

        private void FillPromoData(Promociones promotion)
        {
            idPromocion = promotion.idPromocion;
            producsPromotionList.AddRange(promotion.Productos);
            NombrePromocionTxt.Text = promotion.NombrePromocion;
            DiasPromocionTxt.Text = promotion.DiasPromocion;
            PrecioTxt.Text = promotion.Monto.ToString();
            PorcentajeTxt.Text = promotion.Porcentaje.ToString();
            HoraInicio.Value = new DateTime(promotion.Inicio);
            HoraFin.Value = new DateTime(promotion.Fin);
            ComboDepartamentos.SelectedValue = promotion.idDepartamento != 0 ? promotion.idDepartamento : -1;
            ComboMarcas.SelectedValue = promotion.idMarca != 0 ? promotion.idMarca : -1;
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ProductosPromocion producto = button.DataContext as ProductosPromocion;
            if(producto != null)
            {
                if (producto.idProductoPromocion != 0)
                    _promotionsPresenter.DeleteProductoPromocion(producto.idProductoPromocion);
                producsPromotionList.Remove(producto);
            }
        }
    }
}
