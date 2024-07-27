namespace TaskManagementService.Queries
{
    internal static class UserDBQueries
    {
        internal const string _getUserList = "select id as ID,name as Name, email as Email, approved as Approved,is_admin as IsAdmin, status as Status, contact_no as ContactNo , attempt_count as Attemptcount, is_locked as Islocked from users";

        internal const string _addUser = "INSERT INTO users (name, email, approved, is_admin, status, contact_no, attempt_count, is_locked, password) VALUES (@name, @email, @approved, @is_admin, @status, @contact_no, @attempt_count, @is_locked, @password);";

        public const string _getSingleUser = "select id as ID,name as Name, email as Email, approved as Approved,is_admin as IsAdmin, status as Status, contact_no as ContactNo , attempt_count as Attemptcount, is_locked as Islocked from users where Email = @email";

        public const string _updateUser = "update users set name=@cuser_name,contact_no=@contact_no,is_admin=@is_admin where lower(email)= lower(@email)";

        public const string _deleteUser = "update users set status=@active where lower(email)= lower(@email)";

        public const string _unlockUser = "update users set attempt_count=@attemptcount,is_locked=@islocked where lower(email)= lower(@email)";

        public const string _setApprovalStatusUser = "update users set approved=@approved where lower(email)= lower(@email)";

        public const string _reactivateUser = "update users set status=@active where lower(email)= lower(@email)";

        internal const string _getUserPassword = "select password from users where email =@email";
    }
}