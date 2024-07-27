using Microsoft.AspNetCore.Mvc;
using System.Text;
using TaskManagementPortal.Models.TaskManagement;
using TaskManagementService.Interface;
using TaskManagementService.Models;
using TaskManagementService.Models.TaskManagement;

namespace TaskManagementService.Controllers
{
    public class TaskManagementController : Controller
    {
        private readonly ITaskManagement _taskManagement;

        public TaskManagementController(ITaskManagement taskManagement)
        {
            _taskManagement = taskManagement;
        }

        [Route("GetTaskList")]
        [HttpGet]
        public async Task<IActionResult> TaskList()
        {
            return Ok(await _taskManagement.TaskList());
        }

        [Route("AddTask")]
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] AddTask addTask)
        {
            BaseResponse addTaskRes = await _taskManagement.AddTask(addTask);
            return Ok(addTaskRes);
        }

        [Route("GetTaskDetails/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetTaskDetails(int Id)
        {
            var taskDetail = await _taskManagement.SingleTask(Id);
            if (taskDetail == null)
                throw new CustomException($"Task details not found", null, ErrorCodes.GetSingleTaskDetails, ConstantData.Txn);
            return Ok(taskDetail);
        }

        [Route("UpdateTask")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] AddTask addTask)
        {
            var taskDTO = await _taskManagement.SingleTask(addTask.ID);
            if (taskDTO == null)
                throw new CustomException($"Task not found", null, ErrorCodes.IsTaskExist, ConstantData.Txn);
            BaseResponse updateTaskRes = await _taskManagement.UpdateTask(addTask);
            return Ok(updateTaskRes);
        }

        [Route("DeleteTask")]
        [HttpPost]
        public async Task<IActionResult> DeleteTask([FromBody] DeleteTask deleteTask)
        {
            var taskDTO = await _taskManagement.SingleTask(deleteTask.Id);
            if (taskDTO == null)
                throw new CustomException($"Task not found", null, ErrorCodes.IsTaskExist, ConstantData.Txn);
            if (taskDTO.Active == 0)
                throw new CustomException($"Task is already deleted", null, ErrorCodes.TaskAlreadyDeleted, ConstantData.Txn);
            return Ok(await _taskManagement.DeleteTask(deleteTask));
        }

        [Route("GetEmails")]
        [HttpGet]
        public async Task<IActionResult> GetEmails()
        {
            return Ok(await _taskManagement.EmailList());
        }
    }
}
