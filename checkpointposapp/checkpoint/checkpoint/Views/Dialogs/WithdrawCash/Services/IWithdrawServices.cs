using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Dialogs.WithdrawCash.Services
{
    public interface IWithdrawServices
    {
        bool GetUserAdmin(string user, string pass);
    }
}
