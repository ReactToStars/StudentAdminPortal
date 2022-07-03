using FluentValidation;
using StudentAdminPortalAPI.Core.Infrastructures;
using StudentAdminPortalAPI.Core.ViewModels;

namespace StudentAdminPortalAPI.Validators
{
    public class AddStudentRequestValidation : AbstractValidator<StudentVM>
    {

        public AddStudentRequestValidation(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.physicalAddress).NotEmpty();
            RuleFor(x => x.postalAddress).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).GreaterThan(99999).LessThan(1000000000);
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = unitOfWork.Gender.GetAll().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if(gender != null)
                {
                    return true;
                }

                return false;
            }).WithMessage("Please select a valid Gender");
        }
    }
}
