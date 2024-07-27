using TaskManagementService.Models;
using TaskManagementService.Models.UserManagement;

namespace TaskManagementService.Interface
{
    public interface IUserManagement
    {
        Task<List<UsersInfo>> UserList();

        Task<BaseResponse> AddUser(AddUser addUser);

        Task<UsersInfo> SingleUser(string email);

        Task<BaseResponse> UpdateUser(UpdateUser updateUser);

        Task<BaseResponse> DeleteUser(ApproveUser approveUser);

        Task<BaseResponse> UnlockUser(ApproveUser approveUser);

        Task<BaseResponse> ApproveUser(ApproveUser approveUser);

        Task<BaseResponse> ReactivateUser(ApproveUser approveUser);

        Task<string> GetPassword(string? email, bool isAuthentication);
    }
}