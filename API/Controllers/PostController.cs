using API.Services;
using API.Services.Interfaces;
using DataAccesLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostDatabase _database;
        private readonly UserService userService;

        public PostController(IPostDatabase database, UserService userService)
        {
            this.userService = userService;
            _database = database;
        }

        [HttpGet]
        public async Task<List<Post>> GetPosts()
        {
            var list = await _database.GetPosts();
            return list;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostPost([FromBody] Post post)
        {

            var name = User.Claims.Where(i => i.Type == "Name").FirstOrDefault().Value;
            var creator = await userService.GetCreatorByUserName(name);
            var result = await _database.PostPost(post, creator);
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
        public async Task<ActionResult<bool>> LikePost(Post post)
        {
            var result = await _database.LikePost(post);

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
