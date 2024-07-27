using log4net;
using TaskManagementService.Interface;
using TaskManagementService.Models.LoginManagement;
using TaskManagementService.Models.UserManagement;
using TaskManagementService.Models;
using TaskManagementService.Queries;

namespace TaskManagementService.Services
{
    public class LoginService : ILogin
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginService));
        private readonly ICommanDbHander _commanDbHander;
        private readonly IUserManagement _userManagement;

        public LoginService(ICommanDbHander commanDbHander, IUserManagement userManagement)
        {
            _commanDbHander = commanDbHander;
            _userManagement = userManagement;
        }

        public async Task<UsersInfo> Authentication(AuthenticateUser authenticateUser)
        {

            UsersInfo? userinfo = await _commanDbHander.GetSingleData<UsersInfo>(LoginDBQueries._getLoginUser,
                                    log, $"The {authenticateUser.Email} user details successfully retrieved for authentication",
                                    $"An error occurred while retrieving {authenticateUser.Email} user details for authentication",
                                    ErrorCodes.UserAuthentication,
                                    ConstantData.Txn,
                                    new { @email = authenticateUser.Email!.Trim() }).ConfigureAwait(false);
            if (userinfo == null)
                throw new CustomException($"The {authenticateUser.Email} user not registered with an application", null, ErrorCodes.IsUserRegistered, ConstantData.Txn);

            if (userinfo.Approved == 0)
                throw new CustomException($"The {authenticateUser.Email} user not approved with an application", null, ErrorCodes.loggedUserNotApproved, ConstantData.Txn);

            if (userinfo.Islocked == 0)
                throw new CustomException($"Your account {authenticateUser.Email} has been locked.", null, ErrorCodes.loggedUserIsLocked, ConstantData.Txn);

            string dbPass = await _userManagement.GetPassword(authenticateUser.Email, false).ConfigureAwait(false);
            if (string.IsNullOrEmpty(dbPass))
                throw new CustomException($"Unable to verify credentials of {authenticateUser.Email} user", null, ErrorCodes.UserNotAuthenticate, ConstantData.Txn);

            if (dbPass != authenticateUser.Password)
            {
                if (userinfo.Attemptcount < 3 && userinfo.Islocked != 0)
                {
                    userinfo.Attemptcount++;
                    _ = await _commanDbHander.AddUpdateDeleteData(LoginDBQueries._updateUserAttempt,
                        log, $"The {authenticateUser.Email} user attempt count updated",
                        $"An error occurred while updating {authenticateUser.Email} user attempt count", "",
                        ErrorCodes.UserAuthentication,
                        ConstantData.Txn,
                        new
                        {
                            @attemptcount = userinfo.Attemptcount,
                            @email = authenticateUser.Email.Trim()
                        }).ConfigureAwait(false);
                    if (userinfo.Attemptcount == 3 && userinfo.Islocked == 1)
                    {
                        _ = await _commanDbHander.AddUpdateDeleteData(LoginDBQueries._updateUserLockStatus,
                            log, $"The {authenticateUser.Email} user lock status updated",
                        $"An error occurred while updating {authenticateUser.Email} user lock status", "",
                        ErrorCodes.UserAuthentication,
                        ConstantData.Txn,
                            new
                            {
                                @islocked = 0,
                                @email = authenticateUser.Email.Trim()
                            }).ConfigureAwait(false);
                        throw new CustomException($"No attempts left. Your account {authenticateUser.Email} has been locked.", null, ErrorCodes.loggedUserIsLocked, ConstantData.Txn);
                    }
                }
                throw new CustomException($"You have entered wrong credentials. You have only {(3 - userinfo.Attemptcount)} attempt left.", null, ErrorCodes.UserAuthentication, ConstantData.Txn);
            }
            else
            {
                if (userinfo.Attemptcount > 0)
                {
                    _ = await _commanDbHander.AddUpdateDeleteData(LoginDBQueries._updateUserAttempt,
                                        log, $"The {authenticateUser.Email} user attempt count updated",
                                        $"An error occurred while updating {authenticateUser.Email} user attempt count", "",
                                        ErrorCodes.UserAuthentication,
                                        ConstantData.Txn,
                                        new
                                        {
                                            @attemptcount = 0,
                                            @email = authenticateUser.Email.Trim()
                                        }).ConfigureAwait(false);
                }
                log.Info($"{authenticateUser.Email} user successfully logged in");
            }

            return userinfo;
        }

    }
}