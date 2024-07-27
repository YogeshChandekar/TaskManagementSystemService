using TaskManagementService.Models.UserManagement;
using TaskManagementService.Models;
using TaskManagementPortal.Models.TaskManagement;
using TaskManagementService.Models.TaskManagement;

namespace TaskManagementService.Interface
{
    public interface ITaskManagement
    {
        Task<List<TaskList>> TaskList();

        Task<BaseResponse> AddTask(AddTask addTask);

        Task<TaskList> SingleTask(int Id);

        Task<BaseResponse> UpdateTask(AddTask addTask);

        Task<BaseResponse> DeleteTask(DeleteTask deleteTask);

        Task<List<string>> EmailList();
    }
}
