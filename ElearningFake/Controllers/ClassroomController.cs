using ElearningFake.Contracts;
using ElearningFake.Data;
using ElearningFake.DTOs;
using ElearningFake.Model;
using ElearningFake.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ElearningFake.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroom _classroomContracts;

        public ClassroomController(IClassroom? classroomRepository)
        {
            _classroomContracts = classroomRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _classroomContracts!.GetClassroomByIdAsync(id));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ClassroomDTO model)
        {
            if (model.Name.IsNullOrEmpty() ||
                model.Description.IsNullOrEmpty() ||
                model.Type.IsNullOrEmpty())
            {
                return BadRequest(ModelState);
            }

            return Ok(await _classroomContracts!.AddClassroomAsync(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ClassroomModel model)
        {
            await _classroomContracts.UpdateClassroomAsync(model);
            return NoContent();
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _classroomContracts!.GetClassroomAsync());
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _classroomContracts!.DeleteClassroomAsync(id);
            return NoContent();
        }
        [HttpPost("join")]
        public async Task<IActionResult> Join(int id)
        {
            return Ok(await _classroomContracts.JoinCLassroomAsync(id));
        }

        [HttpPost("exit")]
        public async Task<IActionResult> Exit(int id)
        {
            return Ok(await _classroomContracts.ExitCLassroomAsync(id));
        }
        [HttpPost("Member")]
        public async Task<IActionResult> Member(int id)
        {
            return Ok(await _classroomContracts.GetUserInClassroomAsync(id));
        }
    }
}
