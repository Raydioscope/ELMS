using Courses.Services;
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

        [HttpGet]
        public async Task<List<Courses.Models.Courses>> Get() =>
            await _CourseService.GetAsync();

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

        [HttpPost]
        public async Task<IActionResult> Post(Courses.Models.Courses newCourse)
        {
            await _CourseService.CreateAsync(newCourse);

            return CreatedAtAction(nameof(Get), new { id = newCourse.Id }, newCourse);
        }

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

        [HttpGet("{course}")]
        public async Task<List<Courses.Models.Courses>> GetCoursesByInstructor(string inst) =>
            await _CourseService.GetCourseByInstructorAsync(inst);
    }
}
