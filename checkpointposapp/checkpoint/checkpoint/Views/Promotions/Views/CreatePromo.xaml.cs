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
                DiasPromocion = DiasPromocionTxt.Text.Trim(),
                Estatus = true,
                Fin = ((DateTime)HoraFin.Value).Ticks,
                Inicio = ((DateTime)HoraInicio.Value).Ticks,
                NombrePromocion = NombrePromocionTxt.Text.Trim(),
                idDepartamento = (int)ComboDepartamentos.SelectedValue,
                idMarca = (int)ComboMarcas.SelectedValue,
                Productos = producsPromotionList.ToList(),
                Monto = PrecioTxt.Text.Trim().Length > 0 ? (float?)float.Parse(PrecioTxt.Text.Trim()) : null,
                Porcentaje = !String.IsNullOrEmpty(PorcentajeTxt.Text.Trim()) ? (float?)float.Parse(PorcentajeTxt.Text.Trim()) : null
            });
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            GetProductoByPLU(pluProductoText.Text.Trim());
        }

        private void GetProductoByPLU(string plu)
        {
            pluProductoText.Text = string.Empty;
            ProductosPromocion producto = _promotionsPresenter.GetProductoByPLU(plu);
            producto.Cantidad = 1;
            if (producto != null)
                producsPromotionList.Add(producto);

        }

        private void AddProductButton_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                GetProductoByPLU(pluProductoText.Text.Trim());
            }
        }

        private void PromotionsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            Promociones currentPromotion = grid.SelectedItem as Promociones;
            if(currentPromotion != null)
            {
                TabPageControl.SelectedIndex = 1;
                FillPromoData(currentPromotion);
            }
        }

        private void FillPromoData(Promociones promotion)
        {
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
    }
}
