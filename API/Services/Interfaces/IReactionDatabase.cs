using DataAccesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IReactionDatabase
    {
        public Task<bool> PostReaction(Reaction reaction, long postId, long fanId);
    }
}
