using System.Linq.Expressions;
using TestCQRS.Models;

namespace TestCQRS.Repositories
{
    public interface IReposirory<T> where T : BaseModel
    {
        Task Add(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> prdecit);

        Task SaveChanges();
    }
}
