using API.Services.Interfaces;
using DataAccesLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using DataAccesLibrary.Dto;

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
            var result = await _database.PostFanAccount(fan);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("admin")]
        public async Task<bool> PostAdminAccount(RegisterModel registerModel)
        {
            if (await _database.PostAdminAccount(registerModel))
                return true;

            return false;
        }
    }
}
