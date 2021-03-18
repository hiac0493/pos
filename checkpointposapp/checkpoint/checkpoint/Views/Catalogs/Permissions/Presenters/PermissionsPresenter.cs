using checkpoint.Views.Catalogs.Permissions.Models;
using checkpoint.Views.Catalogs.Permissions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Catalogs.Permissions.Presenters
{
    public class PermissionsPresenter
    {
        private readonly IPermissionsServices _permissionsServices;
        
        public PermissionsPresenter(IPermissionsServices permissionsServices)
        {
            _permissionsServices = permissionsServices;
        }

        public IEnumerable<Usuarios> GetAllUsuariosWithPermissions()
        {
            return _permissionsServices.GetAllUsuariosWithPermissions();
        }

        public IEnumerable<Pantallas> GetAllPantallas()
        {
            return _permissionsServices.GetAllPantallas();
        }

        public void DeleteImpuestoProductoById(long idImpuestoProducto)
        {
            _permissionsServices.DeletePantallaUsuario(idImpuestoProducto);
        }

        public Usuarios UpdateUserPermissionsScreens(PermissionsUserDto usuarioUpdate)
        {
            return _permissionsServices.UpdateUserPermissionsScreens(usuarioUpdate);
        }
    }
}
