using checkpoint.Resources;
using checkpoint.Views.Catalogs.Screens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Screens.Services
{
    public class ScreenServices : IScreenService
    {
        public List<Pantallas> GetAllPantallas()
        {
            string webApiUrl = WebApiMethods.GetAllPantallas;
            IList<Pantallas> pantallasList = new List<Pantallas>();
            var ResponseOK = App.HttpTools.HttpGetList<Pantallas>(webApiUrl, ref pantallasList, "No se encontró la unidad");
            return ResponseOK == HttpStatusCode.OK ? pantallasList.ToList() : null;
        }

        public IEnumerable<Pantallas> GetScreenByName(string name)
        {
            string webApiUrl = WebApiMethods.GetScreenByName + name;
            IList<Pantallas> pantallasList = new List<Pantallas>();
            HttpStatusCode ResponseOk = App.HttpTools.HttpGetList<Pantallas>(webApiUrl, ref pantallasList, "Hubo un error en la lectura de las unidades");
            return ResponseOk == HttpStatusCode.OK ? pantallasList.ToList() : null;
        }


        public async Task<Pantallas> SaveScreen(Pantallas pantalla)
        {
            string webApiUrl = WebApiMethods.SaveScreen;
            Pantallas response = await App.HttpTools.HttpPostObjectWithResponseDataAsync<Pantallas, Pantallas>(webApiUrl, pantalla, "Hubo un error al guardar la unidad").ConfigureAwait(false);
            return response;
        }
    }
}
