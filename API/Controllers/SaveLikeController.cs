using API.Services;
using API.Services.Interfaces;
using DataAccesLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaveLikeController : ControllerBase
    {
        private readonly ILikePostsDatabase _database;
        private readonly UserService userService;

        public SaveLikeController (ILikePostsDatabase database, UserService userService)
        {
            _database = database;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> SaveLike(Post likedPosts)
        {
            var name = User.Claims.FirstOrDefault(i => i.Type == "Name").Value;
            Fan fan = await userService.GetFanByUsername(name);
            var result = await _database.PostILiked(likedPosts, fan.Id);
            if (result)
                return true;
            return false;
        }
    }
}
