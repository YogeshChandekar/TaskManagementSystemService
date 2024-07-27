namespace TaskManagementService
{
    public static class ErrorCodes
    {
        public const string UserList = "E-2000-1";
        public const string AddUser = "E-2000-2";
        public const string GetSingleUserDetails = "E-2000-3";
        public const string IsUserRegistered = "E-2000-4";
        public const string UpdateUser = "E-2000-5";
        public const string DeleteUser = "E-2000-6";
        public const string UserNotBlock = "E-2000-7";
        public const string UnblockUser = "E-2000-8";
        public const string UserAlreadyApproved = "E-2000-9";
        public const string ApproveUser = "E-2000-10";
        public const string UserNotDeleted = "E-2000-11";
        public const string ReactiveUser = "E-2000-12";
        public const string UserAlreadyDeleted = "E-2000-13";


        public const string UIPasswordDecryption = "E-3000-1";


        public const string UserAuthentication = "E-4000-1";
        public const string loggedUserIsLocked = "E-4000-2";
        public const string loggedUserNotApproved = "E-4000-3";
        public const string GetUserPassword = "E-4000-4";
        public const string UserNotAuthenticate = "E-4000-5";

        public const string TaskList = "E-5000-1";
        public const string AddTask = "E-5000-2";
        public const string GetSingleTaskDetails = "E-5000-3";
        public const string IsTaskExist = "E-5000-4";
        public const string UpdateTask = "E-5000-5";
        public const string TaskAlreadyDeleted = "E-5000-6";
        public const string DeleteTask = "E-5000-7";
        public const string EmailList = "E-5000-8";
    }
}
