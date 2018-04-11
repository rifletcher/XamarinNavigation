using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinNavigation.Models;

namespace XamarinNavigation.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private bool _isAuthenticated { get; set; }

        public bool IsAuthenticated => _isAuthenticated;

        public User AuthenticatedUser => throw new NotImplementedException();

        public Task<bool> LoginAsync(string email, string password)
        {
            var user = new Models.User
            {
                Email = email,
                Name = email,
                LastName = string.Empty,
                Token = email
            };
            return Task.FromResult(true);

        }

        public Task<bool> LoginWithMicrosoftAsync()
        {
            return Task.FromResult(true);
        }

        public Task LogoutAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> UserIsAuthenticatedAndValidAsync()
        {
            if (_isAuthenticated) return Task.FromResult(true);
            _isAuthenticated = true;
            return Task.FromResult(false);
        }
    }
}
