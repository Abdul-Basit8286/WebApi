using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using WebApiTask.Models;
using WebApiTask.Repositories;


namespace WebApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepo _permissionRepo;
        public PermissionController(IPermissionRepo permissionRepo)
        {
            _permissionRepo= permissionRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await _permissionRepo.GetUsers());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in retrieving data from database");
            }
            
        }
        [HttpGet("{Id:int}")]
       public async Task<ActionResult<Permission>> GetUser(int Id)
        {
            try
            {
                var result = await _permissionRepo.GetUser(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error in retrieving data from database");
            }
          
        }
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Permission>> DeleteUser(int Id)
        {
            var result = await _permissionRepo.GetUser(Id);
            if(result == null)
            {
                return NotFound();
            }
           return await _permissionRepo.DeleteUser(Id);
        }
        [HttpPost]
        public async Task<ActionResult<Permission>> PostUser(Permission permission)
        {
            try
            {
                if (permission == null)
                {
                    return BadRequest();
                }
                var result = await _permissionRepo.AddUser(permission);
                return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "error in retrieving data from database");
            }
            

        }
        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Permission>> UpdateUser(int Id,Permission permission)
        {
            try
            {
                if (Id != permission.Id)
                {
                    return BadRequest("Id MisMatch");
                }
                var result = await _permissionRepo.GetUser(Id);
                if(result == null)
                {
                    return NotFound();
                }
                return await _permissionRepo.UpdateUser(permission);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in retrieving data from database");
            }
        }
    }
}
