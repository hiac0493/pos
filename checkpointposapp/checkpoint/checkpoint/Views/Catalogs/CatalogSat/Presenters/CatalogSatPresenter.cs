using checkpoint.Views.Catalogs.CatalogSat.Models;
using checkpoint.Views.Catalogs.CatalogSat.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.CatalogSat.Presenters
{
    public class CatalogSatPresenter
    {
        private readonly ICatalogSatServices _CatalogSatServices;

        public CatalogSatPresenter(ICatalogSatServices catalogSatServices)
        {
            _CatalogSatServices = catalogSatServices;
        }

        public IEnumerable<catalogoSat> GetAllCatalogSat()
        {
            return _CatalogSatServices.GetAllCatalogSat();
        }

        public IEnumerable<catalogoSat> GetCatalogSatByName(string name)
        {
            return _CatalogSatServices.GetCatalogSatByName(name);
        }

        public catalogoSat SaveCatalogSat(catalogoSat catalogSat)
        {
            return _CatalogSatServices.SaveCatalogSat(catalogSat).Result;
        }

        public catalogoSat GetCatalogByClave(string claveCatalogo)
        {
            return _CatalogSatServices.GetCatalogByClave(claveCatalogo);
        }
    }
}
