using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApiTask.Models;

namespace WebApiTask.Repositories
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> Search(string searchString);
        Task<IEnumerable<User>> GetUsers(PageList pageList);
        Task<User> GetUser(int Id);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(int Id);

    }
}
