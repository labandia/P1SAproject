using FluentValidation;

namespace ProductConfirm.Models
{
    public  class AuthValidation : AbstractValidator<AuthModel>
    {
        public AuthValidation()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Password)
                 .Cascade(CascadeMode.Stop) 
                .NotEmpty().WithMessage("Password is Required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
