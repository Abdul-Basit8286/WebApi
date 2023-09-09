using Microsoft.AspNetCore.Mvc;
using WebApiTask.Models;
using WebApiTask.Repositories;


namespace WebApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PageList pageList,string searchString)
        {
            try
            {
                var result = await _userRepository.Search(searchString);
                if (result!=null)
                {
                    return Ok(result);
                }
                return Ok(await _userRepository.GetUsers(pageList));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error in retriving data from database");
            }
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<User>> GetUser(int Id)
        {
            try
            {
                var result = await _userRepository.GetUser(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error in retriving data from database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                var createdUser = await _userRepository.AddUser(user);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error in retriving data from database");
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int Id, User user)
        {
            try
            {
                if (Id != user.Id)
                {
                    return BadRequest("id MisMatch");
                }
                var UserUpdate = await _userRepository.GetUser(Id);
                if (UserUpdate == null)
                {
                    return NotFound($"User Id={Id} Not Found");
                }
                return await _userRepository.UpdateUser(user);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error in retriving data from database");
            }
        }
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int Id)
        {
            try
            {

                var UserDelete = await _userRepository.GetUser(Id);
                if (UserDelete == null)
                {
                    return NotFound($"User Id={Id} Not Found");
                }
                return await _userRepository.DeleteUser(Id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error in retriving data from database");
            }
        }

     
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<User>>> Search(string searchString)
        {
            try
            {
                var result = await _userRepository.Search(searchString);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error in retriving data from database");
            }
        }
    }
}
