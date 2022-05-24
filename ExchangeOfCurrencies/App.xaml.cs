using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using ExchangeOfCurrencies.Data;
using ExchangeOfCurrencies.Data.Interfaces;

namespace ExchangeOfCurrencies
{
    public partial class App : Application
    {
        public static IUnitOfWork<AppDbContext> UnitOfWork { get; }


        static App()
        {
            var dbContextFactory = new AppDbContextFactory();

            UnitOfWork = new UnitOfWork<AppDbContext>(dbContextFactory.CreateDbContext());
        }


        private void WindowBorder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is DependencyObject d)
                Window.GetWindow(d)?.DragMove();
        }
    }
}
