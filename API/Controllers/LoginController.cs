using API.Services;
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
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginDatabase _database;
        private readonly AuthenticationService authenticationService;

        public LoginController(ILoginDatabase database, AuthenticationService authenticationService)
        {
            _database = database;
            this.authenticationService = authenticationService;
        }

        [HttpGet("fan")]
        public async Task<ActionResult<bool>> LoginFan(string username, string password)
        {
            var result = await _database.LoginFan(username, password);
            return result;
        }

        [HttpGet("creator")]
        public async Task<ActionResult<bool>> LoginCreator(string username, string password)
        {
            var result = await _database.LoginCreator(username, password);
            return result;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<string> Authenticate([FromBody] LoginModel loginModel)
        {
            var result = await authenticationService.Authenticate(loginModel);
            return result;
        }
    }
}
