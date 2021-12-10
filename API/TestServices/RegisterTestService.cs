using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.TestServices
{
    public class RegisterTestService
    {
        private readonly DatabaseContext _context;

        public RegisterTestService(DatabaseContext context)
        {
            _context = context;
        }


        public async Task<bool> PostFanAccount(Fan fan)
        {
            var _fan = CreateFan(fan);
            try
            {
                _context.Fans.Add(_fan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PostCreatorAccount(Creator creator)
        {
            var _creator = CreateCreator(creator);
            try
            {
                _context.Creators.Add(_creator);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
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

    }
}
