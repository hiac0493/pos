using Microsoft.EntityFrameworkCore;
using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pos.DAL.Repository.Domain
{
    public class ImpuestosRepository : GenericRepository<Impuestos>, IImpuestosRepository
    {
        public ImpuestosRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> getImpuestoByName(string name)
        {
            name = String.IsNullOrEmpty(name) ? string.Empty : name.Trim();
            string[] names = name.Split(' ', ',');
            names = names.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            name = String.Join("%", names);
            name = "%" + name + "%";
            return dbContext.Impuestos
                .Where(x => (EF.Functions.Like(x.Descripcion.ToLower(), name.ToLower())))
                .ToList();
        }
    }
}
