using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ReactionDatabaseService : IReactionDatabase
    {
        private readonly DatabaseContext _context;

        public ReactionDatabaseService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> PostReaction(Reaction reaction, long postId, long fanId)
        {
            var _reaction = CreateReactionWithCorrectReferences(reaction, postId, fanId);
            try
            {
                _context.Reactions.Add(_reaction);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private Reaction CreateReactionWithCorrectReferences(Reaction reaction, long postId, long fanId)
        {
            reaction.FanId = fanId;
            reaction.PostId = postId;
            reaction.Post = null;
            reaction.Fan = null;

            return reaction;
        }
    }
}
