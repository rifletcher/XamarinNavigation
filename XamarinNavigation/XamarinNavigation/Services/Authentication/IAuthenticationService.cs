﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinNavigation.Models;

namespace XamarinNavigation.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }

        User AuthenticatedUser { get; }

        Task<bool> LoginAsync(string email, string password);

        Task<bool> LoginWithMicrosoftAsync();

        Task<bool> UserIsAuthenticatedAndValidAsync();

        Task LogoutAsync();
    }
}
