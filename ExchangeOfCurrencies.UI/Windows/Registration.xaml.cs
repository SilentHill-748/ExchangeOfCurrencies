using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using DataAnnotations = System.ComponentModel.DataAnnotations;

using ExchangeOfCurrencies.UI.Windows.MessageWindows;
using ExchangeOfCurrencies.ClientModel;
using ExchangeOfCurrencies.DbClient;

namespace ExchangeOfCurrencies.UI.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AutorizationWindow loginWin = new ();
            loginWin.Show();
            this.Close();
        }

        private void RegL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Validation();
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

        private void Validation()
        {
            var errors = new List<DataAnnotations.ValidationResult>();
            try
            {
                Dictionary<string, object> personalData = new();
                FillPersonalDataCollection(personalData);
                User currentUser = new(personalData);
                errors = currentUser.Validate(new DataAnnotations.ValidationContext(currentUser)).ToList();
                if (errors.Count > 0) 
                    throw new Exception("Некорректный ввод данных!");
            }
            catch (Exception ex)
            {
                errors = errors.Prepend(new DataAnnotations.ValidationResult(ex.Message)).ToList();
                ShowErrors(errors);
            }
        }

        private void ShowErrors(IEnumerable<DataAnnotations.ValidationResult> errors)
        {
            string message = string.Join("\n", errors.Select(e => e.ErrorMessage));
            //MessageBox.Show(message, "Внимание");
            Message error = new (message, "Внимание!");
            error.ShowDialog();
        }
    }
}
