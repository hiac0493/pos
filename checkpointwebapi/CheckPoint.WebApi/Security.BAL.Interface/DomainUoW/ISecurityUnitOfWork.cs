using Security.BAL.Interface.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.BAL.Interface.DomainUoW
{
    public interface ISecurityUnitOfWork : IDisposable
    {
        IPantallasRepository PantallasRepository { get; }
        IUsuariosRepository UsuariosRepository { get; }
        int Save();
    }
}
