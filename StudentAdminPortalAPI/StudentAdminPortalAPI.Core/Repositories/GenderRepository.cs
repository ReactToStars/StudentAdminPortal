using StudentAdminPortalAPI.Core.Data;
using StudentAdminPortalAPI.Core.Entitties;

namespace StudentAdminPortalAPI.Core.Repositories
{
    public class GenderRepository : BaseRepository<Gender>, IGenderRepository
    {
        public GenderRepository(StudentAdminContext db) : base(db)
        {
        }
    }
}
