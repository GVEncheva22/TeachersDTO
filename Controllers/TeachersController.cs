using Microsoft.AspNetCore.Mvc;
using TeacherManagementApi.DTOs;
using TeacherManagementApi.Services;

namespace TeacherManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll()
        {
            var teachers = await _teacherService.GetAllAsync();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetById(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null) return NotFound(new { message = "Учителят не е намерен." });
            return Ok(teacher);
            
                }

                [HttpPost]
            public async Task<ActionResult<TeacherDto>> Create([FromBody] CreateTeacherDto dto)
            {
                var created = await _teacherService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] UpdateTeacherDto dto)
            {
                var success = await _teacherService.UpdateAsync(id, dto);
                if (!success) return NotFound(new { message = "Учителят не е намерен за редакция." });
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var success = await _teacherService.DeleteAsync(id);
                if (!success) return NotFound(new { message = "Учителят не е намерен за изтриване." });
                return NoContent();
            }
        }
    }