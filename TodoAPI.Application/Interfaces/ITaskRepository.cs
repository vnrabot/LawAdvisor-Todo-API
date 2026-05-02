using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Domain.Entities;

namespace TodoAPI.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TodoTask>> GetPaginatedTaskAsync(int page, int limit);
        Task<TodoTask?> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TodoTask task);
        Task UpdateTaskAsync(TodoTask task);
        Task DeleteTaskAsync(TodoTask task);
    }
}