using API.Services.Interfaces;
using DataAccesLibrary.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class LoginController : ControllerBase
    {
        private readonly ILoginDatabase _database;

        public LoginController(ILoginDatabase database)
        {
            _database = database;
        }

        [AllowAnonymous]
        [HttpGet("fan")]
        public async Task<ActionResult<bool>> LoginFan(LoginModel loginModel)
        {
            var result = await _database.LoginFan(loginModel);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("creator")]
        public async Task<ActionResult<bool>> LoginCreator(LoginModel loginModel)
        {
            var result = await _database.LoginCreator(loginModel);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel loginModel)
        {
            var token = _database.Authenticate(loginModel);
            if (token == null)
                return BadRequest();
            return Ok(token);
        }
    }
}
