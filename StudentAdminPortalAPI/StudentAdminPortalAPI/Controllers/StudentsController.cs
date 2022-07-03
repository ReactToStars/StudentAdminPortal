using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.Core.Entitties;
using StudentAdminPortalAPI.Core.Infrastructures;
using StudentAdminPortalAPI.Core.ViewModels;

namespace StudentAdminPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _unitOfWork.Student.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id:Guid}", Name = "GetStudent")]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            try
            {
                var response = await _unitOfWork.Student.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("add", Name ="Create")]
        public async Task<IActionResult> Create([FromBody] StudentVM studentVM)
        {
            try
            {
                Student student = new Student();
                Address address = new Address();
                student.Id = new Guid();
                student.FirstName = studentVM.FirstName;
                student.LastName = studentVM.LastName;
                student.Email = studentVM.Email;
                student.GenderId = studentVM.GenderId;
                student.DateOfBirth = studentVM.DateOfBirth;
                student.Mobile = studentVM.Mobile;
                _unitOfWork.Student.Add(student);
                address.Id = new Guid();
                address.StudentId = student.Id;
                address.PhysicalAddress = studentVM.physicalAddress;
                address.PostalAddress = studentVM.postalAddress;
                _unitOfWork.Address.Add(address);
                await _unitOfWork.SaveChangesAsync();
                return CreatedAtRoute(nameof(GetStudent), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id:Guid}", Name ="Update")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] StudentVM studentVM)
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
                Student student = await _unitOfWork.Student.GetById(id);
                student.FirstName = studentVM.FirstName;
                student.LastName = studentVM.LastName;
                student.Email = studentVM.Email;
                student.GenderId = studentVM.GenderId;
                student.DateOfBirth = studentVM.DateOfBirth;
                student.Mobile = studentVM.Mobile;
                student.Address.PhysicalAddress = studentVM.physicalAddress;
                student.Address.PostalAddress = studentVM.postalAddress;
                _unitOfWork.Student.Update(student);
                await _unitOfWork.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}", Name ="Delete")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
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
                var student = await _unitOfWork.Student.GetById(id);
                _unitOfWork.Student.Delete(student);
                await _unitOfWork.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:Guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid id, IFormFile profileImage)
        {
            var validExtensions = new List<string>
            {
                ".jpeg",
                ".png",
                ".gif",
                ".jpg"
            };

            if(profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtensions.Contains(extension))
                {
                    //check if student exists
                    if (await _unitOfWork.Student.GetById(id) != null)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                        //Upload Image to local storage
                        var fileImagePath = await _unitOfWork.StorageImage.Upload(profileImage, fileName);

                        //update the profile image path in the database
                        if (await _unitOfWork.Student.UpdateProfileImage(id, fileImagePath))
                        {
                            _unitOfWork.SaveChangesAsync();
                            return Ok(fileImagePath);
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
                    }
                }
                return BadRequest("This is not a valid Image format");
            }

            return NotFound();
        }
    }
}
