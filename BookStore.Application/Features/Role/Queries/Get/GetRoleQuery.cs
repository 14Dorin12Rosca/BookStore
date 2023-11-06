using BookStore.Application.Contracts.DataBase;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.Role.Queries.Get
{
    public record GetRoleQuery(Guid? Id) : IRequest<RoleDto?>
    {
        public Guid? Id = Id;
    }
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, RoleDto?>
    {
        private readonly IGetEntityById<Domain.Entities.Role> _entityById;
        private readonly IValidator<GetRoleQuery> _validator;

        public GetRoleQueryHandler(IGetEntityById<Domain.Entities.Role> entityById, IValidator<GetRoleQuery> validator)
        {
            _entityById = entityById;
            _validator = validator;
        }

        public async Task<RoleDto?> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            var result = await _entityById.GetAsync(request.Id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new RoleDto()
            {
                Id = result.Id,
                Name = result.Name,
            };
        }
    }
}
