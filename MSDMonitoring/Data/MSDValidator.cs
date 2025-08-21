using FluentValidation;

namespace MSDMonitoring.Data
{
    public class MSDValidator : AbstractValidator<PrintLabelModel>
    {
        public MSDValidator()
        {
            RuleFor(res => res.ReelID)
                  .NotEmpty().WithMessage("Username is required");

        }
    }
}
