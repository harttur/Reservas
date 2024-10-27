using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservas.Dtos;
using Reservas.Models;
using Reservas.Services.Contract;
using ZstdSharp.Unsafe;

namespace Reservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return users;
        }

        [Authorize]
        [HttpGet("{id_user}")]
        public async Task<ActionResult<UserDto>> GetUserById(string id_user)
        { 
            var user = await _userService.GetUserByIdAsync(id_user);

            if(user == null)
            {
                return NotFound();    
            }

            return user; 
        }
 
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserDto userDto)
        {

            User user = await _userService.CreateUserAsync(userDto); 

            return CreatedAtAction(nameof(GetUserById),new { id_user = user.Id_user}, userDto);
        }

        [Authorize]
        [HttpPut("{id_user}")]
        public async Task<ActionResult> UpdateUser(string id_user, UserDto userDto)
        {
            await _userService.UpdateUserAsync(id_user, userDto);
            return NoContent(); 

        }
        [Authorize]
        [HttpDelete("{id_user}")]
        public async Task<ActionResult> DeleteUser(string id_user)
        { 
            await _userService.DeleteUserAsync(id_user);
            return NoContent();
        }
    }
}
