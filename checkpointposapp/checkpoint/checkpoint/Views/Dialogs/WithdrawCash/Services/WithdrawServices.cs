using checkpoint.Resources;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace checkpoint.Views.Dialogs.WithdrawCash.Services
{
    public class WithdrawServices : IWithdrawServices
    {
        public bool GetUserAdmin(string user, string pass)
        {
            string webApiUrl = WebApiMethods.GetUserAdmin + $"?user={user}&pass={pass}";
            bool flag = false;
            var response = App.HttpTools.HttpGetSingle(webApiUrl, ref flag, "Hubo un error al obtener el folio");
            if (response == HttpStatusCode.OK)
                return flag;
            else
                return false;
        }
    }
}
