using System;
using System.Collections.Generic;
using System.Text;
using XamarinNavigation.Services.Authentication;
using XamarinNavigation.Validations;
using XamarinNavigation.ViewModels.Base;
using Xamarin.Forms;
using System.Windows.Input;
using XamarinNavigation.Utils;
using System.Threading.Tasks;
using XamarinNavigation.Views;

namespace XamarinNavigation.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly GalaSoft.MvvmLight.Views.INavigationService _navigationService;
        private readonly Dialog.IDialogService _dialogService;

        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;

        public LoginViewModel(
            IAuthenticationService authenticationService, GalaSoft.MvvmLight.Views.INavigationService navigationService, Dialog.IDialogService dialogService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _dialogService = dialogService;

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
                    IsBusy = false;
                    _navigationService.NavigateTo(ViewModelLocator.RootView);

                }
            }
            catch (ServiceAuthenticationException)
            {
                await _dialogService.ShowAlertAsync("Please, try again", "Login error", "Ok");
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync("An error occurred, try again", "Error", "Ok");
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