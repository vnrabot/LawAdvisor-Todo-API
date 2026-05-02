using Microsoft.EntityFrameworkCore;
using TodoAPI.Domain.Entities;

namespace TodoAPI.Infrastructure.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<TodoTask> TodoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Indexing rank colum for faster sorting
            modelBuilder.Entity<TodoTask>().HasIndex(t => t.Rank);
        }
    }
}