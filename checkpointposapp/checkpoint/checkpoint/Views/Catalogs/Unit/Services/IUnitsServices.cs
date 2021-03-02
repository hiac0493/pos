using checkpoint.Views.Catalogs.Unit.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Unit.Services
{
    public interface IUnitsServices
    {
        List<Unidades> GetAllUnits();

        IEnumerable<Unidades> GetUnitsByName(string name);

        Task<Unidades> SaveUnit(Unidades unidad);
    }
}
