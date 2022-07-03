using StudentAdminPortalAPI.Core.Entitties;

namespace StudentAdminPortalAPI.Core.Repositories
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<bool> UpdateProfileImage(Guid id, string profileImageUrl);
    }
}
