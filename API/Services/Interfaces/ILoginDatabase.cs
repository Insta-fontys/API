using DataAccesLibrary.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ILoginDatabase
    {
        public Task<bool> LoginFan(LoginModel loginModel);
        public Task<bool> LoginCreator(LoginModel loginModel);
        public string GetSalt(string username);
        string Authenticate(LoginModel loginModel);
    }
}
