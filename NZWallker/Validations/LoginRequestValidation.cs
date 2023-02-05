using FluentValidation;

namespace NZWallker.API.Validations
{
    public class LoginRequestValidation : AbstractValidator<Models.DTO.LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor( x => x.UserName ).NotEmpty();  
            RuleFor( x => x.Password ).NotEmpty();
        }
    }
}
