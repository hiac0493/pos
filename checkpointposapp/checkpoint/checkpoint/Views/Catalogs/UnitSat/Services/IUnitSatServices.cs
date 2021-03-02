using checkpoint.Views.Catalogs.UnitSat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.UnitSat.Services
{
    public interface IUnitSatServices
    {
        List<unidadesSat> GetAllUnitSat();

        IEnumerable<unidadesSat> GetUnidadesSatByid(string name);

        IEnumerable<unidadesSat> GetUnitsSatByName(string name);

        Task<unidadesSat> SaveUnitSat(unidadesSat unidadSat);
    }
}
