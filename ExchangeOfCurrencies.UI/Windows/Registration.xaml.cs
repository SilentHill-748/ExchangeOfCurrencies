using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataAnnotations = System.ComponentModel.DataAnnotations;

using ExchangeOfCurrencies.UI.Windows.MessageWindows;
using ExchangeOfCurrencies.ClientModel.Validation;
using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.UI.Windows
{
    public partial class Registration : Window
    {
        private readonly List<Control> inputFieldsOfPersonalData;

        public Registration()
        {
            InitializeComponent();
            inputFieldsOfPersonalData = new List<Control>();
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseWindow();
        }

        private void PersonalDataBox_GotFocus(object sender, RoutedEventArgs e)
        {
            object eventSource = e.Source;
            if (!inputFieldsOfPersonalData.Contains(eventSource))
            {
                inputFieldsOfPersonalData.Add(eventSource as Control);
            }
        }

        private void RegL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<string> personalData = GetValuesFromInputFields();
            try
            {
                if (!Validation(personalData)) return;
                string quary = BuildQuaryString(personalData);
                Request.Send(quary);
                CloseWindow();
            }
            catch (Exception ex)
            {
                Message mes = new (ex.Message, "Внимание!");
                mes.ShowDialog();
            }
        }

        private bool Validation(List<string> personalData)
        {
            UserValidationContext validation = new (personalData);
            try
            {
                if (!validation.Validate())
                {
                    throw new Exception();
                }
                return true;
            }
            catch (Exception)
            {
                ShowErrors(validation.Errors);
                return false;
            }
        }

        private void ShowErrors(IEnumerable<DataAnnotations.ValidationResult> errors)
        {
            string message = string.Join("\n", errors.Select(e => e.ErrorMessage));
            Message error = new (message, "Внимание!");
            error.ShowDialog();
        }

        private List<string> GetValuesFromInputFields()
        {
            List<string> result = new ();
            foreach (Control control in inputFieldsOfPersonalData)
            {
                if (control is TextBox tb)
                {
                    result.Add(tb.Text);
                }
                else
                {
                    result.Add((control as PasswordBox).Password);
                }
            }
            return result;
        }

        private string BuildQuaryString(List<string> personalData)
        {
            string quary = "INSERT INTO users (id, firstname, secondname, lastname, phone, email, login, password) ";
            string values = GetValuesFromProperties(personalData);
            return quary + $"VALUES ({values});";
        }

        // Приводит каждую строку данных к нужному формату и записывает всё в 1 строку.
        private string GetValuesFromProperties(List<string> insertValues)
        {
            insertValues.RemoveAt(7);
            insertValues = insertValues.Prepend((GetPreviousUserId() + 1).ToString()).ToList();
            var setFormat = insertValues.Select(value => $"\'{value}\'").ToArray();
            return string.Join(",", setFormat);
        }

        private int GetPreviousUserId()
        {
            DataRow row = Request.Send("SELECT MAX(users.id) FROM users;").
                Tables[0].Rows[0];
            if (row.ItemArray[0] is DBNull)
            {
                return 0;
            }
            return Convert.ToInt32(row.ItemArray[0]);
        }

        private void CloseWindow()
        {
            AutorizationWindow autorization = new();
            autorization.Show();
            Close();
        }
    }
}
