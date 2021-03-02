using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckPoint.WebApi.Dtos
{
    public class CancelacionDto
    {
        public int idUsuario { get; set; }
        public long folioVenta { get; set; }
    }
}
