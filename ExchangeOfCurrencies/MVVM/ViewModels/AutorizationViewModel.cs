using System;
using System.Threading.Tasks;

using AsyncAwaitBestPractices.MVVM;

using ExchangeOfCurrencies.Core;
using ExchangeOfCurrencies.Core.Commands;
using ExchangeOfCurrencies.MVVM.Views;
using ExchangeOfCurrencies.Logic.Services.Interfaces;
using ExchangeOfCurrencies.Logic.Services;

namespace ExchangeOfCurrencies.MVVM.ViewModels
{
    internal class AutorizationViewModel : DialogViewModel
    {
        #region Private fields
        private string _login;
        private string _password;

        private readonly IAutorizationService _autorizationService;
        #endregion

        #region Constructors
        public AutorizationViewModel()
        {
            _login = string.Empty;
            _password = string.Empty;

            //TODO: Нужен единый объект, который может выдавать любой сервис (IServiceCollection, как пример)!
            _autorizationService = new AutorizationService(App.UnitOfWork);
        }
        #endregion

        #region Properties
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        #endregion

        #region Commands
        public AsyncCommand<object?> LogInCommand
            => LogInCommand ?? new AsyncCommand<object?>(LogIn, CanLogIn);

        public ParameterlessCommand ForgetPasswordCommand
            => ForgetPasswordCommand ?? new ParameterlessCommand(ForgetPassword);
        #endregion

        #region Private methods
        private async Task LogIn(object? parameter)
        {
            if (parameter is null) 
                return;

            bool isAutorized = false;
            
            try
            {
                isAutorized = await _autorizationService.AutorizeAsync(Login, Password);
            }
            catch (Exception ex)
            {
                //TODO:WARNING: Написать нормальный логгер и объект вызова окна с сообщением!
                MessageView messageView = new("Ошибка авторизации.", ex.Message, MessageViewButtons.Ok);
                messageView.ShowDialog();
            }
            finally
            {
                if (isAutorized)
                {
                    MainWindow mainWindow = new();
                    mainWindow.Show();

                    Close((ICloseable)parameter);
                }
            }
        }

        private void ForgetPassword()
        {
            string text = "Тут будет какой-то текст о том, как не хорошо забывать и терять пароли.";
            MessageView message = new("Информация", text, MessageViewButtons.Ok);
            message.ShowDialog();
        }

        private bool CanLogIn(object? parameter)
        {
            return Login.Length > 0 && Password.Length > 0;
        }
        #endregion
    }
}
