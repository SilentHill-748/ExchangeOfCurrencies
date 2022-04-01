using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using ExchangeOfCurrencies.Core;

namespace ExchangeOfCurrencies.MVVM.Views
{
    public partial class MessageView : Window
    {
        private MessageViewButtons _messageViewButtons;

        public MessageView()
        {
            InitializeComponent();
        }

        public MessageView(string title, string message, MessageViewButtons buttons) 
            : this()
        {
            Title = title;
            Message = message;

            MessageViewButtonsChanged += MessageView_MessageViewButtonsChanged;
            Buttons = buttons;

        }


        public event Action? MessageViewButtonsChanged;


        public string Message { get; set; }

        public MessageViewButtons Buttons
        {
            get => _messageViewButtons;
            set
            {
                _messageViewButtons = value;
                MessageViewButtonsChanged?.Invoke();
            }
        }


        private void MessageView_MessageViewButtonsChanged()
        {
            switch (Buttons)
            {
                case MessageViewButtons.Ok:
                    ChangeButton(BtnOne, "Ok");
                    break;
                case MessageViewButtons.Cancel:
                    ChangeButton(BtnOne, "Cancel");
                    break;
                case MessageViewButtons.OkCancel:
                    ChangeButton(BtnTwo, "Отмена");
                    break;
                default:
                    ChangeButton(BtnOne, "Да");
                    ChangeButton(BtnTwo, "Нет");
                    break;
            }
        }

        private static void ChangeButton(Button button, string content, Visibility value = Visibility.Visible)
        {
            button.Content = content;
            button.Visibility = value;
        }

        private void BtnOne_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void BtnTwo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
