using API.Services.Interfaces;
using DataAccesLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionDatabase _database;

        public ReactionController(IReactionDatabase database)
        {
            _database = database;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostPost([FromBody] Reaction reaction, long postId, long fanId)
        {
            var result = await _database.PostReaction(reaction, postId, fanId);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
