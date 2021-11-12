using DataAccesLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IPostDatabase
    {
        public Task<bool> PostPost(Post post, long creatorId);
        public Task<List<Post>> GetPosts();
        public Task<bool> DeletePost(long id);
        public Task<bool> LikePost(long id);
        public Task<bool> RemoveLike(long id);
    }
}
