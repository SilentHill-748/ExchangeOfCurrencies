using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataAnnotations = System.ComponentModel.DataAnnotations;

using ExchangeOfCurrencies.UI.Windows.MessageWindows;
using ExchangeOfCurrencies.ClientModel;
using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.UI.Windows
{
    public partial class Registration : Window
    {
        private User currentUser;
        public Registration()
        {
            InitializeComponent();
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AutorizationWindow loginWin = new ();
            loginWin.Show();
            this.Close();
        }

        private void RegL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!Validation()) return;
                string quary = BuildQuaryString();
                Request.Send(quary);
                this.Close();
            }
            catch (Exception ex)
            {
                Message mes = new (ex.Message, "Внимание!");
                mes.ShowDialog();
            }
        }

        private void FillPersonalDataCollection(Dictionary<string, object> personalData)
        {
            Control[] inputBoxes =
            { LoginText, PassBox, ConfPassBox, FirstNameText, MiddleNameText, LastNameText, PhoneText, EmailText };
            string[] keys =
            { "Login", "Password", "ConfirmPassword", "FirstName", "MiddleName", "LastName", "Phone", "Email" };

            string value = "";
            for (int i = 0; i < inputBoxes.Length; i++)
            {
                if (inputBoxes[i] is TextBox tb)
                    value = tb.Text;
                else if (inputBoxes[i] is PasswordBox pb)
                    value = pb.Password;
                personalData.Add(keys[i], value);
            }
        }

        private bool Validation()
        {
            var errors = new List<DataAnnotations.ValidationResult>();
            try
            {
                Dictionary<string, object> personalData = new();
                FillPersonalDataCollection(personalData);
                currentUser = new(personalData);

                DataAnnotations.ValidationContext context = new (currentUser);
                if (!DataAnnotations.Validator.TryValidateObject(currentUser, context, errors, true))
                    throw new Exception("Регистрационные данные введены некорректно!");
                return true;
            }
            catch (Exception ex)
            {
                errors = errors.Prepend(new DataAnnotations.ValidationResult(ex.Message)).ToList();
                ShowErrors(errors);
                return false;
            }
        }

        private void ShowErrors(IEnumerable<DataAnnotations.ValidationResult> errors)
        {
            string message = string.Join("\n", errors.Select(e => e.ErrorMessage));
            Message error = new (message, "Внимание!");
            error.ShowDialog();
        }

        private string BuildQuaryString()
        {
            string quary = "INSERT INTO users(firstname, secondname, lastname, email, phone, login, password) ";
            var propertyValuesOfCurrentUser = currentUser.GetType().GetProperties().
                Select(p => p.GetValue(currentUser)).ToArray();

            string values = GetValuesFromProperties(propertyValuesOfCurrentUser);
            return quary + $"VALUES ({values});";
        }

        private string GetValuesFromProperties(object[] propertyValues)
        {
            string result = "";
            for (int i = 0; i < propertyValues.Length - 1; i++)
            {
                if (i < propertyValues.Length - 2)
                    result += $"\'{propertyValues[i]}\', ";
                else
                    result += $"\'{propertyValues[i]}\'";
            }
            return result;
        }
    }
}
