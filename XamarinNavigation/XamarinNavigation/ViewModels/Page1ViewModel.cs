using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinNavigation.ViewModels.Base;

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
