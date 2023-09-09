using Microsoft.EntityFrameworkCore;
using WebApiTask.ModelContext;
using WebApiTask.Models;

namespace WebApiTask.Repositories
{
    public class PermissionRepo : IPermissionRepo
    {
        private readonly ApplicationDbContext _context;
        public PermissionRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Permission> AddUser(Permission permission)
        {
            var result= await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<Permission> DeleteUser(int Id)
        {
            var result= await _context.Permissions.FirstOrDefaultAsync(a => a.Id == Id);
            if(result!=null)
            {
                _context.Permissions.Remove(result);
                await _context.SaveChangesAsync();
                return result; 
            }
            return null;
        }

        public async Task<Permission> GetUser(int Id)
        {
            return await _context.Permissions.FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<IEnumerable<Permission>> GetUsers()
        {
           return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> UpdateUser(Permission permission)
        {
            var result=await _context.Permissions.FirstOrDefaultAsync(x => x.Id == permission.Id);
            if (result != null)
            {
                result.ControllerName=permission.ControllerName;
                return result;
            }
            return null;
        }
    }
}
