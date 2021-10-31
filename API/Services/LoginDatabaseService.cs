using API.Security;
using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class LoginDatabaseService : ILoginDatabase
    {
        private readonly DatabaseContext _context;
        private EncryptManager encryptManager = new EncryptManager();
        private readonly string key = "This is the key for encrypting this thing";

        public LoginDatabaseService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> LoginFan(LoginModel loginModel)
        {
            var salt = GetSalt(loginModel.Username);
            var password = encryptManager.Hash(salt, loginModel.Password);
            var result = _context.Fans.Where(x => x.Username.Equals(loginModel.Username) && x.Password.Equals(password)).FirstOrDefault();

            if (result == null)
                return false;
            return true;
        }

        public async Task<bool> LoginCreator(LoginModel loginModel)
        {
            var salt = GetSalt(loginModel.Username);
            var password = encryptManager.Hash(salt, loginModel.Password);
            var result = _context.Creators.Where(x => x.Username.Equals(loginModel.Username) && x.Password.Equals(password)).FirstOrDefault();

            if (result == null)
                return false;
            return true;
        }

        public string GetSalt(string username)
        {
            Fan result =_context.Fans.Where(i => i.Username == username).FirstOrDefault();
            return result.Salt;
        }

        public string Authenticate(LoginModel loginModel)
        {
            if (!_context.Fans.Any(f => f.Username == loginModel.Username && f.Password == loginModel.Password))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, loginModel.Username)
                }),
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
