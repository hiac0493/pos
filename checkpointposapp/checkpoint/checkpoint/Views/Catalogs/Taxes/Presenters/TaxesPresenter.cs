using checkpoint.Views.Catalogs.Taxes.Models;
using checkpoint.Views.Catalogs.Taxes.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Taxes.Presenters
{
    public class TaxesPresenter : ITaxesServices
    {
        public readonly ITaxesServices _taxesPresenter;
        public TaxesPresenter(ITaxesServices taxesServices)
        {
            _taxesPresenter = taxesServices;
        }

        public IEnumerable<Impuestos> getAllTaxes()
        {
            return _taxesPresenter.getAllTaxes();
        }

        public List<Impuestos> getTaxesByName(string name)
        {
            return _taxesPresenter.getTaxesByName(name);
        }

        public Task<Impuestos> saveTaxes(Impuestos impuestos)
        {
            return _taxesPresenter.saveTaxes(impuestos);
        }
    }
}
