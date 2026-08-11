using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestCQRS.Contexts;
using TestCQRS.Models;

namespace TestCQRS.Repositories
{
    public class Reposirory<T> : IReposirory<T> where T : BaseModel
    {
        private readonly ApplicationContext _dbcontext;

        public Reposirory(ApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task Add(T entity)
        {
          await  _dbcontext.AddAsync(entity);
         
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> prdecit)
        {
            return _dbcontext.Set<T>().Where(prdecit).AsNoTracking();
        }

        public IQueryable<T> GetAll()
        {
            return _dbcontext.Set<T>();
        }

        public async Task SaveChanges()
        {
            try
            {

            await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
