using checkpoint.Resources;
using checkpoint.Views.MenuCruds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace checkpoint.Views.MenuCruds.Services
{
    public class MenuCrudsServices : IMenuCrudsServices
    {
        public List<Pantallas> GetAllSubPantallas()
        {
            string webApiUrl = WebApiMethods.GetAllSubPantallas;
            IList<Pantallas> pantallasResult = new List<Pantallas>();
            var result = App.HttpTools.HttpGetList<Pantallas>(webApiUrl, ref pantallasResult, "Hubo un error en la lectura de las pantallas de catalogos");
            if(result == HttpStatusCode.OK)
                return pantallasResult.ToList();
            else
                return null;
        }
    }
}
