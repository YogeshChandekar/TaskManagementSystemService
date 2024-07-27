using TaskManagementService.Models;
using TaskManagementService.Models.LoginManagement;
using TaskManagementService.Models.UserManagement;

namespace TaskManagementService.Interface
{
    public interface ILogin
    {
        Task<UsersInfo> Authentication(AuthenticateUser authenticateUser);
    }
}
