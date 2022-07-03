using StudentAdminPortalAPI.Core.Data;
using StudentAdminPortalAPI.Core.Entitties;

namespace StudentAdminPortalAPI.Core.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(StudentAdminContext db) : base(db)
        {
        }
    }
}
