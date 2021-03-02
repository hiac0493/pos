using Microsoft.EntityFrameworkCore;
using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pos.DAL.Repository.Domain
{
    public class DepartamentosRepository : GenericRepository<Departamentos>, IDepartamentosRepository
    {
        public DepartamentosRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> getDepartmenByName(string name)
        {
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim();
            string[] names = name.Split(' ', ',');
            names = names.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            name = string.Join("%", names);
            name = "%" + name + "%";
            return dbContext.Departamentos
                .Where(x => (EF.Functions.Like(x.Descripcion.ToLower(), name.ToLower())))
                .ToList();
        }
    }
}
