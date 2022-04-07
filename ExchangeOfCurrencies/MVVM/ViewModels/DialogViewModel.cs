using ExchangeOfCurrencies.Core;

namespace ExchangeOfCurrencies.MVVM.ViewModels
{
    internal class DialogViewModel : BaseViewModel
    {
        private protected virtual void Close(ICloseable closeableView)
        {
            closeableView?.Close();
        }
    }
}
