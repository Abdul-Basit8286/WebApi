using WebApiTask.Models;

namespace WebApiTask.Repositories
{
    public interface IPermissionRepo
    {
        Task<IEnumerable<Permission>> GetUsers();
        Task<Permission> GetUser(int Id);
        Task<Permission> DeleteUser(int Id);
        Task<Permission> UpdateUser(Permission permission);
        Task<Permission> AddUser(Permission permission);
    }
}
