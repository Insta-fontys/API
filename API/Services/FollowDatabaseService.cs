using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class FollowDatabaseService : IFollowDatabase
    {
        private readonly DatabaseContext _context;

        public FollowDatabaseService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<CreatorFans>> GetFollowers(long creatorId)
        {
            var list = await _context.CreatorFans.Where(i => i.CreatorId == creatorId).Include(i => i.Fan).ToListAsync();
            return list;
        }

        public async Task<List<CreatorFans>> GetFollowings(long fanId)
        {
            var list = await _context.CreatorFans.Where(i => i.FanId == fanId).Include(i => i.Creator).ToListAsync();
            return list;
        }

        public async Task<bool> PostFollower(CreatorFans model)
        {
            if (CheckIfAlreadyFollowed(model) != null)
                return false;
            try
            {
                _context.CreatorFans.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteFollower(CreatorFans model)
        {
            try
            {
                _context.CreatorFans.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private CreatorFans CheckIfAlreadyFollowed(CreatorFans model)
        {
            return _context.CreatorFans.Where(i => i.FanId == model.FanId && i.CreatorId == model.CreatorId).FirstOrDefault();
        }
    }
}
