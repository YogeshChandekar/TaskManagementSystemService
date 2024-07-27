namespace TaskManagementService.Queries
{
    public class LoginDBQueries
    {
        internal const string _getLoginUser = "select id as ID,name as Name, email as Email, approved as Approved,is_admin as IsAdmin, status as Status, contact_no as ContactNo , attempt_count as Attemptcount, is_locked as Islocked from users where Email = @email AND Status=1";

        internal const string _updateUserAttempt = "update users set attempt_count=@attemptcount where lower(users.email)= lower(@email)";

        internal const string _updateUserLockStatus = "update users set islocked=@islocked  where lower(users.email)= lower(@email)";
    }
}
