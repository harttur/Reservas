using Microsoft.AspNetCore.Mvc;
using Reservas.Dtos;
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
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return users;
        }

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
        public async Task<ActionResult> CreateUser(UserDto userDto)
        { 
            await _userService.CreateUserAsync(userDto); 
            return CreatedAtAction(nameof(GetUserById),new { id_user = userDto.Id_user}, userDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(string id_user, UserDto userdto)
        {
            await _userService.UpdateUserAsync(id_user, userdto);
            return NoContent(); 

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(string id_user)
        { 
            await _userService.DeleteUserAsync(id_user);
            return NoContent();
        }
    }
}
