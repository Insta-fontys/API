using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class LoginDatabaseService : ILoginDatabase
    {
        private readonly DatabaseContext _context;

        public LoginDatabaseService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> LoginFan(string username, string password)
        {
            var result = _context.Fans.Where(x => x.Username.Equals(username) && x.Password.Equals(password)).FirstOrDefault();

            if (result == null)
                return false;
            return true;
        }

        public async Task<bool> LoginCreator(string username, string password)
        {
            var result = _context.Creators.Where(x => x.Username.Equals(username) && x.Password.Equals(password)).FirstOrDefault();

            if (result == null)
                return false;
            return true;
        }
    }
}
