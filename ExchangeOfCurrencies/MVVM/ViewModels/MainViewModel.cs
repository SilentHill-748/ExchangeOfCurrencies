using System;
using System.Windows.Controls;

using ExchangeOfCurrencies.Pages;
using ExchangeOfCurrencies.Services;
using ExchangeOfCurrencies.Core.Commands;

namespace ExchangeOfCurrencies.MVVM.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private Page _page;


        public MainViewModel()
        {
            _navigationService = NavigationService.Instance;

            _navigateToCommand = new Command(OnNavigateTo);
            _page = new AccountInfo();
        }


        public Page Page
        {
            get => _page;
            set
            {
                _page = value;
                OnPropertyChanged(nameof(Page));
            }
        }


        #region Commands
        private Command _navigateToCommand;
        public Command NavigateToCommand
            => _navigateToCommand;
        #endregion


        private void OnNavigateTo(object? commandParameter)
        {
            if (commandParameter is null)
            {
                return;
            }
            else
                Page = _navigationService.NavigateTo((Type)commandParameter);
        }
    }
}
