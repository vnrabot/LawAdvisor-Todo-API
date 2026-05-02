using TodoAPI.Application.Algorithms;
using TodoAPI.Application.Interfaces;
using TodoAPI.Domain.Entities;

namespace TodoAPI.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly ISortingStrategy<TodoTask> _sortingStrategy;

        public TaskService(ITaskRepository repository, ISortingStrategy<TodoTask> sortingStrategy)
        {
            _repository = repository;
            _sortingStrategy = sortingStrategy;
        }

        public async Task<List<TodoTask>> GetTaskAsync(int page, int limit)
        {
            var rawTasks = await _repository.GetPaginatedTaskAsync(page, limit);

            return _sortingStrategy.Sort(rawTasks);
        }

        public async Task<TodoTask> AddTaskAsync(string title, string details)
        {
            var task = new TodoTask
            {
                Title = title,
                Details = details,
                Rank = Guid.NewGuid().ToString()
            };

            await _repository.AddTaskAsync(task);
            return task;
        }

        public async Task<TodoTask?> UpdateTaskAsync(int id, string title, string details)
        {
            var task = await _repository.GetTaskByIdAsync(id);
            if (task == null) return null;

            task.Title = title;
            task.Details = details;

            await _repository.UpdateTaskAsync(task);
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _repository.GetTaskByIdAsync(id);
            if (task == null) return false;

            await _repository.DeleteTaskAsync(task);
            return true;
        }

        public async Task<bool> ReorderTaskAsync(int id, string previousRank, string nextRank)
        {
            var task = await _repository.GetTaskByIdAsync(id);
            if (task == null) return false;

            // handle the "move more than 50 times" constraint
            string newRank;
            if (string.IsNullOrEmpty(previousRank)) newRank = "a";
            else if (string.IsNullOrEmpty(nextRank)) newRank = previousRank + "z";
            else newRank = previousRank + "m";

            task.Rank = newRank;

            await _repository.UpdateTaskAsync(task);
            return true;
        }
    }
}