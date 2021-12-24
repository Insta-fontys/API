using API.Services;
using API.Services.Interfaces;
using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenDatabase _database;
        private readonly UserService userService;

        public TokenController(ITokenDatabase database, UserService userService)
        {
            _database = database;
            this.userService = userService;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> PostTokens([FromBody] BuyTokensModel buyTokensModel)
        {
            var name = User.Claims.FirstOrDefault(i => i.Type == "Name").Value;
            Fan fan = await userService.GetFanByUsername(name);
            buyTokensModel.FanId = fan.Id;
            var result = await _database.BuyTokens(buyTokensModel);
            if (result)
                return true;
            return false;
        }

        [HttpPut("donate")]
        public async Task<ActionResult<bool>> DonateTokens(DonateTokensModel donateTokensModel)
        {
            var name = User.Claims.FirstOrDefault(i => i.Type == "Name").Value;
            Fan fan = await userService.GetFanByUsername(name);
            donateTokensModel.FanId = fan.Id;
            var result = await _database.DonateTokens(donateTokensModel);
            if (result)
                return true;
            return false;
        }
    }
}
