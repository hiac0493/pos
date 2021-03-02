using checkpoint.Views.Catalogs.Turns.Models;
using checkpoint.Views.Catalogs.Turns.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Turns.Presenters
{
    class TurnPresenter
    {
        private readonly ITurnsServices _turnServices;

        public TurnPresenter(ITurnsServices turnServices)
        {
            _turnServices = turnServices;
        }

        public IEnumerable<Turnos> GetAllTurns() 
        {
            return _turnServices.GetAllTurns();
        }

        public IEnumerable<Turnos> GetTurnsByName(string name)
        {
            return _turnServices.GetTurnsByName(name);
        }

        public Task<Turnos> SaveTurn(Turnos turno)
        {
            return _turnServices.SaveTurn(turno);
        }

    }
}
