using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinNavigation.ViewModels.Base;

namespace XamarinNavigation.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand GoHomeCommand { get; set; }
        public ICommand GoPage1Command { get; set; }

        public MenuPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoHomeCommand = new Command(GoHome);
            GoPage1Command = new Command(GoPage1);
        }

        private void GoHome(object obj)
        {
            _navigationService.NavigateTo(ViewModelLocator.MainView);
            App.MenuIsPresented = false;
        }

        private void GoPage1(object obj)
        {
            _navigationService.NavigateTo(ViewModelLocator.Page1View);
            App.MenuIsPresented = false;

        }
    }
}

