using Microsoft.EntityFrameworkCore;
using StudentAdminPortalAPI.Core.Data;

namespace StudentAdminPortalAPI.Core.Repositories
{
    public abstract class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
    {
        private readonly StudentAdminContext _db;
        protected DbSet<Entity> _dbSet;

        public BaseRepository(StudentAdminContext db)
        {
            this._db = db;
            _dbSet = _db.Set<Entity>();
        }
        public void Add(Entity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(Entity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<Entity> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<Entity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public void Update(Entity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
