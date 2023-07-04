using ELMS.Models;
using ELMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELMS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<List<Users>> Get()
        {
            _logger.LogInformation("Get all users method called");
            try
            {
                return await _userService.GetAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get all users method failed with exception : {ex}",ex.InnerException);
                return new List<Users>();
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> Get(string id)
        {
            _logger.LogInformation("Get  user method called for userid : {userid}",id);
            try
            {
                var user = await _userService.GetAsync(id);

                if (user is null)
                {
                    _logger.LogInformation("Get  user method failed for userid : {userid}", id);
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get  user method failed with exception : {ex}", ex.InnerException);
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(Users newUser)
        {
            _logger.LogInformation("Create  user method called for userid : {userid}", newUser.UserID);
            try
            {
                await _userService.CreateAsync(newUser);

                return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Create  user method failed with exception : {ex}", ex.InnerException);
                return BadRequest();
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Users updatedUser)
        {
            _logger.LogInformation("Update  user method called for userid : {userid}", updatedUser.UserID);
            try
            {
                var user = await _userService.GetAsync(id);

                if (user is null)
                {
                    return NotFound();
                }

                updatedUser.Id = user.Id;

                await _userService.UpdateAsync(id, updatedUser);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Update  user method failed with exception : {ex}", ex.InnerException);
                return NoContent();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Delete  user method called for userid : {userid}", id);
            try
            {
                var user = await _userService.GetAsync(id);

                if (user is null)
                {
                    return NotFound();
                }

                await _userService.RemoveAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Delete  user method failed with exception : {ex}", ex.InnerException);
                return NoContent();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{role}")]
        public async Task<List<Users>> GetUsersByRole(string role)
        {
            _logger.LogInformation("GetUsersByRole method called for role : {role}", role);
            try
            {
                return await _userService.GetByRoleAsync(role);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("GetUsersByRole method failed with exception : {ex}", ex.InnerException);
                return new List<Users>();
            }
        }
    }
}
