using checkpoint.Resources;
using checkpoint.Views.Catalogs.Taxes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Taxes.Services
{
    public class TaxesServices : ITaxesServices
    {
        public IEnumerable<Impuestos> getAllTaxes()
        {
            string webApiUrl = WebApiMethods.GetAllImpuestos;
            IList<Impuestos> impuestosList = new List<Impuestos>();
            var ResponseOK = App.HttpTools.HttpGetList<Impuestos>(webApiUrl, ref impuestosList, "No se encontró el impuesto");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return impuestosList.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<Impuestos> getTaxesByName(string name)
        {
            string webApiUrl = WebApiMethods.GetImpuestoByName + name;
            IList<Impuestos> impuestosList = new List<Impuestos>();
            var ResponseOk = App.HttpTools.HttpGetList<Impuestos>(webApiUrl, ref impuestosList, "No se encontró el impuesto");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return impuestosList.ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<Impuestos> saveTaxes(Impuestos impuestos)
        {
            string webApiUrl = WebApiMethods.SaveImpuesto;
            Impuestos impuestoResult = await App.HttpTools
                .HttpPostObjectWithResponseDataAsync<Impuestos, Impuestos>(webApiUrl, impuestos, "Hubo un error en el guardado del impuesto").ConfigureAwait(false); ;
            return impuestoResult;
        }
    }
}
