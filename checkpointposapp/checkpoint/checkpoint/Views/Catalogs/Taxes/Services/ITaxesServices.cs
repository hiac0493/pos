using checkpoint.Views.Catalogs.Taxes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Taxes.Services
{
    public interface ITaxesServices
    {
        IEnumerable<Impuestos> getAllTaxes();
        List<Impuestos> getTaxesByName(string name);
        Task<Impuestos> saveTaxes(Impuestos impuestos);
    }
}
