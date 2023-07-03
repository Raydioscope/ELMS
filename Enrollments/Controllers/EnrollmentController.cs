using Enrollments.Models;
using Enrollments.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enrollments.Controllers
{
    
        [ApiController]
        [Route("api/[controller]/[action]")]
        public class EnrollmentController : ControllerBase
        {
            private readonly EnrollmentService _EnrollmentService;

            public EnrollmentController(EnrollmentService EnrollmentService) =>
                _EnrollmentService = EnrollmentService;

        [Authorize]
            [HttpGet]
            public async Task<List<Enrollment>> Get() =>
                await _EnrollmentService.GetAsync();

        [Authorize]
        [HttpGet("{id}")]
            public async Task<ActionResult<Enrollment>> Get(string id)
            {
                var enr = await _EnrollmentService.GetAsync(id);

                if (enr is null)
                {
                    return NotFound();
                }

                return enr;
            }
        [Authorize(Roles = "Learner")]
        [HttpPost]
            public async Task<IActionResult> Post(Enrollment newEnrollment)
            {
                await _EnrollmentService.CreateAsync(newEnrollment);

                return CreatedAtAction(nameof(Get), new { id = newEnrollment.Id }, newEnrollment);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(string id, Enrollment updatedUser)
            {
                var enr = await _EnrollmentService.GetAsync(id);

                if (enr is null)
                {
                    return NotFound();
                }

                updatedUser.Id = enr.Id;

                await _EnrollmentService.UpdateAsync(id, updatedUser);

                return NoContent();
            }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(string id)
            {
                var enr = await _EnrollmentService.GetAsync(id);

                if (enr is null)
                {
                    return NotFound();
                }

                await _EnrollmentService.RemoveAsync(id);

                return NoContent();
            }
        [Authorize(Roles = "Admin")]
        [HttpGet("{Learner}")]
            public async Task<List<Enrollment>> GetEnrollmentByLearner(string Learner) =>
                await _EnrollmentService.GetEnrollmentByLearnerAsync(Learner);
        }
    }

