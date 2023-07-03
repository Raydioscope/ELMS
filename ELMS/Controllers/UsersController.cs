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

        public UsersController(UserService userService) =>
            _userService = userService;
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<List<Users>> Get() =>
            await _userService.GetAsync();

        [Authorize(Roles ="Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> Get(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(Users newUser)
        {
            await _userService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Users updatedUser)
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(id);

            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{role}")]
        public async Task<List<Users>> GetUsersByRole(string role) =>
            await _userService.GetByRoleAsync(role);
    }
}
