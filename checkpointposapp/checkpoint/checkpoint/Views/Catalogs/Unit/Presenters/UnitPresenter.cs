using checkpoint.Views.Catalogs.Unit.Models;
using checkpoint.Views.Catalogs.Unit.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Unit.Presenters
{
    public class UnitPresenter
    {
        private readonly IUnitsServices _unitsServices;

        public UnitPresenter(IUnitsServices unitServices)
        {
            _unitsServices = unitServices;
        }

        public IEnumerable<Unidades> GetAllUnits()
        {
            return _unitsServices.GetAllUnits();
        }

        public IEnumerable<Unidades> GetUnitsByName(string name)
        {
            return _unitsServices.GetUnitsByName(name);
        }

        public Task<Unidades> SaveUnit(Unidades unidad)
        {
            return _unitsServices.SaveUnit(unidad);
        }
    }
}
