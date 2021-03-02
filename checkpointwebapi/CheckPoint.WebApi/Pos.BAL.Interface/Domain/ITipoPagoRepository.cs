﻿using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.BAL.Interface.Domain
{
    public interface ITipoPagoRepository : IGenericRepository<TipoPago>
    {
        IEnumerable<object> GetPaymentByName(string name);
    }
}
