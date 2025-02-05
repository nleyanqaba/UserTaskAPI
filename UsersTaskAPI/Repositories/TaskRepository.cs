using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserTasksAPI.Data;
using UserTasksAPI.Models;

namespace UserTasksAPI.Repositories
{

    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetExpiredTasksAsync();
        Task<IEnumerable<TaskItem>> GetActiveTasksAsync();
        Task<IEnumerable<TaskItem>> GetTasksFromDateAsync(DateTime date);
    }

    public class TaskRepository : ITaskRepository
    {
        private readonly UserTaskDbContext _context;

        public TaskRepository(UserTaskDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TaskItem>> GetExpiredTasksAsync()
        {
            return await _context.Tasks
                                 .Where(t => t.DueDate < DateTime.Now)
                                 .ToListAsync();
        }


        public async Task<IEnumerable<TaskItem>> GetActiveTasksAsync()
        {
            return await _context.Tasks
                                 .Where(t => t.DueDate >= DateTime.Now)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksFromDateAsync(DateTime date)
        {
            return await _context.Tasks
                                 .Where(t => t.DueDate >= date)
                                 .ToListAsync();
        }



        public async Task<IEnumerable<TaskItem>> GetAll() => await _context.Tasks.ToListAsync();
        public async Task<TaskItem> GetById(int id) => await _context.Tasks.FindAsync(id);
        public async Task Add(TaskItem entity)
        {
            await _context.Tasks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(TaskItem entity)
        {
            _context.Tasks.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

   
    }
}
