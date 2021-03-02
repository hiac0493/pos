using checkpoint.Views.Catalogs.Payments.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Payments.Services
{
    public interface IPaymentsServices
    {
        IEnumerable<TipoPago> getAllPayments();
        List<TipoPago> getPaymentByName(string name);
        Task<TipoPago> savePayment(TipoPago tipoPago);
    }
}
