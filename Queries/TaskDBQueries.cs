namespace TaskManagementService.Queries
{
    public class TaskDBQueries
    {
        internal const string _getTaskList = "select * from Task";

        internal const string _addTask = "INSERT INTO Task (TaskName, Assignee, Reporter, StartDate, EndDate, Status, Active, AttachFile, Notes) VALUES (@TaskName, @Assignee, @Reporter, @StartDate, @EndDate, @Status, @Active, @AttachFile, @Notes);";

        public const string _getSingleTask = "select ID,TaskName, Assignee, Reporter, StartDate, EndDate, Status, Active, AttachFile as AttachFileInBase64, Notes from Task where ID = @id";

        public const string _updateTask = "UPDATE Task SET TaskName = @TaskName, Assignee = @Assignee, Reporter = @Reporter, StartDate = @StartDate, EndDate = @EndDate, Status = @Status, AttachFile = @AttachFile, Notes = @Notes WHERE ID = @id;";

        public const string _deleteTask = "Update Task SET Active = @active where ID = @id;";

        public const string _getEmailList = "select email from users";
    }
}
