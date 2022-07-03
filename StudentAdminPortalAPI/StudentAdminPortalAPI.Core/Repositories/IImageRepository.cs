using Microsoft.AspNetCore.Http;

namespace StudentAdminPortalAPI.Core.Repositories
{
    public interface IImageRepository
    {
        Task<string> Upload(IFormFile file, string fileName);
    }
}
