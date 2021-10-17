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
    public class PostController : ControllerBase
    {
        private readonly IPostDatabase _database;

        public PostController(IPostDatabase database)
        {
            _database = database;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostPost([FromBody] Post post, long creatorId)
        {
            var result = await _database.PostPost(post, creatorId);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<bool>> DeletePost(long id)
        {
            var result = await _database.DeletePost(id);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("id")]
        public async Task<ActionResult<bool>> LikePost(long id)
        {
            var result = await _database.LikePost(id);

            if (!result)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("removeLike/id")]
        public async Task<ActionResult<bool>> RemoveLike(long id)
        {
            var result = await _database.RemoveLike(id);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
