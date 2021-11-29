using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IFollowDatabase
    {
        public Task<bool> PostFollower(CreatorFans model);

    }
}
