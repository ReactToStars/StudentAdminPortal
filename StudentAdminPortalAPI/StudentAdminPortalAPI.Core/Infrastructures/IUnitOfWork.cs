using StudentAdminPortalAPI.Core.Data;
using StudentAdminPortalAPI.Core.Repositories;

namespace StudentAdminPortalAPI.Core.Infrastructures
{
    public interface IUnitOfWork : IDisposable
    {
        StudentRepository Student{ get;}
        AddressRepository Address{ get;}
        GenderRepository Gender{ get;}
        LocalStorageImageRepository StorageImage{ get;}
        StudentAdminContext Db { get;}
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
