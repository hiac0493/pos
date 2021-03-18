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
        public List<Pantallas> GetAllPrincipalPantallasByUserId(int idUsuario)
        {
            return _mainTemplateServices.GetAllPrincipalPantallasByUserId(idUsuario);
        }

        public SystemConfig cashConfig()
        {
            return _mainTemplateServices.cashConfig();
        }

    }
}
