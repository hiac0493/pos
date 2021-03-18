using checkpoint.Views.Catalogs.Permissions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Catalogs.Permissions.Services
{
    public interface IPermissionsServices
    {
        IEnumerable<Usuarios> GetAllUsuariosWithPermissions();
        IEnumerable<Pantallas> GetAllPantallas();
        void DeletePantallaUsuario(long idPantallaUsuario);
        Usuarios UpdateUserPermissionsScreens(PermissionsUserDto permissions);
    }
}
