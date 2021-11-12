using Microsoft.AspNetCore.Identity;
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
            var password = "!erQWE2qweqweS";
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                return true;
            }
            return false;
        }
    }
}
