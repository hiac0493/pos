using Microsoft.EntityFrameworkCore;
using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pos.DAL.Repository.Domain
{
    public class UsuariosRepository : GenericRepository<Usuarios>, IUsuariosRepository
    {
        public UsuariosRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> GetUsuarioByName(string name)
        {
            name = String.IsNullOrEmpty(name) ? string.Empty : name.Trim();
            string[] names = name.Split(' ', ',');
            names = names.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            name = String.Join("%", names);
            name= "%" + name + "%";
            return dbContext.Usuarios
                .Where(x => (EF.Functions.Like((x.ApellidoPaterno+ " "+ x.ApellidoMaterno+ " "+ x.Nombres).ToLower(), name.ToLower())
                             || EF.Functions.Like((x.Nombres+ " "+ x.ApellidoPaterno+ " "+ x.ApellidoMaterno).ToLower(), name.ToLower())))
                .ToList();
        }

        public Usuarios GetUsuarioByUserName(string userName)
        {
            return dbContext.Usuarios.Where(x => x.NombreUsuario.Equals(userName)).SingleOrDefault();
        }

        public bool GetUsuarioAdmin(string user, string pass)
        {
            Usuarios usuario = dbContext.Usuarios.Where(x => x.NombreUsuario.Equals(user) && x.Contraseña.Equals(pass)).SingleOrDefault();
            return usuario.Tipo.Equals("Admin") ? true : false;
        }

    }
}
