using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
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

        public async Task<bool> PostFollower(CreatorFans model)
        {
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
    }
}
