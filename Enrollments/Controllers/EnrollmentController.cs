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
        private readonly ILogger<EnrollmentController> _logger;

        public EnrollmentController(EnrollmentService EnrollmentService, ILogger<EnrollmentController> logger)
        {
            _EnrollmentService = EnrollmentService;
            _logger = logger;

        }

        [Authorize]
        [HttpGet]
        public async Task<List<Enrollment>> Get()
        {
            _logger.LogInformation("Get all enrollments method called");
            try
            {
                return await _EnrollmentService.GetAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get all enrollments method failed with exception : {ex}", ex.InnerException);
                return new List<Enrollment>();
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> Get(string id)
        {
            _logger.LogInformation("Get  enrollment method called for enrollmentID : {enrollmentid}", id);
            try {
                var enr = await _EnrollmentService.GetAsync(id);

                if (enr is null)
                {
                    return NotFound();
                }

                return enr;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get enrollment method failed with exception : {ex}", ex.InnerException);
                return NotFound();
            }
        }
        [Authorize(Roles = "Learner")]
        [HttpPost]
            public async Task<IActionResult> Post(Enrollment newEnrollment)
            {
            _logger.LogInformation("Create  enrollment method called for enrollmentID : {enrollment}", newEnrollment.EnrollmentID);
            try
            {
                await _EnrollmentService.CreateAsync(newEnrollment);

                return CreatedAtAction(nameof(Get), new { id = newEnrollment.Id }, newEnrollment);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Create  enrollment method failed with exception : {ex}", ex.InnerException);
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Enrollment updatedEnrollment)
        {
            _logger.LogInformation("update enrollment method called for enrollmentID : {enrollment}", updatedEnrollment.EnrollmentID);
            try {
                var enr = await _EnrollmentService.GetAsync(id);

                if (enr is null)
                {
                    return NotFound();
                }

                updatedEnrollment.Id = enr.Id;

                await _EnrollmentService.UpdateAsync(id, updatedEnrollment);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Update  enrollment method failed with exception : {ex}", ex.InnerException);
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Delete  enrollment method called for enrollmentID : {enrollmentID}", id);
            try {
                var enr = await _EnrollmentService.GetAsync(id);

                if (enr is null)
                {
                    return NotFound();
                }

                await _EnrollmentService.RemoveAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Delete enrollment method failed with exception : {ex}", ex.InnerException);
                return NotFound();
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{Learner}")]
        public async Task<List<Enrollment>> GetEnrollmentByLearner(string Learner)
        {
            _logger.LogInformation("GetEnrollmentByLearner method called for Learner : {Learner}", Learner);
            try
            {
                return await _EnrollmentService.GetEnrollmentByLearnerAsync(Learner);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("GetEnrollmentByLearner method failed with exception : {ex}", ex.InnerException);
                return new List<Enrollment>();
            }
        }
        }
    }

