using Courses.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _CourseService;

        public CoursesController(CourseService CourseService) =>
            _CourseService = CourseService;

        [Authorize]
        [HttpGet]
        public async Task<List<Courses.Models.Courses>> Get() =>
            await _CourseService.GetAsync();

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Courses.Models.Courses>> Get(string id)
        {
            var course = await _CourseService.GetAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            return course;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(Courses.Models.Courses newCourse)
        {
            await _CourseService.CreateAsync(newCourse);

            return CreatedAtAction(nameof(Get), new { id = newCourse.Id }, newCourse);
        }

        [Authorize(Roles = "Instructor,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Courses.Models.Courses updatedCourse)
        {
            var course = await _CourseService.GetAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            updatedCourse.Id = course.Id;

            await _CourseService.UpdateAsync(id, updatedCourse);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var course = await _CourseService.GetAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            await _CourseService.RemoveAsync(id);

            return NoContent();
        }

        [Authorize]
        [HttpGet("{Instructor}")]
        public async Task<List<Courses.Models.Courses>> GetCoursesByInstructor(string Instructor) =>
            await _CourseService.GetCourseByInstructorAsync(Instructor);
    }
}
