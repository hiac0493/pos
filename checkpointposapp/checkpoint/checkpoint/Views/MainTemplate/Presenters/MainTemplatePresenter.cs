using checkpoint.Resources.SystemModels;
using checkpoint.Views.MainTemplate.Models;
using checkpoint.Views.MainTemplate.Services;
using System.Collections.Generic;

namespace checkpoint.Views.MainTemplate.Presenters
{
    public class MainTemplatePresenter 
    {
        private readonly IMainTemplateServices _mainTemplateServices;

        public MainTemplatePresenter(IMainTemplateServices mainTemplateServices)
        {
            _mainTemplateServices = mainTemplateServices;
        }
        public List<Pantallas> GetAllPrincipalPantallas()
        {
            return _mainTemplateServices.GetAllPrincipalPantallas();
        }

        public SystemConfig cashConfig()
        {
            return _mainTemplateServices.cashConfig();
        }

    }
}
