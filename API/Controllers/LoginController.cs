using API.Services.Interfaces;
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

        public LoginController(ILoginDatabase database)
        {
            _database = database;
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
    }
}
