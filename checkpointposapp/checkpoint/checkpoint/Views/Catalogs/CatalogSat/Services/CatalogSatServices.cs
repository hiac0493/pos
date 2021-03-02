using checkpoint.Resources;
using checkpoint.Views.Catalogs.CatalogSat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.CatalogSat.Services
{
    public class CatalogSatServices : ICatalogSatServices
    {
        public List<catalogoSat> GetAllCatalogSat()
        {
            string webApiUrl = WebApiMethods.GetAllCatalogoSat;
            IList<catalogoSat> catalogoSatList = new List<catalogoSat>();
            var ResponseOK = App.HttpTools.HttpGetList<catalogoSat>(webApiUrl, ref catalogoSatList, "No se encontró el catalogo");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return catalogoSatList.ToList();
            }
            else
            {
                return null;
            }

        }

        public IEnumerable<catalogoSat> GetCatalogSatByName(string name)
        {
            string webApiUrl = WebApiMethods.GetCatalogByName + name;
            IList<catalogoSat> catalogList = new List<catalogoSat>();
            HttpStatusCode ResponseOk = App.HttpTools.HttpGetList<catalogoSat>(webApiUrl, ref catalogList, "Hubo un error en la lectura del catalogo");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return catalogList != null ? catalogList.ToList() : null;
            }
            else
            {
                return null;
            }
        }

        public async Task<catalogoSat> SaveCatalogSat(catalogoSat catalogSat)
        {
            string webApiUrl = WebApiMethods.SaveCatalogSat;
            catalogoSat responseResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<catalogoSat, catalogoSat>(webApiUrl, catalogSat, "Hubo un error al guardar el catalogo").ConfigureAwait(false);
            return responseResult;
        }
    }
}
