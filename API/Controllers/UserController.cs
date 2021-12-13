using API.Services;
using DataAccesLibrary.Dto;
using DataAccesLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<Creator>> GetCreator(UserRequestModel userRequestModel)
        {
            return await userService.GetCreatorByUserName(userRequestModel.Username);
        }
    }
}
