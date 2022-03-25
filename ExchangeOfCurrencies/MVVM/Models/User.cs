using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeOfCurrencies.MVVM.Models
{
    /// <summary>
    /// Autorizated user
    /// </summary>
    internal class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Login { get; set; }

        //TODO: Не забудь пароль шифровать и хранить шифр.
        public string Password { get; set; }
    }
}
