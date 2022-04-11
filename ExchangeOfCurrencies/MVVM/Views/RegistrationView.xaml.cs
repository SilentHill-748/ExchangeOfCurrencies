using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ExchangeOfCurrencies.MVVM.Views
{
    public partial class RegistrationView : Window
    {
        private Dictionary<DependencyObject, int> _unvalidatedObjects = new();


        public RegistrationView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var children = ((Grid)this.Content).Children;

            foreach (DependencyObject d in children)
            {
                var errors = Validation.GetErrors(d);

                if (errors.Count > 0)
                {
                    _unvalidatedObjects[d] = errors.Count;
                }
                else
                    _unvalidatedObjects.Remove(d);
            }

            AllowBtn.IsEnabled = _unvalidatedObjects.Count == 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
