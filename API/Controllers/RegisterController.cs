using API.Services.Interfaces;
using DataAccesLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterDatabase _database;

        public RegisterController(IRegisterDatabase database)
        {
            _database = database;
        }

        [HttpPost("creator")]
        public async Task<ActionResult<bool>> PostCreatorAccount([FromBody] Creator creator)
        {
            var result = await _database.PostCreatorAccount(creator);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("fan")]
        public async Task<ActionResult<bool>> PostFanAccount([FromBody] Fan fan)
        {
            var user = new IdentityUser
            {
                Email = fan.Email.Trim(),
                UserName = fan.Username.Trim()
            };

            var result = await _database.PostFanAccount(fan);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
