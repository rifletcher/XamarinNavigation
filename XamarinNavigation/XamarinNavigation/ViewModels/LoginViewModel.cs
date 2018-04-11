﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinNavigation.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;

        public LoginViewModel(
            IAuthenticationService authenticationService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
        }

        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignInCommand => new AsyncCommand(SignInAsync);

        public ICommand MicrosoftSignInCommand => new AsyncCommand(MicrosoftSignInAsync);

        public ICommand SettingsCommand => new AsyncCommand(NavigateToSettingsAsync);

        private async Task SignInAsync()
        {
            IsBusy = true;

            bool isValid = Validate();

            if (isValid)
            {
                bool isAuth = await _authenticationService.LoginAsync(UserName.Value, Password.Value);

                if (isAuth)
                {
                    IsBusy = false;
                    var menuPage = new MenuPage();
                    App.Navigation = new NavigationPage(new MainPage());

                    App.RootPage = new RootPage
                    {
                        Master = menuPage,
                        Detail = App.Navigation
                    };
                    Application.Current.MainPage = App.RootPage;

                    _navigationService.NavigateTo(ViewModelLocator.RootView);
                }
            }

            IsBusy = false;
        }

        private async Task MicrosoftSignInAsync()
        {
            try
            {
                IsBusy = true;

                bool succeeded = await _authenticationService.LoginWithMicrosoftAsync();

                if (succeeded)
                {
                    // todo
                    //_navigationService.NavigateTo(ViewModelLocator.RootView);
                    IsBusy = false;
                    //var menuPage = new MenuPage();
                    ////App.Navigation = new NavigationPage(new MainPage());

                    //App.RootPage = new RootPage
                    //{
                    //    Master = menuPage,
                    //    Detail = App.Navigation
                    //};
                    //Application.Current.MainPage = App.RootPage;

                    _navigationService.NavigateTo(ViewModelLocator.RootView);

                }
            }
            catch (ServiceAuthenticationException)
            {
                // todo
                //await DialogService.ShowAlertAsync("Please, try again", "Login error", "Ok");
            }
            catch (Exception ex)
            {
                // todo
                //await DialogService.ShowAlertAsync("An error occurred, try again", "Error", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username should not be empty" });
            _userName.Validations.Add(new EmailRule());
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password should not be empty" });
        }

        private bool Validate()
        {
            bool isValidUser = _userName.Validate();
            bool isValidPassword = _password.Validate();

            return isValidUser && isValidPassword;
        }

        private Task NavigateToSettingsAsync(object obj)
        {
            // todo
            return null;
            //return NavigationService.NavigateToAsync(typeof(SettingsViewModel<RemoteSettings>));
        }
    }
}