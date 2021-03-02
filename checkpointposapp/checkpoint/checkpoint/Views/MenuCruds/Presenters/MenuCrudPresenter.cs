using checkpoint.Views.MenuCruds.Models;
using checkpoint.Views.MenuCruds.Services;
using System.Collections.Generic;

namespace checkpoint.Views.MenuCruds.Presenters
{
    public class MenuCrudPresenter
    {
        private readonly IMenuCrudsServices _menuCrudsServices;

        public MenuCrudPresenter(IMenuCrudsServices menuCrudsServices)
        {
            _menuCrudsServices = menuCrudsServices;
        }

        public List<Pantallas> GetAllSubPantallas()
        {
            return _menuCrudsServices.GetAllSubPantallas();
        }
    }
}
