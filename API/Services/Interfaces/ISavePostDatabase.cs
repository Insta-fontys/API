using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ISavePostDatabase
    {
        public Task<bool> SavePost(long fanId, long postId);
    }
}
