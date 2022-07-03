namespace StudentAdminPortalAPI.Core.Repositories
{
    public interface IBaseRepository<Entity>
    {
        Task<IEnumerable<Entity>> GetAll();
        void Add(Entity entity);
        void Delete(Entity entity);
        void Update(Entity entity);
        Task<Entity> GetById(Guid id);

    }
}
