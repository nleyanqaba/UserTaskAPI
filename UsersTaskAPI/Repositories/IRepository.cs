using System.Collections.Generic;
using System.Threading.Tasks;
using UserTasksAPI.Models;

namespace UserTasksAPI.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }

    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmail(string email);
        Task<bool> ValidateUser(string email, string password);
    }
}
