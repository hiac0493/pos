using checkpoint.Resources;
using checkpoint.Views.Catalogs.UnitSat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.UnitSat.Services
{
    public class UnitSatServices : IUnitSatServices
    {
        public List<unidadesSat> GetAllUnitSat()
        {
            string webApiUrl = WebApiMethods.GetAllUnidadesSat;
            IList<unidadesSat> unidadesSatList = new List<unidadesSat>();
            var ResponseOK = App.HttpTools.HttpGetList<unidadesSat>(webApiUrl, ref unidadesSatList, "No se encontró la unidad");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return unidadesSatList.ToList();
            }
            else
            {
                return null;
            }

        }

        public IEnumerable<unidadesSat> GetUnidadesSatByid(string name)
        {
            string webApiUrl = WebApiMethods.GetUnidadesSatByid + name;
            IList<unidadesSat> unidadesSatList = new List<unidadesSat>();
            HttpStatusCode ResponseOk = App.HttpTools.HttpGetList<unidadesSat>(webApiUrl, ref unidadesSatList, "Hubo un error en la lectura de las unidades");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return unidadesSatList != null ? unidadesSatList.ToList() : null;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<unidadesSat> GetUnitsSatByName(string name)
        {
            string webApiUrl = WebApiMethods.GetUnitsSatByName + name;
            IList<unidadesSat> unidadesList = new List<unidadesSat>();
            HttpStatusCode ResponseOk = App.HttpTools.HttpGetList<unidadesSat>(webApiUrl, ref unidadesList, "Hubo un error en la lectura de las unidades");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return unidadesList != null ? unidadesList.ToList() : null;
            }
            else
            {
                return null;
            }
        }

        public async Task<unidadesSat> SaveUnitSat(unidadesSat unidadSat)
        {
            string webApiUrl = WebApiMethods.SaveUnitSat;
            unidadesSat unidadesSatResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<unidadesSat, unidadesSat>(webApiUrl, unidadSat, "Hubo un error al guardar la unidad").ConfigureAwait(false);
            return unidadesSatResult;
        }
    }
}
