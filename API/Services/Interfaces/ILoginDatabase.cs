using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ILoginDatabase
    {
        public Task<bool> LoginFan(string username, string password);
        public Task<bool> LoginCreator(string username, string password);
    }
}
