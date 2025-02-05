using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserTasksAPI.Models;
using UserTasksAPI.Repositories;

namespace UserTasksAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() => Ok(await _userRepository.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _userRepository.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.ID }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return NotFound();

            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            await _userRepository.Update(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.Delete(id);
            return NoContent();
        }
    }
}
