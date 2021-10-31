using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;
using API.Security.Interface;
using API.Security;

namespace API.Services
{
    public class RegisterDatabaseService : IRegisterDatabase
    {
        private readonly DatabaseContext _context;
        private EncryptManager encryptManager = new EncryptManager();

        public RegisterDatabaseService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> PostFanAccount(Fan fan)
        {
            if (CheckFanUsername(fan.Username))
                return false;

            var _fan = CreateFan(fan);
            _fan.Salt = encryptManager.CreateSaltString();
            _fan.Password = encryptManager.Hash(_fan.Salt, _fan.Password);

            try
            {
                _context.Fans.Add(_fan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> PostCreatorAccount(Creator creator)
        {
            if (CheckCreatorUsername(creator.Username))
                return false;

            var _creator = CreateCreator(creator);
            try
            {
                _context.Creators.Add(_creator);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private Fan CreateFan(Fan fan)
        {
            fan.CreatorFans = new List<CreatorFans>();
            fan.LikedPosts = new List<LikedPosts>();
            fan.SavedPosts = new List<SavedPosts>();
            fan.Reactions = new List<Reaction>();

            return fan;
        }

        private Creator CreateCreator(Creator creator)
        {
            creator.BannedFans = new List<Fan>();
            creator.Followers = new List<CreatorFans>();
            creator.Posts = new List<Post>();

            return creator;
        }

        private bool CheckFanUsername(string username)
        {
            return _context.Fans.Any(i => i.Username.Equals(username));
        }

        private bool CheckCreatorUsername(string username)
        {
            return _context.Creators.Any(i => i.Username.Equals(username));
        }
    }
}
