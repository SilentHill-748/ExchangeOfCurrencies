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
        public static readonly DependencyProperty IsPasswordProperty;
        public static readonly DependencyProperty PasswordVisibilityProperty;
        public static readonly DependencyProperty TextBoxVisibilityProperty;
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

            IsPasswordProperty = DependencyProperty.Register(
                "IsPassword",
                typeof(bool),
                typeof(RegistrationFieldItem),
                new PropertyMetadata(false, OnIsPasswordChanged));

            PasswordVisibilityProperty = DependencyProperty.Register(
                "PasswordVisibility",
                typeof(Visibility),
                typeof(RegistrationFieldItem),
                new PropertyMetadata(Visibility.Collapsed));

            TextBoxVisibilityProperty = DependencyProperty.Register(
                "TextBoxVisibility",
                typeof(Visibility),
                typeof(RegistrationFieldItem),
                new PropertyMetadata(Visibility.Visible));
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

        public Visibility PasswordVisibility
        {
            get => (Visibility)GetValue(PasswordVisibilityProperty);
            set => SetValue(PasswordVisibilityProperty, value);
        }

        public Visibility TextBoxVisibility
        {
            get => (Visibility)GetValue(TextBoxVisibilityProperty);
            set => SetValue(TextBoxVisibilityProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
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

        private static void OnIsPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                d.SetValue(PasswordVisibilityProperty, Visibility.Visible);
                d.SetValue(TextBoxVisibilityProperty, Visibility.Collapsed);
            }
            else
            {
                d.SetValue(PasswordVisibilityProperty, Visibility.Collapsed);
                d.SetValue(TextBoxVisibilityProperty, Visibility.Visible);
            }
        }
    }
}