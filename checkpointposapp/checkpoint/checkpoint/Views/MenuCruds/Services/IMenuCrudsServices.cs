using checkpoint.Views.MenuCruds.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.MenuCruds.Services
{
    public interface IMenuCrudsServices
    {
        List<Pantallas> GetAllSubPantallas();
    }
}
