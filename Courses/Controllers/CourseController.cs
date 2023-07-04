using Courses.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Courses.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _CourseService;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(CourseService CourseService,ILogger<CoursesController> logger)
        {
            _CourseService = CourseService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<Courses.Models.Courses>> Get()
        {
            _logger.LogInformation("Get all Courses method called");
            try
            {
                return await _CourseService.GetAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get all courses method failed with exception : {ex}", ex.InnerException);
                return new List<Courses.Models.Courses>();
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Courses.Models.Courses>> Get(string id)
        {
            _logger.LogInformation("Get  course method called for courseid : {courseid}", id);
            try {
                var course = await _CourseService.GetAsync(id);

                if (course is null)
                {
                    return NotFound();
                }

                return course;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get course method failed with exception : {ex}", ex.InnerException);
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(Courses.Models.Courses newCourse)
        {
            _logger.LogInformation("Create  course method called for courseid : {courseid}", newCourse.CourseID);
            try
            {
                await _CourseService.CreateAsync(newCourse);

                return CreatedAtAction(nameof(Get), new { id = newCourse.Id }, newCourse);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Create  course method failed with exception : {ex}", ex.InnerException);
                return BadRequest();
            }
        }

        [Authorize(Roles = "Instructor,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Courses.Models.Courses updatedCourse)
        {
            _logger.LogInformation("Update  course method called for courseid : {courseid}", updatedCourse.CourseID);
            try
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
            catch (Exception ex)
            {
                _logger.LogInformation("Update  course method failed with exception : {ex}", ex.InnerException);
                return NoContent();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Delete  course method called for courseID : {courseID}", id);
            try
            {
                var course = await _CourseService.GetAsync(id);

                if (course is null)
                {
                    return NotFound();
                }

                await _CourseService.RemoveAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Delete course method failed with exception : {ex}", ex.InnerException);
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("{Instructor}")]
        public async Task<List<Courses.Models.Courses>> GetCoursesByInstructor(string Instructor)
        {
            _logger.LogInformation("GetCoursesByInstructor method called for Instructor : {Instructor}", Instructor);
            try
            {
                return await _CourseService.GetCourseByInstructorAsync(Instructor);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("GetCoursesByInstructor method failed with exception : {ex}", ex.InnerException);
                return new List<Courses.Models.Courses>();
            }
        }
    }
}
