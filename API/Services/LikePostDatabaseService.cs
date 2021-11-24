using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class LikePostDatabaseService: ILikePostsDatabase
    {
        private readonly DatabaseContext _context;

        public LikePostDatabaseService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> PostILiked(Post post, long fanId)
        {
            if (AlreadyLiked(post.Id, fanId))
                return false;

            LikedPosts likedPosts = new LikedPosts()
            {
                PostId = post.Id,
                FanId = fanId
            };
            try
            {
                _context.LikedPosts.Add(likedPosts);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool AlreadyLiked(long postId, long fanId)
        {
            var response =  _context.LikedPosts.Where(i => i.PostId == postId && i.FanId == fanId).FirstOrDefault();
            if (response != null)
                return true;
            return false;
        }
    }
}
