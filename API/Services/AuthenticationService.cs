using API.Security;
using DataAccesLibrary.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class AuthenticationService
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        private async Task<bool> EmailExists(string email)
        {
            var result = await userManager.FindByEmailAsync(email);
            if (result != null)
                return false;
            return true;
        }

        private async Task<bool> IsPasswordCorrect(LoginModel loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            var result = await signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            return result.Succeeded;
        }

        public async Task<string> Authenticate(LoginModel loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

            if (result.Succeeded)
                return JwtAuthenticationManager.GenerateJwtToken();
            return null;
        }
    }
}
