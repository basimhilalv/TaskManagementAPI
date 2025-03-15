using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class TaskServices : ITaskServices
    {
        public readonly List<Tasks> TaskData = new List<Tasks>();

        public IEnumerable<Tasks?> GetTaskByName(string title)
        {
            
                var task = TaskData.Where(t=>t.Title.StartsWith(title));
                if (task == null) return null;
                return task;
        }
        public Tasks? AddTask(Tasks task)
        {
            if (TaskData.Any(t => t.Title == task.Title)) return null;
            var newTask = new Tasks
            {
                Title = task.Title,
                Description = task.Description,
                Status = task.Status
            };
            newTask.Id = TaskData.Any() ? TaskData.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1 : 1;
            TaskData.Add(newTask);
            return newTask;
        }

        public Tasks? DeleteTask(int id)
        {
            if(TaskData.Any(t => t.Id == id))
            {
                var task = TaskData.FirstOrDefault(t => t.Id == id);
                TaskData.Remove(task);
                return task;
            }
            return null;
        }

        public IEnumerable<Tasks> GetAllTasks()
        {
            var tasks = TaskData;
            return tasks;
        }

        public Tasks GetTaskById(int id)
        {
            if(TaskData.Any(t => t.Id == id))
            {
                var task = TaskData.FirstOrDefault(t => t.Id == id);
                return task;
            }
            return null;
        }

        public Tasks UpdateTask(int id, Tasks task)
        {
            if (task == null) return null;
            if(TaskData.Any(t => t.Id == id))
            {
                var taskToUpdate = TaskData.FirstOrDefault(t => t.Id == id);
                taskToUpdate.Title = task.Title;
                taskToUpdate.Description = task.Description;
                taskToUpdate.Status = task.Status;
                return taskToUpdate;
            }
            return null;
        }
    }
}
