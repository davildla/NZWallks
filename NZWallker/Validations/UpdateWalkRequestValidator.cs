using FluentValidation;

namespace NZWallker.API.Validations
{
    public class UpdateWalkRequestValidator : AbstractValidator<Models.DTO.UpdateWalk>
    {
        public UpdateWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
