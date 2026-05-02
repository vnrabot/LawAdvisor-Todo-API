using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TodoAPI.Application.Interfaces;
using TodoAPI.Domain.Entities;
using TodoAPI.Infrastructure.Data;

namespace TodoAPI.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoDbContext _context;

        public TaskRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoTask>> GetPaginatedTaskAsync(int page, int limit)
        {
            return await _context.TodoTasks
                  .AsNoTracking()
                  .Skip((page - 1) * limit)
                  .Take(limit)
                  .ToListAsync();
        }

        public async Task<TodoTask?> GetTaskByIdAsync(int id)
        {
            return await _context.TodoTasks.FindAsync(id);
        }

        public async Task AddTaskAsync(TodoTask task)
        {
            _context.TodoTasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TodoTask task)
        {
            _context.TodoTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TodoTask task)
        {
            _context.TodoTasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}