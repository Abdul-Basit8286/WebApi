using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTask.Models;
using WebApiTask.Repositories;

namespace WebApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepo _userRoleRepo;
        public UserRoleController(IUserRoleRepo userRoleRepo) 
        {
            _userRoleRepo = userRoleRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await _userRoleRepo.GetUsers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error in retriving data from database");
            }
            
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<UserRole>> GetUser(int Id)
        {
            try
            {
                var result = await _userRoleRepo.GetUser(Id);
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
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<UserRole>> DeleteUser(int Id)
        {
            try
            {
                var result = await _userRoleRepo.GetUser(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return await _userRoleRepo.DeleteUser(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in retriving data from database");
            }
           
        } 
        [HttpPut("{Id:int}")]
        public async Task<ActionResult<UserRole>> UpdateUser(int Id,UserRole userRole)
        {
            try
            {
                if (Id != userRole.Id)
                {
                    return BadRequest("Id MissMatch");
                }

                var result = await _userRoleRepo.GetUser(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return await _userRoleRepo.UpdateUser(userRole);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in retriving data from database");
            }
           
        }
        [HttpPost]
        public async Task<ActionResult<UserRole>> PostUser(UserRole userRole)
        {
            try
            {
                if (userRole == null)
                {
                    return BadRequest();
                }

                var result = await _userRoleRepo.AddUser(userRole);
                return CreatedAtAction(nameof(GetUser),new { id =result.Id}, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error in retriving data from database");
            }
           
        }

    }
}
