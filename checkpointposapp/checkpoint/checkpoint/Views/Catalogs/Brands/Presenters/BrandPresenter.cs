using checkpoint.Views.Catalogs.Brands.Models;
using checkpoint.Views.Catalogs.Brands.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Brands.Presenters
{
    public class BrandPresenter
    {
        private readonly IBrandServices _brandsServices;
        
        public BrandPresenter(IBrandServices brandServices)
        {
            _brandsServices = brandServices;
        }

        public IEnumerable<Marcas> GetAllMarcas()
        {
            return _brandsServices.GetAllMarcas();
        }

        public IEnumerable<Marcas> GetBrandsByName(string name)
        {
            return _brandsServices.GetBrandsByName(name);
        }

        public Task<Marcas> SaveBrand(Marcas marca)
        {
            return _brandsServices.SaveBrand(marca);
        }
    }
}
