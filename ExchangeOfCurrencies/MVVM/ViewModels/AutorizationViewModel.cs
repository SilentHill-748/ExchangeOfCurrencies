using System;

using ExchangeOfCurrencies.Core;
using ExchangeOfCurrencies.Core.Commands;
using ExchangeOfCurrencies.MVVM.Views;
using ExchangeOfCurrencies.Logic.Services.Interfaces;
using ExchangeOfCurrencies.Logic.Services;

namespace ExchangeOfCurrencies.MVVM.ViewModels
{
    internal class AutorizationViewModel : ObservableObject
    {
        private string _login;
        private string _password;

        private readonly IAutorizationService _autorizationService;


        public AutorizationViewModel()
        {
            _login = string.Empty;
            _password = string.Empty;

            //TODO: Нужен единый объект, который может выдавать любой сервис (IServiceCollection, как пример)!
            _autorizationService = new AutorizationService(App.UnitOfWork);
        }


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



        public Command LogInCommand
            => LogInCommand ?? new Command(LogIn, CanLogIn);

        public ParameterlessCommand ForgetPasswordCommand
            => ForgetPasswordCommand ?? new ParameterlessCommand(ForgetPassword);

        private void LogIn(object? parameter)
        {
            if (parameter is null) 
                return;

            bool isAutorized = false;
            
            try
            {
                isAutorized = _autorizationService.Autorize(Login, Password);
            }
            catch (Exception ex)
            {
                //TODO: Написать нормальный логгер и объект вызова окна с сообщением!
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

        private static void Close(ICloseable closeableView)
        {
            closeableView?.Close();
        }
    }
}
