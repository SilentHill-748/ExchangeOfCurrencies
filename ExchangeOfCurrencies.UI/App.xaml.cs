using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ExchangeOfCurrencies.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            Color peekColor = (Color)ColorConverter.ConvertFromString("#EABA3E");
            if (sender is Label lab)
                lab.Foreground = new SolidColorBrush(peekColor);
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Label lab)
                lab.Foreground = Brushes.White;
        }
    }
}
