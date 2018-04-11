using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinNavigation.ViewModels
{
    public class Page1ViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public Page1ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
