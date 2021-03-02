using checkpoint.Views.CashClose.Models;
using checkpoint.Views.withdrawCash.Models;
using checkpoint.Views.withdrawCash.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.withdrawCash.Presenters
{
    class WithdrawPresenter
    {
        private readonly IWithdrawServices _withdrawServices;
        public WithdrawPresenter(IWithdrawServices withdrawServices)
        {
            _withdrawServices = withdrawServices;
        }

        public Task<Retiros> SaveRetiros(Retiros retiro)
        {
            return _withdrawServices.SaveRetiros(retiro);
        }

        public Cortes GetCurrentCashClose(int idUsuario)
        {
            return _withdrawServices.GetCurrentCashClose(idUsuario);
        }
        public IEnumerable<Retiros> GetAllRetiros(long idusuario)
        {
            return _withdrawServices.GetAllRetiros(idusuario);
        }
        public IEnumerable<Retiros> GetAllCashIncomes(long idusuario)
        {
            return _withdrawServices.GetAllCashIncomes(idusuario);
        }
    }
}
