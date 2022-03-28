using System.Windows;
using System.Windows.Controls;

namespace ExchangeOfCurrencies.UserControls
{
    public partial class RegistrationField : UserControl
    {
        #region Dependency properties
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty DescriptionProperty;
        public static readonly DependencyProperty DescriptionVisibilityProperty;
        #endregion

        #region Constructors
        static RegistrationField()
        {
            TextProperty = DependencyProperty.Register(
                "Text", 
                typeof(string), 
                typeof(RegistrationField));

            DescriptionProperty = DependencyProperty.Register(
                "Description", 
                typeof(string), 
                typeof(RegistrationField),
                new PropertyMetadata(string.Empty, OnDescriptionChanged));

            DescriptionVisibilityProperty = DependencyProperty.Register(
                "DescriptionVisibility", 
                typeof(Visibility),
                typeof(RegistrationField),
                new PropertyMetadata(Visibility.Collapsed));
        }

        public RegistrationField()
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

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public Visibility DescriptionVisibility
        {
            get => (Visibility)GetValue(DescriptionVisibilityProperty);
            set => SetValue(DescriptionVisibilityProperty, value);
        }
        #endregion

        private static void OnDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string description = (string)d.GetValue(DescriptionProperty);

            if (string.IsNullOrEmpty(description))
            {
                d.SetValue(DescriptionVisibilityProperty, Visibility.Collapsed);
            }
            else
                d.SetValue(DescriptionVisibilityProperty, Visibility.Visible);
        }
    }
}