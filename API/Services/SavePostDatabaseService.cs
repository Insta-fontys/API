using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class SavePostDatabaseService : ISavePostDatabase
    {
        private readonly DatabaseContext _context;

        public SavePostDatabaseService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> SavePost(long fanId, long postId)
        {
            if (AlreadySaved(fanId, postId))
                return false;
            var savedPost = CreateNewSavedPost(fanId, postId);

            try
            {
                _context.SavedPosts.Add(savedPost);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private SavedPosts CreateNewSavedPost(long fanId, long postId)
        {
            SavedPosts savedPosts = new SavedPosts()
            {
                FanId = fanId,
                PostId = postId
            };
            return savedPosts;
        }

        private bool AlreadySaved(long fanId, long postId)
        {
            return _context.SavedPosts.Any(i => i.FanId == fanId && i.PostId == postId);
        }
    }
}
