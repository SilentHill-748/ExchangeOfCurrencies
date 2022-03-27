using System.Windows;
using System.Windows.Controls;

namespace ExchangeOfCurrencies.UserControls
{
    public partial class RegistrationFieldItem : UserControl
    {
        #region Dependency properties
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty SeparatorWidthProperty;
        public static readonly DependencyProperty DescriptionProperty;
        public static readonly DependencyProperty DescriptionVisibilityProperty;
        #endregion

        #region Constructors
        static RegistrationFieldItem()
        {
            TextProperty = DependencyProperty.Register(
                "Text", 
                typeof(string), 
                typeof(RegistrationFieldItem));

            DescriptionProperty = DependencyProperty.Register(
                "Description", 
                typeof(string), 
                typeof(RegistrationFieldItem),
                new PropertyMetadata("", OnDescriptionChanged));

            SeparatorWidthProperty = DependencyProperty.Register(
                "SeparatorWidth", 
                typeof(double), 
                typeof(RegistrationFieldItem));

            DescriptionVisibilityProperty = DependencyProperty.Register(
                "DescriptionVisibility", 
                typeof(Visibility),
                typeof(RegistrationFieldItem),
                new PropertyMetadata(Visibility.Collapsed));
        }

        public RegistrationFieldItem()
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

        public double SeparatorWidth
        {
            get => (double)GetValue(SeparatorWidthProperty);
            set => SetValue(SeparatorWidthProperty, value);
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
