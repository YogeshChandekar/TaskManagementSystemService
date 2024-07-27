using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TaskManagementService.Interface;
using TaskManagementService.Models;
using TaskManagementService.Models.UserManagement;
using TaskManagementService.Queries;

namespace TaskManagementService.Services
{
    public class UserManagementService : IUserManagement
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserManagementService));
        private readonly ICommanDbHander _commanDbHander;

        public UserManagementService(ICommanDbHander commanDbHander)
        {
            _commanDbHander = commanDbHander;
        }
        public Task<List<UsersInfo>> UserList()
        {
            return _commanDbHander.GetData<UsersInfo>(UserDBQueries._getUserList, log, "User List successfully retrieved", "An error occurred while retrieving user list", ErrorCodes.UserList, ConstantData.Txn);
        }

        public async Task<BaseResponse> AddUser(AddUser addUser)
        {
            BaseResponse baseResponse = new BaseResponse();
            baseResponse = await _commanDbHander.AddUpdateDeleteData(
                UserDBQueries._addUser,
                log,
                $"The {addUser.Email} user has been successfully created and is pending approval.",
                $"An error occurred while adding {addUser.Email} user details.",
                $"The {addUser.Email} User email already been used.",
                ErrorCodes.AddUser,
                ConstantData.Txn,
                new
                {
                    @name = addUser.Name,
                    @email = addUser.Email,
                    @contact_no = addUser.Phone,
                    @is_admin = addUser.IsAdmin,
                    @approved = addUser.Approved,
                    @status = addUser.Status,
                    @attempt_count = addUser.Attemptcount,
                    @is_locked = addUser.Islocked,
                    @password = addUser.Password
                }).ConfigureAwait(false);
            return baseResponse;
        }

        public Task<UsersInfo> SingleUser(string email)
        {
            return _commanDbHander.GetSingleData<UsersInfo>(UserDBQueries._getSingleUser, log,
                $"The {email} user details successfully retrieved",
                $"An error occurred while retrieving {email} user details",
                ErrorCodes.GetSingleUserDetails, ConstantData.Txn, new { @email = email });
        }

        public async Task<BaseResponse> UpdateUser(UpdateUser updateUser)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = await _commanDbHander.AddUpdateDeleteData(
                               UserDBQueries._updateUser,
                               log,
                               $"The {updateUser.Email} user has been successfully updated.",
                               $"An error occurred while updating {updateUser.Email} user details.",
                               $"The {updateUser.Email} email already been used.",
                               ErrorCodes.UpdateUser,
                               ConstantData.Txn,
                               new
                               {
                                   @cuser_name = updateUser.Name,
                                   @email = updateUser.Email,
                                   @contact_no = updateUser.ContactNo,
                                   @is_admin = updateUser.IsAdmin,
                               }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex, ErrorCodes.UpdateUser, ConstantData.Txn);
            }
            return baseResponse;
        }

        public async Task<BaseResponse> DeleteUser(ApproveUser approveUser)
        {
            BaseResponse? res = await _commanDbHander.AddUpdateDeleteData(
              UserDBQueries._deleteUser,
              log,
              $"The {approveUser.Username} user successfully deleted by {approveUser.ApprovedBy}",
              $"An error occurred while deleting {approveUser.Username} user",
              "",
              ErrorCodes.DeleteUser,
              ConstantData.Txn,
              new
              {
                  @email = approveUser.Username,
                  @active = 0,
              }).ConfigureAwait(false);
            return res;
        }

        public Task<BaseResponse> UnlockUser(ApproveUser approveUser)
        {
            return _commanDbHander.AddUpdateDeleteData(
               UserDBQueries._unlockUser,
               log,
               $"The {approveUser.Username} user successfully unblock by {approveUser.ApprovedBy}",
               $"An error occurred while unblocking {approveUser.Username} user",
               "",
               ErrorCodes.UnblockUser,
               ConstantData.Txn,
               new
               {
                   @email = approveUser.Username,
                   @attemptcount = 0,
                   @islocked = 1,
               });
        }

        public Task<BaseResponse> ApproveUser(ApproveUser approveUser)
        {
            // need to check valid user then approve = 0
            return _commanDbHander.AddUpdateDeleteData(
                UserDBQueries._setApprovalStatusUser,
                log,
                $"The {approveUser.Username} User successfully approved by {approveUser.ApprovedBy}",
                $"An error occurred while approving {approveUser.Username} user",
                "",
                ErrorCodes.ApproveUser,
                ConstantData.Txn,
                new
                {
                    @email = approveUser.Username,
                    @approved = 1,
                });
        }

        public async Task<BaseResponse> ReactivateUser(ApproveUser approveUser)
        {
            // need to check valid user then approve = 0
            BaseResponse baseResponse = await _commanDbHander.AddUpdateDeleteData(
               UserDBQueries._reactivateUser,
               log,
               $"The {approveUser.Username} user successfully reactivate by {approveUser.ApprovedBy}",
               $"An error occurred while reactivating {approveUser.Username} user",
               "",
               ErrorCodes.ReactiveUser,
               ConstantData.Txn,
               new
               {
                   @email = approveUser.Username,
                   @active = 1,
               }).ConfigureAwait(false);
            return baseResponse;
        }

        public async Task<string> GetPassword(string? email, bool isAuthentication)
        {
            string res = await _commanDbHander.GetSingleData<string>(UserDBQueries._getUserPassword, log,
                  $"The {email} user Password successfully retrieved",
                $"An error occurred while getting {email} user Password",
                  ErrorCodes.GetUserPassword, ConstantData.Txn, new { email }).ConfigureAwait(false);
            return res;
        }

    }
}
