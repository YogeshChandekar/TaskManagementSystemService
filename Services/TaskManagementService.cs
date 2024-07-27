using log4net;
using TaskManagementService.Interface;
using TaskManagementService.Models;
using TaskManagementService.Queries;
using TaskManagementPortal.Models.TaskManagement;
using TaskManagementService.Models.TaskManagement;

namespace TaskManagementService.Services
{
    public class TasksManagementService : ITaskManagement
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TasksManagementService));
        private readonly ICommanDbHander _commanDbHander;

        public TasksManagementService(ICommanDbHander commanDbHander)
        {
            _commanDbHander = commanDbHander;
        }
        public Task<List<TaskList>> TaskList()
        {
            return _commanDbHander.GetData<TaskList>(TaskDBQueries._getTaskList, log, "Task List successfully retrieved", "An error occurred while retrieving task list", ErrorCodes.TaskList, ConstantData.Txn);
        }

        public async Task<BaseResponse> AddTask(AddTask addTask)
        {
            BaseResponse baseResponse = await _commanDbHander.AddUpdateDeleteData(
                TaskDBQueries._addTask,
                log,
                $"The task has been successfully created.",
                $"An error occurred while creating task details.",
                $"",
                ErrorCodes.AddTask,
                ConstantData.Txn,
                new
                {
                    @TaskName = addTask.TaskName,
                    @Assignee = addTask.Assignee,
                    @Reporter = addTask.Reporter,
                    @StartDate = addTask.StartDate,
                    @EndDate = addTask.EndDate,
                    @Status = addTask.Status,
                    @Active = 1,
                    @AttachFile = addTask.AttachFileInBase64,
                    @Notes = addTask.Notes
                }).ConfigureAwait(false);
            return baseResponse;
        }

        public Task<TaskList> SingleTask(int Id)
        {
            return _commanDbHander.GetSingleData<TaskList>(TaskDBQueries._getSingleTask, log,
                $"The task details successfully retrieved",
                $"An error occurred while retrieving task details",
                ErrorCodes.GetSingleTaskDetails, ConstantData.Txn, new { @id = Id });
        }

        public async Task<BaseResponse> UpdateTask(AddTask addTask)
        {
            BaseResponse baseResponse = new();
            try
            {
                baseResponse = await _commanDbHander.AddUpdateDeleteData(
                              TaskDBQueries._updateTask,
                              log,
                              $"The task has been successfully updated.",
                              $"An error occurred while updating task details.",
                              $"",
                              ErrorCodes.UpdateTask,
                              ConstantData.Txn,
                              new
                              {
                                  @id = addTask.ID,
                                  @TaskName = addTask.TaskName,
                                  @Assignee = addTask.Assignee,
                                  @Reporter = addTask.Reporter,
                                  @StartDate = addTask.StartDate,
                                  @EndDate = addTask.EndDate,
                                  @Status = addTask.Status,
                                  @AttachFile = addTask.AttachFileInBase64,
                                  @Notes = addTask.Notes
                              }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex, ErrorCodes.UpdateUser, ConstantData.Txn);
            }
            return baseResponse;
        }

        public async Task<BaseResponse> DeleteTask(DeleteTask deleteTask)
        {
            BaseResponse? res = await _commanDbHander.AddUpdateDeleteData(
              TaskDBQueries._deleteTask,
              log,
              $"The task successfully deleted by {deleteTask.Email}",
              $"An error occurred while deleting task.",
              "",
              ErrorCodes.DeleteTask,
              ConstantData.Txn,
              new
              {
                  @id = deleteTask.Id,
                  @active = 0,
              }).ConfigureAwait(false);
            return res;
        }

        public Task<List<string>> EmailList()
        {
            return _commanDbHander.GetData<string>(TaskDBQueries._getEmailList, log, "Email list successfully retrieved", "An error occurred while retrieving Email list", ErrorCodes.EmailList, ConstantData.Txn);
        }
    }
}
