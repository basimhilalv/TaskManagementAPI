using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public readonly ITaskServices _taskServices;
        public TaskController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Tasks>> GetAllTasks()
        {
            var tasks = _taskServices.GetAllTasks();
            return Ok(tasks);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetTask{id}")]
        public ActionResult<Tasks> GetTaskById(int id)
        {
            var task = _taskServices.GetTaskById(id);
            if (task == null)
            {
                return NotFound("Task not found");
            }
            return Ok(task);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpPost("Create")]
        public ActionResult<Tasks> AddTask(Tasks task)
        {
            var newTask = _taskServices.AddTask(task);
            if (newTask == null)
            {
                return BadRequest("Task already exists");
            }
            return Ok(newTask);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update{id}")]
        public ActionResult<Tasks> UpdateTask(int id, Tasks task)
        {
            var taskToUpdate = _taskServices.UpdateTask(id, task);
            if (taskToUpdate == null)
            {
                return NotFound("Task not found");
            }
            return Ok(taskToUpdate);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete{id}")]
        public ActionResult<Tasks> DeleteTask(int id)
        {
            var taskToDelete = _taskServices.DeleteTask(id);
            if (taskToDelete == null)
            {
                return NotFound("Task not found");
            }
            return Ok(taskToDelete);
        }
    }
}
