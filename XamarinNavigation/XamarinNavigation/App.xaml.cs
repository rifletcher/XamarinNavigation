using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinNavigation
{
    public partial class App : Application
    {
        public static NavigationPage Navigation { get; set; }
        public static RootPage RootPage;
        public static bool MenuIsPresented
        {
            get => RootPage.IsPresented;
            set => RootPage.IsPresented = value;
        }

        public App()
        {
            InitializeComponent();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            ViewModelLocator.LoadViewModelLocators();

            var nav = new Services.NavigationService();

            nav.Configure(ViewModelLocator.MainView, typeof(MainPage));
            nav.Configure(ViewModelLocator.LoginView, typeof(LoginView));
            nav.Configure(ViewModelLocator.Page1View, typeof(Page1View));
            nav.Configure(ViewModelLocator.RootView, typeof(RootPage));
            // todo register services
            SimpleIoc.Default.Register<INavigationService>(() => nav);
            var authentication = new AuthenticationService();
            SimpleIoc.Default.Register<IAuthenticationService>(() => authentication);
            //var menuPage = new MenuPage();
            Navigation = new NavigationPage(new MainPage());

            //RootPage = new RootPage
            //{
            //    Master = menuPage,
            //    Detail = Navigation
            //};
            //MainPage = RootPage;

            nav.Initialize(Navigation);
            nav.NavigateTo(ViewModelLocator.LoginView);
        }

        protected override void OnStart()
        {
            //MobileCenter.Start($"ios={AppSettings.MobileCenterAnalyticsIos};uwp={AppSettings.MobileCenterAnalyticsWindows};android={AppSettings.MobileCenterAnalyticsAndroid}", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
