using checkpoint.Resources.SystemModels;
using checkpoint.Views.Catalogs.Config.Presenters;
using checkpoint.Views.Catalogs.Config.Services;
using System.Windows;
using System.Windows.Controls;

namespace checkpoint.Views.Catalogs.Config.Views
{
    /// <summary>
    /// Lógica de interacción para Config.xaml
    /// </summary>
    public partial class Config : UserControl
    {
        Caja xml = new Caja();
        private ConfigPresenter _configPresenter;

        public Config()
        {
            InitializeComponent();
            _configPresenter = new ConfigPresenter(new ConfigServices());
        }

        public void saveConfig_Click(object sender, RoutedEventArgs e)
        {
            xml.name = "MINI MARKET";
            xml.fiscal_name = "CHECKPOINT";
            xml.rfc = "MMC110808MA8";
            xml.adress = "CARRETERA ESTATAL LIBRAMIENTO NORTE SANTIAGO OCEGUERA KM 12 MARQUEZ DE LEON LA PAZ BAJA CALIFORNIA SUR MEXICO 23030";
            _configPresenter.WriteCashConfig(xml);
        }
    }
}
