using API.Services.Interfaces;
using DataAccesLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using System.Security.Claims;
using System.IO;
using System.Net.Http.Headers;

namespace API.Controllers
{
    [Authorize(Roles ="creator")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostDatabase _database;
        private readonly UserService userService;

        public PostController(IPostDatabase database, UserService userService)
        {
            this.userService = userService;
            _database = database;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostPost([FromBody] Post post)
        {

            var name = User.Claims.Where(i => i.Type == "Name").FirstOrDefault().Value;
            var creator = await userService.GetCreatorByUserName(name);
            var result = await _database.PostPost(post, creator.Id);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            Post post = new Post();

            try
            {
                var file = Request.Form.Files[0];
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FontysGram")))
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FontysGram"));

                var pathToSave = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FontysGram");
                if (file.Length > 0)
                {
                    var fileName = User.Claims.Where(i => i.Type == "Name").FirstOrDefault().Value;
                    var extention = file.ContentType;
                    extention = extention.Replace("image/", ".");
                    fileName = fileName + extention;
                    var fullPath = Path.Combine(pathToSave, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var name = User.Claims.Where(i => i.Type == "Name").FirstOrDefault().Value;
                    var creator = await userService.GetCreatorByUserName(name);

                    post.Image = fullPath;
                    post.Description = "test";
                    var result = await _database.PostPost(post, creator.Id);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult<bool>> DeletePost(long id)
        {
            var result = await _database.DeletePost(id);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("id")]
        public async Task<ActionResult<bool>> LikePost(long id)
        {
            var result = await _database.LikePost(id);

            if (!result)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("removeLike/id")]
        public async Task<ActionResult<bool>> RemoveLike(long id)
        {
            var result = await _database.RemoveLike(id);
            if (!result)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
