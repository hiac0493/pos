﻿using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.DAL.Repository.Domain
{
    public class ProductosVentaRepository : GenericRepository<ProductosVenta>, IProductosVentaRepository
    {
        public ProductosVentaRepository(PosDbContext context) : base(context) { }
        public PosDbContext DbContext
        {
            get { return _context as PosDbContext; }
        }
    }
}
