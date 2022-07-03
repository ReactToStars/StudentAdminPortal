using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.Core.Entitties;
using StudentAdminPortalAPI.Core.Infrastructures;

namespace StudentAdminPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddressesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _unitOfWork.Address.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:Guid}", Name = "GetAddress")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            try
            {
                var response = await _unitOfWork.Address.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Address address)
        {
            try
            {
                address.Id = new Guid();
                _unitOfWork.Address.Add(address);
                await _unitOfWork.SaveChangesAsync();
                return CreatedAtRoute(nameof(GetAddress), new { id = address.Id }, address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Update(Guid id, [FromBody] Address address)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                _unitOfWork.Address.Update(address);
                await _unitOfWork.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var address = await _unitOfWork.Address.GetById(id);
                _unitOfWork.Address.Delete(address);
                await _unitOfWork.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
