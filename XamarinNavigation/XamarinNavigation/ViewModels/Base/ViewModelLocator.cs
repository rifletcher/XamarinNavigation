using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinNavigation.ViewModels
{
    public static class ViewModelLocator
    {
        public const string MainView = "MainView";
        public const string LoginView = "LoginView";
        public const string Page1View = "Page1View";
        public const string RootView = "RootView";

        public static void LoadViewModelLocators()
        {
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<Page1ViewModel>();
        }
    }
}