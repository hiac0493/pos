using checkpoint.Views.Catalogs.Brands.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Brands.Services
{
    public interface IBrandServices
    {
        List<Marcas> GetAllMarcas();

        IEnumerable<Marcas> GetBrandsByName(string name);

        Task<Marcas> SaveBrand(Marcas marca);
    }
}
