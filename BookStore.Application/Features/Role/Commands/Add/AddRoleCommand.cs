using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.Role.Commands.Add
{
    public record AddRoleCommand(CreateRoleRequest Model) : IRequest<RoleDto?>
    {
        public string? Name = Model.Name;
    }

    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, RoleDto?>
    {
        private readonly IAddEntity<Domain.Entities.Role> _addEntity;
        private readonly IValidator<AddRoleCommand> _validator;

        public AddRoleCommandHandler(IAddEntity<Domain.Entities.Role> addEntity, IValidator<AddRoleCommand> validator)
        {
            _addEntity = addEntity;
            _validator = validator;
        }

        public async Task<RoleDto?> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            var role = new Domain.Entities.Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };
            var result = await _addEntity.InsertAsync(role);
            //to change
            if (result != null)
            {
                 return new RoleDto
                 {
                      Id = result.Id,
                      Name = result.Name,
                 };
            }

            throw new DataBaseErrorException();
        }
    }
}
