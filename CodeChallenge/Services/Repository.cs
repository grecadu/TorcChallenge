using CodeChallenge.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }
    }

}
