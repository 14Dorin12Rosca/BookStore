using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.Application.Contracts.User;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Application.Features.Login.Commands.Login
{
    public record LoginCommand(LoginRequest Model) : IRequest<string>
    {
        public string? Email = Model.Email;
        public string? Password = Model.Email;
    }


    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IValidator<LoginCommand> _validator;
        private readonly IGetUserByEmail _userByEmail;
        private readonly IConfiguration _configuration;
        public LoginCommandHandler(IValidator<LoginCommand> validator, IGetUserByEmail userByEmail, IConfiguration configuration)
        {
            _validator = validator;
            _userByEmail = userByEmail;
            _configuration = configuration;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            var user = await _userByEmail.GetAsync(request.Email!);
            var token = CreateToken(user);
            return token;
        }

        private string CreateToken(Domain.Entities.User? user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
               {
                    new Claim(ClaimTypes.NameIdentifier, user?.Email!),
                    new Claim(ClaimTypes.Surname, user?.Email!),
                    new Claim(ClaimTypes.Email, user?.Email!)
               };
            if (user?.Role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role.Name!));
            }
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                 expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
