using Microsoft.EntityFrameworkCore;
using UserTasksAPI.Models;

namespace UserTasksAPI.Data
{
    public class UserTaskDbContext : DbContext
    {
        public UserTaskDbContext(DbContextOptions<UserTaskDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<TaskItem>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.Assignee)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
