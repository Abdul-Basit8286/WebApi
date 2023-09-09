
using Microsoft.EntityFrameworkCore;
using WebApiTask.ModelContext;
using WebApiTask.Models;

namespace WebApiTask.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddUser(User user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> DeleteUser(int Id)
        {
            var result = await _context.Users.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (result != null)
            {
                _context.Users.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<User> GetUser(int Id)
        {

            return await _context.Users.Where(a => a.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsers(PageList pageList)
        {

            return await _context.Users
                .Skip((pageList.PageNumber - 1) * pageList.pageSize)
                .Take(pageList.pageSize)
                .ToListAsync();
        }


        public async Task<IEnumerable<User>> Search(string searchString)
        {
            var result = from x in _context.Users select x;
            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.Name.Contains(searchString) || x.Email.Contains(searchString));
            }
            return await result.AsNoTracking().ToListAsync();
        }

        async Task<User> IUserRepository.UpdateUser(User user)
        {
            var result = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == user.Id);
            if (result != null)
            {
                result.Name = user.Name;
                result.Email = user.Email;
                result.City = user.City;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }


    }
}
