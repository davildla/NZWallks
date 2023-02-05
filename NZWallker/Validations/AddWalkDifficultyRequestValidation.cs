using FluentValidation;

namespace NZWallker.API.Validations
{
    public class AddWalkDifficultyRequestValidation : AbstractValidator<Models.DTO.AddWalkDifficulty>
    {
        public AddWalkDifficultyRequestValidation() 
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
