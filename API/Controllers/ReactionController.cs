using API.Services;
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
        private readonly UserService userService;

        public ReactionController(IReactionDatabase database, UserService userService)
        {
            this.userService = userService;
            _database = database;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostPost([FromBody] Reaction reaction, long postId, long fanId)
        {
            var name = User.Claims.Where(i => i.Type == "Name").FirstOrDefault().Value;
            var creator = await userService.GetFanByUsername(name);
            var result = await _database.PostReaction(reaction, postId, creator.Id);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
