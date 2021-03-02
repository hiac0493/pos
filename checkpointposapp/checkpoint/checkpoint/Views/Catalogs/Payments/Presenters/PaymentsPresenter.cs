using checkpoint.Views.Catalogs.Payments.Models;
using checkpoint.Views.Catalogs.Payments.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Payments.Presenters
{
    public class PaymentsPresenter : IPaymentsServices
    {
        public readonly IPaymentsServices _paymentPresenter;
        public PaymentsPresenter(IPaymentsServices paymentsServices)
        {
            _paymentPresenter = paymentsServices;
        }

        public IEnumerable<TipoPago> getAllPayments()
        {
            return _paymentPresenter.getAllPayments();
        }

        public List<TipoPago> getPaymentByName(string name)
        {
            return _paymentPresenter.getPaymentByName(name);
        }

        public Task<TipoPago> savePayment(TipoPago tipoPago)
        {
            return _paymentPresenter.savePayment(tipoPago);
        }
    }
}
