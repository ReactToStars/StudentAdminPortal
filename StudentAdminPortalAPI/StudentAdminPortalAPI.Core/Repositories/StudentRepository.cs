using Microsoft.EntityFrameworkCore;
using StudentAdminPortalAPI.Core.Data;
using StudentAdminPortalAPI.Core.Entitties;

namespace StudentAdminPortalAPI.Core.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentAdminContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Student>> GetAll()
        {
            return await _dbSet.Include(nameof(Gender)).Include(x => x.Address).ToListAsync();

        }

        public override async Task<Student> GetById(Guid id)
        {
            return await _dbSet.Include(nameof(Gender)).Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateProfileImage(Guid id, string profileImageUrl)
        {
            var student = await GetById(id);

            if (student != null)
            {
                student.ProfileImageUrl = profileImageUrl;
                return true;
            }

            return false;
        }
    }
}
