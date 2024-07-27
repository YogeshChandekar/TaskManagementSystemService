using Microsoft.AspNetCore.Mvc;
using System.Text;
using TaskManagementService.Interface;
using TaskManagementService.Models;
using TaskManagementService.Models.LoginManagement;
using TaskManagementService.Models.UserManagement;

namespace TaskManagementService.Controllers
{
    public class UserManagementController : ControllerBase
    {

        private readonly IUserManagement _userManagement;

        public UserManagementController(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        [Route("GetUserList")]
        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            return Ok(await _userManagement.UserList());
        }

        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUser addUser)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(addUser.Password);
            addUser.Password = Convert.ToBase64String(plainTextBytes);

            BaseResponse addUserRes = await _userManagement.AddUser(addUser);
            return Ok(addUserRes);
        }

        [Route("GetUserDetails/{username}")]
        [HttpGet]
        public async Task<IActionResult> GetUserDetails(string username)
        {
            var userDetail = await _userManagement.SingleUser(username);
            if (userDetail == null)
                throw new CustomException($"User {username} details not found", null, ErrorCodes.GetSingleUserDetails, ConstantData.Txn);
            return Ok(userDetail);
        }

        [Route("UpdateUser")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser updateUser)
        {
            var userDTO = await _userManagement.SingleUser(updateUser.Email);
            if (userDTO == null)
                throw new CustomException($"User {updateUser.Email} is not registered", null, ErrorCodes.IsUserRegistered, ConstantData.Txn);
            BaseResponse updateUserRes = await _userManagement.UpdateUser(updateUser);
            return Ok(updateUserRes);
        }

        [Route("DeleteUser")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromBody] ApproveUser approveUser)
        {
            var userDTO = await _userManagement.SingleUser(approveUser.Username);
            if (userDTO == null)
                throw new CustomException($"User {approveUser.Username} is not registered", null, ErrorCodes.IsUserRegistered, ConstantData.Txn);
            if (userDTO.Status == 0)
                throw new CustomException($"User {approveUser.Username} already deleted", null, ErrorCodes.UserAlreadyDeleted, ConstantData.Txn);
            return Ok(await _userManagement.DeleteUser(approveUser));
        }

        [Route("UnblockUser")]
        [HttpPost]
        public async Task<IActionResult> UnblockUser([FromBody] ApproveUser approveUser)
        {
            var userDTO = await _userManagement.SingleUser(approveUser.Username);
            if (userDTO == null)
                throw new CustomException($"User {approveUser.Username} is not registered", null, ErrorCodes.IsUserRegistered, ConstantData.Txn);
            if (userDTO.Islocked == 1)
                throw new CustomException($"User {approveUser.Username} is not block", null, ErrorCodes.UserNotBlock, ConstantData.Txn);
            return Ok(await _userManagement.UnlockUser(approveUser));
        }

        [Route("ApproveUser")]
        [HttpPost]
        public async Task<IActionResult> ApproveUser([FromBody] ApproveUser approveUser)
        {
            var userDTO = await _userManagement.SingleUser(approveUser.Username);
            if (userDTO == null)
                throw new CustomException($"User {approveUser.Username} is not registered", null, ErrorCodes.IsUserRegistered, ConstantData.Txn);
            if (userDTO.Approved == 1)
                throw new CustomException($"User {approveUser.Username} already approved", null, ErrorCodes.UserAlreadyApproved, ConstantData.Txn);
            return Ok(await _userManagement.ApproveUser(approveUser));
        }

        [Route("ReactivateUser")]
        [HttpPost]
        public async Task<IActionResult> ReactivateUser([FromBody] ApproveUser approveUser)
        {
            var userDTO = await _userManagement.SingleUser(approveUser.Username);
            if (userDTO == null)
                throw new CustomException($"User {approveUser.Username} is not registered", null, ErrorCodes.IsUserRegistered, ConstantData.Txn);
            if (userDTO.Status == 1)
                throw new CustomException($"User {approveUser.Username} is not deleted", null, ErrorCodes.UserNotDeleted, ConstantData.Txn);
            return Ok(await _userManagement.ReactivateUser(approveUser));
        }
    }
}