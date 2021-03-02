using checkpoint.Views.Catalogs.Screens.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Screens.Services
{
    public interface IScreenService
    {
        List<Pantallas> GetAllPantallas();

        IEnumerable<Pantallas> GetScreenByName(string name);

        Task<Pantallas> SaveScreen(Pantallas unidadSat);
    }
}
