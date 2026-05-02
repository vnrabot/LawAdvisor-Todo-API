using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Domain.Entities;

namespace TodoAPI.Application.Services
{
    public interface ITaskService
    {
        //parameters to satisfy "1 million tasks"
        Task<List<TodoTask>> GetTaskAsync(int page, int limit);
        Task<TodoTask> AddTaskAsync(string title, string details);
        Task<TodoTask?> UpdateTaskAsync(int id, string title, string details);
        Task<bool> DeleteTaskAsync(int id);

        //Reordering Tasks
        Task<bool> ReorderTaskAsync(int id, string previousRank, string nextRank);
    }
}