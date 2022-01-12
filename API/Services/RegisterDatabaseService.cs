using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DataAccesLibrary.Dto;

namespace API.Services
{
    public class RegisterDatabaseService : IRegisterDatabase
    {
        private readonly DatabaseContext _context;
        private readonly IdentityRegistrationService identityRegistrationService;

        public RegisterDatabaseService(DatabaseContext context, IdentityRegistrationService identityRegistrationService)
        {
            _context = context;
            this.identityRegistrationService = identityRegistrationService;
        }

        public async Task<bool> PostFanAccount(Fan fan)
        {
            var _fan = CreateFan(fan);

            var user = new IdentityUser
            {
                Email = fan.Email.Trim(),
                UserName = fan.Username.Trim()
            };

            if (!await identityRegistrationService.DoesEmailExist(fan.Email))
                return false;
            if (await identityRegistrationService.CreateIdentityUser(user, "fan"))
            {
                _context.Fans.Add(_fan);
                await _context.SaveChangesAsync();
            }
            return true;

        }

        public async Task<bool> PostCreatorAccount(Creator creator)
        {
            var _creator = CreateCreator(creator);

            var user = new IdentityUser
            {
                Email = creator.Email.Trim(),
                UserName = creator.Username.Trim()
            };

            if (!await identityRegistrationService.DoesEmailExist(creator.Email))
                return false;
            if (await identityRegistrationService.CreateIdentityUser(user, "creator"))
            {
                _context.Creators.Add(_creator);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> PostAdminAccount(RegisterModel registerModel)
        {
            var user = new IdentityUser
            {
                Email = registerModel.Email.Trim(),
                UserName = registerModel.Username.Trim()
            };
            if (!await identityRegistrationService.DoesEmailExist(registerModel.Email))
                return false;
            if (await identityRegistrationService.CreateIdentityUser(user, "admin"))
                return true;
            return false;
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

    }
}
