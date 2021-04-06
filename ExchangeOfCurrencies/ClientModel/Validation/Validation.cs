using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace ExchangeOfCurrencies.ClientModel.Validation
{
    public class ControlValidation
    {
        private enum TextBoxName
        {
            Name,
            SecondName,
            LastName,
            Phone,
            Email,
            Login,
            Password,
            ConfirmPassword
        }

        /// <summary>
        ///    Произведёт валидацию введенных данных в указанный контрол TextBox. 
        /// </summary>
        /// <returns>
        ///     True, если валидация прошла успешно, иначе false.
        /// </returns>
        //public bool Validate(Control control, Func<bool> validationContext)
        //{
            
        //}

        //private bool ValidateTextBox(TextBox tb, TextBoxName name)
        //{

        //}
    }
}
