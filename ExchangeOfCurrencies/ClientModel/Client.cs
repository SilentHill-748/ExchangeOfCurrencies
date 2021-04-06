using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeOfCurrencies.ClientModel
{
    public class Client : Person
    {
        // TODO: Описать клиента.
        public Client()
        {

        }

        public void BuyCurrency()
        {

        }

        public void SellCurrency()
        {

        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
