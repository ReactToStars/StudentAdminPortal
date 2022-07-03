using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.Core.Infrastructures;

namespace StudentAdminPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GendersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _unitOfWork.Gender.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:Guid}", Name = "GetGender")]
        public async Task<IActionResult> GetGender(Guid id)
        {
            try
            {
                var response = await _unitOfWork.Gender.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
