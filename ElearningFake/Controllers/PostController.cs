using ElearningFake.Contracts;
using ElearningFake.Data;
using ElearningFake.DTOs;
using ElearningFake.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ElearningFake.Controllers
{
    [Route("api/classroom/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class PostController : ControllerBase
    {
        private readonly IPost _postContract;

        public PostController(IPost postContract)
        {
            _postContract = postContract;
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postContract.GetPostAsync());
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postContract.DeletePostAsync(id);
            return NoContent();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPost(PostDTO model,int classroomId)
        {
            if (model.Description.IsNullOrEmpty())
            {
                return BadRequest(ModelState);
            }

            return Ok(await _postContract.AddPostAsync(model,classroomId));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePost(PostModel model)
        {
            await _postContract.UpdatePostAsync(model);
            return NoContent();
        }
    }
}
