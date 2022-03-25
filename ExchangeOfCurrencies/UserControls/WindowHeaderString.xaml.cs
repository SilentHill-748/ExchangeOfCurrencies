using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExchangeOfCurrencies.UserControls
{
    public partial class WindowHeaderString : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty ImageSourceProperty; //path for icon image
        public static readonly DependencyProperty CloseButtonProperty;
        public static readonly DependencyProperty FillProperty;
        #endregion

        #region Constructors
        static WindowHeaderString()
        {
            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(WindowHeaderString));
            ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(WindowHeaderString));
            CloseButtonProperty = DependencyProperty.Register("CloseButton", typeof(Button), typeof(WindowHeaderString));
            FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(WindowHeaderString));
        }

        public WindowHeaderString()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties for Dependency Properties
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public Button CloseButton
        {
            get => (Button)GetValue(CloseButtonProperty);
            set => SetValue(CloseButtonProperty, value);
        }

        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }
        #endregion


        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void WindowHeaderString_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            HeaderString.StartPoint = new Point(0, ActualHeight);
            Line1.Point = new Point(ActualWidth - 10, 0);
            Arc1.Point = new Point(ActualWidth, 10);
            Line2.Point = new Point(ActualWidth, ActualHeight);
            Line3.Point = new Point(0, ActualHeight);
        }
    }
}
