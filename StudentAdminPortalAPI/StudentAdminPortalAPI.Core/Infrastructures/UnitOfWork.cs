using StudentAdminPortalAPI.Core.Data;
using StudentAdminPortalAPI.Core.Repositories;

namespace StudentAdminPortalAPI.Core.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentAdminContext _db;
        private StudentRepository _studentRepository;
        private AddressRepository _addressRepository;
        private GenderRepository _genderRepository;
        private LocalStorageImageRepository _storageImage;

        public UnitOfWork(StudentAdminContext db)
        {
            _db = db;
        }
        public StudentRepository Student => (_studentRepository ?? new StudentRepository(Db));

        public AddressRepository Address => (_addressRepository ?? new AddressRepository(Db));

        public GenderRepository Gender => (_genderRepository ?? new GenderRepository(Db));

        public StudentAdminContext Db => _db;

        public LocalStorageImageRepository StorageImage => (_storageImage ?? new LocalStorageImageRepository());

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _db.SaveChangesAsync();
        }
    }
}
