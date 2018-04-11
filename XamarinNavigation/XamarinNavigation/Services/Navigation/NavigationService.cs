using GalaSoft.MvvmLight.Views;
using XamarinNavigation.Services.Authentication;
using XamarinNavigation.ViewModels;
using XamarinNavigation.ViewModels.Base;
using XamarinNavigation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinNavigation.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private NavigationPage _navigation;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (_navigation.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = _navigation.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Key
                        : null;
                }
            }
        }

        public void GoBack()
        {
            _navigation.PopAsync();
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    var type = _pagesByKey[pageKey];
                    ConstructorInfo constructor = null;
                    object[] parameters = null;

                    if (parameter == null)
                    {
                        constructor = type.GetTypeInfo()
                            .DeclaredConstructors
                            .FirstOrDefault(c => !c.GetParameters().Any());

                        parameters = new object[]
                        {
                        };
                    }
                    else
                    {
                        constructor = type.GetTypeInfo()
                            .DeclaredConstructors
                            .FirstOrDefault(
                                c =>
                                {
                                    var p = c.GetParameters();
                                    return p.Count() == 1
                                           && p[0].ParameterType == parameter.GetType();
                                });

                        parameters = new[]
                        {
                            parameter
                        };
                    }

                    if (constructor == null)
                    {
                        throw new InvalidOperationException(
                            "No suitable constructor found for page " + pageKey);
                    }

                    var page = constructor.Invoke(parameters) as Page;

                    if (page is LoginView)
                    {
                        CurrentApplication.MainPage = page;
                    }
                    else
                    {
                        if (CurrentApplication.MainPage is LoginView)
                        {
                            var menuPage = new MenuPage();

                            (page as RootPage).Master = menuPage;
                            (page as RootPage).Detail = App.Navigation;
                            App.RootPage = (page as RootPage);
                            Application.Current.MainPage = App.RootPage;
                        }
                        else
                        {
                            _navigation.PushAsync(page);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such page: {0}. Did you forget to call NavigationService.Configure?",
                            pageKey),
                        "pageKey");
                }
            }
        }

        public void Configure(string pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        public void Initialize(NavigationPage navigation)
        {
            _navigation = navigation;
        }
    }
}
//    private readonly IAuthenticationService _authenticationService;
//    protected readonly Dictionary<Type, Type> _mappings;

//    protected Application CurrentApplication
//    {
//        get { return Application.Current; }
//    }

//    public NavigationService(IAuthenticationService authenticationService)
//    {
//        _authenticationService = authenticationService;
//        _mappings = new Dictionary<Type, Type>();

//        CreatePageViewModelMappings();
//    }

//    public async Task InitializeAsync()
//    {
//        if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
//        {
//            await NavigateToAsync<MainPageViewModel>();
//        }
//        else
//        {
//            await NavigateToAsync<LoginViewModel>();
//        }
//    }

//    public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
//    {
//        return InternalNavigateToAsync(typeof(TViewModel), null);
//    }

//    public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
//    {
//        return InternalNavigateToAsync(typeof(TViewModel), parameter);
//    }

//    public Task NavigateToAsync(Type viewModelType)
//    {
//        return InternalNavigateToAsync(viewModelType, null);
//    }

//    public Task NavigateToAsync(Type viewModelType, object parameter)
//    {
//        return InternalNavigateToAsync(viewModelType, parameter);
//    }

//    public async Task NavigateBackAsync()
//    {
//        if (CurrentApplication.MainPage is RootPage)
//        {
//            var mainPage = CurrentApplication.MainPage as RootPage;
//            await mainPage.Detail.Navigation.PopAsync();
//        }
//        else if (CurrentApplication.MainPage != null)
//        {
//            await CurrentApplication.MainPage.Navigation.PopAsync();
//        }
//    }

//    public virtual Task RemoveLastFromBackStackAsync()
//    {
//        var mainPage = CurrentApplication.MainPage as RootPage;

//        if (mainPage != null)
//        {
//            mainPage.Detail.Navigation.RemovePage(
//                mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
//        }

//        return Task.FromResult(true);
//    }

//    protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
//    {
//        Page page = CreateAndBindPage(viewModelType, parameter);

//        if (page is RootPage)
//        {
//            CurrentApplication.MainPage = page;
//        }
//        else if (page is LoginView)
//        {
//            CurrentApplication.MainPage = new CustomNavigationPage(page);
//        }
//        else if (CurrentApplication.MainPage is RootPage)
//        {
//            var mainPage = CurrentApplication.MainPage as RootPage;
//            var navigationPage = mainPage.Detail as CustomNavigationPage;

//            if (navigationPage != null)
//            {
//                var currentPage = navigationPage.CurrentPage;

//                if (currentPage.GetType() != page.GetType())
//                {
//                    await navigationPage.PushAsync(page);
//                }
//            }
//            else
//            {
//                navigationPage = new CustomNavigationPage(page);
//                mainPage.Detail = navigationPage;
//            }

//            mainPage.IsPresented = false;
//        }
//        else
//        {
//            var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

//            if (navigationPage != null)
//            {
//                await navigationPage.PushAsync(page);
//            }
//            else
//            {
//                CurrentApplication.MainPage = new CustomNavigationPage(page);
//            }
//        }

//        await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
//    }

//    protected Type GetPageTypeForViewModel(Type viewModelType)
//    {
//        if (!_mappings.ContainsKey(viewModelType))
//        {
//            throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
//        }

//        return _mappings[viewModelType];
//    }

//    protected Page CreateAndBindPage(Type viewModelType, object parameter)
//    {
//        Type pageType = GetPageTypeForViewModel(viewModelType);

//        if (pageType == null)
//        {
//            throw new Exception($"Mapping type for {viewModelType} is not a page");
//        }

//        Page page = Activator.CreateInstance(pageType) as Page;
//        ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
//        page.BindingContext = viewModel;

//        if (page is RootPage)
//        {
//            var rootPage = (page as RootPage);
//            rootPage.Master.BindingContext = viewModel;
//        }

//        return page;
//    }

//    private void CreatePageViewModelMappings()
//    {
//        _mappings.Add(typeof(LoginViewModel), typeof(LoginView));
//        _mappings.Add(typeof(MainPageViewModel), typeof(MainPage));            
//        _mappings.Add(typeof(RootPageViewModel), typeof(RootPage));
//        _mappings.Add(typeof(Page1ViewModel), typeof(Page1View));
//        _mappings.Add(typeof(ExtendedSplashViewModel), typeof(ExtendedSplashView));

//        //if (Device.Idiom == TargetIdiom.Desktop)
//        //{
//        //    _mappings.Add(typeof(HomeViewModel), typeof(UwpHomeView));
//        //    _mappings.Add(typeof(SuggestionsViewModel), typeof(UwpSuggestionsView));
//        //}
//        //else
//        //{
//        //    _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
//        //    _mappings.Add(typeof(SuggestionsViewModel), typeof(SuggestionsView));
//        //}
//    }
//}
