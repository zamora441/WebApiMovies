using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiMovies.Data;
using WebApiMovies.Data.Entities;

namespace WebApiMovies.Data_Access.Implements
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            this._dbContext = applicationDbContext;
        }
        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }


    }
}
