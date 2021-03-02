using Security.BAL.Interface.Domain;
using Security.BAL.Interface.DomainUoW;
using Security.DAL.Repository.Domain;
using Security.EntityFramework.Edbm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.DAL.Repository.DomainUoW
{
    public class SecurityUnitOfWork : ISecurityUnitOfWork
    {
        private readonly SecurityDbContext _context;
        
        public SecurityUnitOfWork(SecurityDbContext context)
        {
            _context = context;
            PantallasRepository = new PantallasRepository(_context);
            UsuariosRepository = new UsuariosRepository(_context);
        }
        public IPantallasRepository PantallasRepository { get; private set; }
        public IUsuariosRepository UsuariosRepository { get; private set; }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
