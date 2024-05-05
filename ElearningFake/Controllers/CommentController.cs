using ElearningFake.Contracts;
using ElearningFake.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ElearningFake.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CommentController : ControllerBase
    {
        private readonly IComment _comment;

        public CommentController(IComment comment)
        {
            _comment = comment;
        }
        [HttpPost]
        public async Task<ActionResult> Post(CommentDTO commentDTO,int id)
        {
            if (commentDTO.Description.IsNullOrEmpty())
            {
                return BadRequest(ModelState);
            }
            return Ok(await _comment.PostAsync(commentDTO,id));
        }
        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _comment.GetAsync(id));
        }
        
    }
}
