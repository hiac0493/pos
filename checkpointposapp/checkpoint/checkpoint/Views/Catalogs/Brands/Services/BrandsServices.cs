using checkpoint.Resources;
using checkpoint.Views.Catalogs.Brands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Brands.Services
{
    public class BrandsServices : IBrandServices
    {
        public List<Marcas> GetAllMarcas()
        {
            string webApiUrl = WebApiMethods.GetAllMarcas;
            IList<Marcas> marcasList = new List<Marcas>();
            var ResponseOK = App.HttpTools.HttpGetList<Marcas>(webApiUrl, ref marcasList, "No se encontró la marca");
            if(ResponseOK == HttpStatusCode.OK)
            {
                return marcasList.ToList();
            }
            else
            {
                return null;
            }

        }

        public IEnumerable<Marcas> GetBrandsByName(string name)
        {
            string webApiUrl = WebApiMethods.GetBrandsByName + name;
            IList<Marcas> marcasList = new List<Marcas>();
            HttpStatusCode ResponseOk = App.HttpTools.HttpGetList<Marcas>(webApiUrl, ref marcasList, "Hubo un error en la lectura de las marcas");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return marcasList != null ? marcasList.ToList() : null;
            }
            else
            {
                return null;
            }
        }

        public async Task<Marcas> SaveBrand(Marcas marca)
        {
            string webApiUrl = WebApiMethods.SaveBrand;
            Marcas supplierResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<Marcas, Marcas>(webApiUrl, marca, "Hubo un error el guardar la marca").ConfigureAwait(false);
            return supplierResult;
        }


    }
}
