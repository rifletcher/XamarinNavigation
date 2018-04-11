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

//        private IContainer _container;
//        private ContainerBuilder _containerBuilder;

//        private static readonly Locator _instance = new Locator();

//        public static Locator Instance
//        {
//            get
//            {
//                return _instance;
//            }
//        }

//        public Locator()
//        {
//            _containerBuilder = new ContainerBuilder();

//            _containerBuilder.RegisterType<DialogService>().As<IDialogService>();
//            _containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
//            _containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>();

//            //if (AppSettings.UseFakes)
//            //{
//            //    _containerBuilder.RegisterType<FakeBookingService>().As<IBookingService>();
//            //    _containerBuilder.RegisterType<FakeHotelService>().As<IHotelService>();
//            //    _containerBuilder.RegisterType<FakeNotificationService>().As<INotificationService>();
//            //    _containerBuilder.RegisterType<FakeSuggestionService>().As<ISuggestionService>();
//            //}
//            //else
//            //{
//            //    _containerBuilder.RegisterType<BookingService>().As<IBookingService>();
//            //    _containerBuilder.RegisterType<HotelService>().As<IHotelService>();
//            //    _containerBuilder.RegisterType<NotificationService>().As<INotificationService>();
//            //    _containerBuilder.RegisterType<SuggestionService>().As<ISuggestionService>();
//            //}
//            _containerBuilder.RegisterType<LoginViewModel>();
//            _containerBuilder.RegisterType<MainPageViewModel>();
//            _containerBuilder.RegisterType<Page1ViewModel>();
//            _containerBuilder.RegisterType<RootPageViewModel>();
//            _containerBuilder.RegisterType<ExtendedSplashViewModel>();
//        }

//        public T Resolve<T>()
//        {
//            return _container.Resolve<T>();
//        }

//        public object Resolve(Type type)
//        {
//            return _container.Resolve(type);
//        }

//        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
//        {
//            _containerBuilder.RegisterType<TImplementation>().As<TInterface>();
//        }

//        public void Register<T>() where T : class
//        {
//            _containerBuilder.RegisterType<T>();
//        }

//        public void Build()
//        {
//            _container = _containerBuilder.Build();
//        }
//    }
//}