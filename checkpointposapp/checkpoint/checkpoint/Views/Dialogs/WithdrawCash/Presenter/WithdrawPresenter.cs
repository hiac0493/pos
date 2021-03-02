using checkpoint.Views.Dialogs.WithdrawCash.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Dialogs.WithdrawCash.Presenter
{
    class WithdrawPresenter
    {
        private readonly IWithdrawServices _withdrawServices;
        public WithdrawPresenter(IWithdrawServices withdrawServices)
        {
            _withdrawServices = withdrawServices;
        }
        public bool GetUserAdmin(string user, string pass)
        {
            return _withdrawServices.GetUserAdmin(user, pass);
        }
    }
}
