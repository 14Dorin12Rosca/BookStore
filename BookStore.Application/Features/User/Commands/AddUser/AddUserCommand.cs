using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.User.Commands.AddUser
{
     public record AddUserCommand(CreateUserRequest Model) : IRequest<UserDto?>
    {
        public string? Email = Model.Email;
        public string? Password = Model.Password;
        public string? ConfirmPassword = Model.ConfirmPassword;

    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto?>
    {
         private readonly IValidator<AddUserCommand> _validator;
         private readonly IAddEntity<Domain.Entities.User> _entity;


          public AddUserCommandHandler( IValidator<AddUserCommand> validator, IAddEntity<Domain.Entities.User> entity)
          {
               _validator = validator;
               _entity = entity;
          }

         public async Task<UserDto?> Handle(AddUserCommand request, CancellationToken cancellationToken)
         {
              var validation = await _validator.ValidateAsync(request, cancellationToken);
              if (!validation.IsValid)
              {
                   throw new ValidationException(validation.Errors);
              }

              var user = new Domain.Entities.User
              {
                   Id = Guid.NewGuid(),
                   Email = request.Email,
                   Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
              };
              var result = await _entity.InsertAsync(user);
              if (result == null)
              {
                   throw new DataBaseErrorException();
              }

              return new UserDto
              {
                   Id = result.Id,
                   Email = result.Email,
              };
         }
    }
}
