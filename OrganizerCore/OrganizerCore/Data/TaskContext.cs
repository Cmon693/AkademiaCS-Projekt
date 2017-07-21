
using Microsoft.EntityFrameworkCore;
using Organizer.Models;

namespace Organizer.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().ToTable("Task");
            modelBuilder.Entity<Subtask>().ToTable("Subtask");

        }
    }
}