using API.Services;
using API.Services.Interfaces;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {

        private readonly IFollowDatabase _database;
        private readonly UserService userService;

        public FollowController(IFollowDatabase database, UserService userService)
        {
            this.userService = userService;
            _database = database;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostFollow(CreatorFans followModel)
        {
            var name = User.Claims.Where(i => i.Type == "Name").FirstOrDefault().Value;
            Fan fan = await userService.GetFanByUsername(name);
            followModel.FanId = fan.Id;
            var response = await _database.PostFollower(followModel);
            if (!response)
                return BadRequest();
            return Ok();
        }
    }
}
