using checkpoint.Resources;
using checkpoint.Views.Catalogs.Unit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Unit.Services
{
    public class UnitsServices : IUnitsServices
    {
        public List<Unidades> GetAllUnits()
        {
            string webApiUrl = WebApiMethods.GetAllUnidades;
            IList<Unidades> unidadesList = new List<Unidades>();
            var ResponseOK = App.HttpTools.HttpGetList<Unidades>(webApiUrl, ref unidadesList, "No se encontró la unidad");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return unidadesList.ToList();
            }
            else
            {
                return null;
            }

        }

        public IEnumerable<Unidades> GetUnitsByName(string name)
        {
            string webApiUrl = WebApiMethods.GetUnitsByName + name;
            IList<Unidades> unidadList = new List<Unidades>();
            HttpStatusCode ResponseOk = App.HttpTools.HttpGetList<Unidades>(webApiUrl, ref unidadList, "Hubo un error en la lectura de las unidades");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return unidadList != null ? unidadList.ToList() : null;
            }
            else
            {
                return null;
            }
        }

        public async Task<Unidades> SaveUnit(Unidades unidad)
        {
            string webApiUrl = WebApiMethods.SaveUnit;
            Unidades unitResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<Unidades, Unidades>(webApiUrl, unidad, "Hubo un error al guardar la unidad").ConfigureAwait(false);
            return unitResult;
        }
    }
}
