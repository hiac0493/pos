using checkpoint.Resources;
using checkpoint.Views.Catalogs.Turns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Turns.Services
{
    public class TurnsServices : ITurnsServices
    {
        public List<Turnos> GetAllTurns()
        {
            string webApiUrl = WebApiMethods.GetAllTurns;
            IList<Turnos> turnsList = new List<Turnos>();
            var ResponseOK = App.HttpTools.HttpGetList<Turnos>(webApiUrl, ref turnsList, "No se encontró el turno");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return turnsList.ToList();
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<Turnos> GetTurnsByName(string name)
        {
            string webApiUrl = WebApiMethods.GetTurnsByName + name;
            IList<Turnos> turnsList = new List<Turnos>();
            HttpStatusCode ResponseOk = App.HttpTools.HttpGetList<Turnos>(webApiUrl, ref turnsList, "Hubo un error en la lectura de los turnos");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return turnsList != null ? turnsList.ToList() : null;
            }
            else
            {
                return null;
            }
        }

        public async Task<Turnos> SaveTurn(Turnos turno)
        {
            string webApiUrl = WebApiMethods.SaveTurn;
            Turnos response = await App.HttpTools.HttpPostObjectWithResponseDataAsync<Turnos, Turnos>(webApiUrl, turno, "Hubo un error al guardar el turno").ConfigureAwait(false);
            return response;
        }
    }
}
