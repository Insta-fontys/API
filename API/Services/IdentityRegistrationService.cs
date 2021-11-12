using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class IdentityRegistrationService
    {
        private readonly UserManager<IdentityUser> userManager;


        public IdentityRegistrationService(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> DoesEmailExist(string email)
        {
            if (await userManager.FindByEmailAsync(email) != null)
                return false;
            return true;
        }

        public async Task<bool> CreateIdentityUser(IdentityUser user, string role)
        {
            // TODO Generate random password here
            var password = "!erQWE2qweqweS";
            //var password = RandomPasswordGenerator(13);

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                return true;
            }
            return false;
        }

        private string RandomPasswordGenerator(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*(";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
