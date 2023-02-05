using FluentValidation;

namespace NZWallker.API.Validations
{
    public class AddWalkRequestValidator :  AbstractValidator<Models.DTO.AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor( x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
