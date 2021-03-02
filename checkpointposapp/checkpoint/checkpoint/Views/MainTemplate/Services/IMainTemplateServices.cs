using checkpoint.Resources.SystemModels;
using checkpoint.Views.MainTemplate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.MainTemplate.Services
{
    public interface IMainTemplateServices
    {
        List<Pantallas> GetAllPrincipalPantallas();
        SystemConfig cashConfig();
    }
}
