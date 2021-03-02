using checkpoint.Views.Catalogs.CatalogSat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.CatalogSat.Services
{
    public interface ICatalogSatServices
    {
        List<catalogoSat> GetAllCatalogSat();

        IEnumerable<catalogoSat> GetCatalogSatByName(string name);


        Task<catalogoSat> SaveCatalogSat(catalogoSat catalogSat);

    }
}
