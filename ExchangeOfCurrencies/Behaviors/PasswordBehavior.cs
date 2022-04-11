using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

namespace ExchangeOfCurrencies.Behaviors
{
    internal class PasswordBehavior : Behavior<PasswordBox>
    {
        public static readonly DependencyProperty PasswordProperty = 
            DependencyProperty.Register(
                "Password", 
                typeof(string),
                typeof(PasswordBehavior));


        bool _skipUpdate;


        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += PasswordBox_PasswordChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= PasswordBox_PasswordChanged;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == PasswordProperty)
            {
                if (!_skipUpdate)
                {
                    if (AssociatedObject is null)
                        return;

                    _skipUpdate = true;
                    AssociatedObject.Password = e.NewValue as string;
                    _skipUpdate = false;
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _skipUpdate = true;
            Password = AssociatedObject.Password;
            _skipUpdate = false;
        }
    }
}
