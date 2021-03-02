using checkpoint.Views.Catalogs.Turns.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Turns.Services
{
    public interface ITurnsServices 
    {
        List<Turnos> GetAllTurns();

        IEnumerable<Turnos> GetTurnsByName(string name);

        Task<Turnos> SaveTurn(Turnos turno);
    }
}
