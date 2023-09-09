using Microsoft.EntityFrameworkCore;
using WebApiTask.ModelContext;
using WebApiTask.Models;

namespace WebApiTask.Repositories
{
    public class UserRoleRepo : IUserRoleRepo
    {
        private readonly ApplicationDbContext  _context;
        public UserRoleRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserRole> AddUser(UserRole userRole)
        {
            var result= await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserRole> DeleteUser(int Id)
        {
            var result=  await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == Id);
            if(result != null)
            {
                _context.UserRoles.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<UserRole> GetUser(int Id)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<UserRole>> GetUsers()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole> UpdateUser(UserRole userRole)
        {
            var result= await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == userRole.Id);
            if(result !=null)
            {
                result.Writer = userRole.Writer;
                result.Creater = userRole.Creater;
                result.Admin = userRole.Admin;
                result.Approver = userRole.Approver;
                result.Editor = userRole.Editor;
                result.Manager = userRole.Manager;
                result.SuperAdmin = userRole.SuperAdmin;
                return result;
            }
            return null;
        } 
    }
}
