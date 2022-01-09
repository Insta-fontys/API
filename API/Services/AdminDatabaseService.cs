using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class AdminDatabaseService
    {
        private readonly UserManager<IdentityUser> userManager;

        public async Task<bool> DeleteFan(string name)
        {
            IdentityUser user = await userManager.FindByNameAsync(name);
            try
            {
                await userManager.DeleteAsync(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
