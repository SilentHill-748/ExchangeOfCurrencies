using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ExchangeOfCurrencies.Services
{
    public class NavigationService
    {
        #region NavigationService Singleton
        private static readonly object _locker = new();
        private static NavigationService? _instance;

        public static NavigationService Instance
        {
            get
            {
                if (_instance is null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                            _instance = new NavigationService();
                    }
                }

                return _instance;
            }
        }
        #endregion


        private readonly Dictionary<Type, Page> _navigations;


        public NavigationService()
        {
            _navigations = new Dictionary<Type, Page>();
        }

        public Page NavigateTo(Type pageType)
        {
            if (!_navigations.ContainsKey(pageType))
            {
                Page page = (Page?)Activator.CreateInstance(pageType) ?? 
                    throw new ArgumentException("Argument was not page type.");

                _navigations.Add(pageType, page);
            }

            return _navigations[pageType];
        }
    }
}
