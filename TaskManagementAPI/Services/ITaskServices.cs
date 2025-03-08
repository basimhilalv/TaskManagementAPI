using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface ITaskServices
    {
        IEnumerable<Tasks> GetAllTasks();
        Tasks? GetTaskById(int id);
        Tasks? AddTask(Tasks task);
        Tasks? UpdateTask(int id, Tasks task);
        Tasks? DeleteTask(int id);
    }
}
