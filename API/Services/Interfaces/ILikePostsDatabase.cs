using DataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ILikePostsDatabase
    {
        public Task<bool> PostILiked(Post post, long fanId);
    }
}
