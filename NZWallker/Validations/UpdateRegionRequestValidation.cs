using FluentValidation;

namespace NZWallker.API.Validations
{
    public class UpdateRegionRequestValidation : AbstractValidator<Models.DTO.UpdateRegion>
    {
        public UpdateRegionRequestValidation()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
