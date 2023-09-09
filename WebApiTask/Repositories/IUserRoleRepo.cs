using WebApiTask.Models;

namespace WebApiTask.Repositories
{
    public interface IUserRoleRepo
    {
        Task<IEnumerable<UserRole>> GetUsers();
        Task<UserRole> GetUser(int Id);
        Task<UserRole> DeleteUser(int Id);
        Task<UserRole> AddUser(UserRole userRole);
        Task<UserRole> UpdateUser(UserRole userRole);
        
    }
}
