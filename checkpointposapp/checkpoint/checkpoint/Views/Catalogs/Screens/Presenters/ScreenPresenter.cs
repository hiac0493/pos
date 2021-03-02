
using checkpoint.Views.Catalogs.Screens.Models;
using checkpoint.Views.Catalogs.Screens.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Screens.Presenters
{
    class ScreenPresenter
    {
        private readonly IScreenService _ScreenServices;

        public ScreenPresenter(IScreenService screenServices)
        {
            _ScreenServices = screenServices;
        }

        public IEnumerable<Pantallas> GetAllPantallas()
        {
            return _ScreenServices.GetAllPantallas();
        }

        public IEnumerable<Pantallas> GetScreenByName(string name)
        {
            return _ScreenServices.GetScreenByName(name);
        }

        public Task<Pantallas> SaveScreen(Pantallas pantalla)
        {
            return _ScreenServices.SaveScreen(pantalla);
        }
    }
}
