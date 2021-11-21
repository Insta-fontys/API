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
    public class PostDatabaseService : IPostDatabase
    {
        private readonly DatabaseContext _context;

        public PostDatabaseService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetPosts()
        {
            var list = await _context.Posts.ToListAsync();
            return list;
        }

        public async Task<bool> PostPost(Post post, long creatorId)
        {
            var _post = CreatePostWithCorrectReferences(post, creatorId);
            try
            {
                _context.Posts.Add(_post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DeletePost(long id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> LikePost(Post post)
        {
            try
            {
                var _post = await _context.Posts.FindAsync(post.Id);

                _post.Likes += 1;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }

        public async Task<bool> RemoveLike(long id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);

                post.Likes -= 1;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private Post CreatePostWithCorrectReferences(Post post, long creatorId)
        {
            post.Reactions = new List<Reaction>();

            post.CreatorId = creatorId;
            post.Creator = null;

            return post;
        }
    }
}
