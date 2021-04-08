using System.Windows;
using System.Windows.Input;

namespace ExchangeOfCurrencies.UI.Windows.MessageWindows
{
    public partial class Message : Window
    { 
        public Message(string message, string header)
        {
            InitializeComponent();
            Init(message, header);
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Init(string message, string header)
        {
            OkLabel.MouseDown += CloseBox_MouseLeftButtonDown;
            HeaderLabel.MouseLeftButtonDown += Header_MouseLeftButtonDown;
            HeaderLabel.Content = header;
            MessageBlock.Text = message;
        }
    }
}
