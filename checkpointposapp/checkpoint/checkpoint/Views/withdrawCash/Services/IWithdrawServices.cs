using checkpoint.Views.CashClose.Models;
using checkpoint.Views.withdrawCash.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.withdrawCash.Services
{
    public interface IWithdrawServices
    {
        Task<Retiros> SaveRetiros(Retiros retiro);
        Cortes GetCurrentCashClose(int idUsuario);
        List<Retiros> GetAllRetiros(long idcorte);
        List<Retiros> GetAllCashIncomes(long idcorte);
        bool GetUserAdmin(string user, string pass);
    }
}
