using System;

namespace Pos.DAL.Repository.Domain
{
    internal class RouteAttribute : Attribute
    {
        private string v;

        public RouteAttribute(string v)
        {
            this.v = v;
        }
    }
}