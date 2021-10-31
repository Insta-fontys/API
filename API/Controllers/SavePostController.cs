using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavePostController : ControllerBase
    {
        private readonly ISavePostDatabase _database;

        public SavePostController(ISavePostDatabase database)
        {
            _database = database;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> SavePost(long fanId, long postId)
        {
            var result = await _database.SavePost(fanId, postId);
            return result;
        }
    }
}
