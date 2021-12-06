using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IFollowDatabase
    {
        public Task<bool> PostFollower(CreatorFans model);
        public Task<List<CreatorFans>> GetFollowers(long creatorId);
        public Task<List<CreatorFans>> GetFollowings(long fanId);
        public Task<bool> DeleteFollower(CreatorFans model);
    }
}
