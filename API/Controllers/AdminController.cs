using API.Services.Interfaces;
using API_Admin.Services;
using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminDatabase _database;

        public AdminController(IAdminDatabase database)
        {
            _database = database;
        }

        [HttpGet("fans")]
        public async Task<ActionResult<IList<Fan>>> GetFans()
        {
            var fans = await _database.GetFans();
            return Ok(fans);
        }

        [HttpGet("creators")]
        public async Task<ActionResult<IList<Creator>>> GetCreators()
        {
            var creators = await _database.GetCreators();
            return Ok(creators);
        }

        [HttpGet("posts")]
        public async Task<ActionResult<IList<Post>>> GetPosts()
        {
            var posts = await _database.GetPosts();
            return Ok(posts);
        }

        [HttpDelete("creator/{username}")]
        public async Task<bool> DeleteCreator([FromRoute] string username)
        {
            if (await _database.DeleteCreatorByName(username))
                return true;
            return false;
        }

        [HttpDelete("fan/{username}")]
        public async Task<bool> DeleteFan([FromRoute]string username)
        {
            if (await _database.DeleteFanByName(username))
                return true;
            return false;
        }

        [HttpDelete("post/{id}")]
        public async Task<bool> DeletePost([FromRoute]long id)
        {
            if (await _database.DeletePostById(id))
                return true;
            return false;
        }
    }
}
