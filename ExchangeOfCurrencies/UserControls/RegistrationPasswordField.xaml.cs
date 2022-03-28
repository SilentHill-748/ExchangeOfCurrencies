using System.Windows;
using System.Windows.Controls;

namespace ExchangeOfCurrencies.UserControls
{
    public partial class RegistrationPasswordField : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty TextProperty;
        #endregion

        #region Constructors
        static RegistrationPasswordField()
        {
            TextProperty = DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(RegistrationPasswordField));
        }


        public RegistrationPasswordField()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties for dependency properties
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion
    }
}
