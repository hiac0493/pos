using checkpoint.Views.Catalogs.UnitSat.Models;
using checkpoint.Views.Catalogs.UnitSat.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.UnitSat.Presenters
{
    public class UnitSatPresenter
    {
        private readonly IUnitSatServices _UnitsSatServices;

        public UnitSatPresenter(IUnitSatServices unitSatServices)
        {
            _UnitsSatServices = unitSatServices;
        }

        public IEnumerable<unidadesSat> GetAllUnitSat()
        {
            return _UnitsSatServices.GetAllUnitSat();
        }

        public IEnumerable<unidadesSat> GetUnidadesSatByid(string name)
        {
            return _UnitsSatServices.GetUnidadesSatByid(name);
        }

        public IEnumerable<unidadesSat> GetUnitSatByName(string name)
        {
            return _UnitsSatServices.GetUnitsSatByName(name);
        }

        public Task<unidadesSat> SaveUnitSat(unidadesSat unidadSat)
        {
            return _UnitsSatServices.SaveUnitSat(unidadSat);
        }
    }
}
