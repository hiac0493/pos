using checkpoint.Resources.SystemModels;
using checkpoint.Views.Catalogs.Config.Services;
using System.Collections.Generic;

namespace checkpoint.Views.Catalogs.Config.Presenters
{
    public class ConfigPresenter
    {
        private readonly IConfigServices _configServices;

        public ConfigPresenter(IConfigServices configServices)
        {
            _configServices = configServices;
        }
        public void WriteCashConfig(Caja data)
        {
            _configServices.WriteCashConfig(data);
        }
    }
}
