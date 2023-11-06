using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Exceptions;
using MediatR;

namespace BookStore.Application.Features.Role.Queries
{
     public record GetRolesQuery : IRequest<IEnumerable<RoleDto>?>;
     public class GetRolesQueryHandler :IRequestHandler<GetRolesQuery,IEnumerable<RoleDto>?>
     {
          private readonly ISelectAll<Domain.Entities.Role> _entitiesAll;

          public GetRolesQueryHandler(ISelectAll<Domain.Entities.Role> entitiesAll)
          {
               _entitiesAll = entitiesAll;
          }

          public async Task<IEnumerable<RoleDto>?> Handle(GetRolesQuery request, CancellationToken cancellationToken)
          {
               var result = await _entitiesAll.GetAsync();
               var enumerable = result as Domain.Entities.Role[] ?? result.ToArray();
               if (!enumerable.Any())
               {
                    throw new NotFoundException("The List of Roles is Empty");
               }
               IEnumerable<RoleDto> roles = enumerable.Select(role => new RoleDto
               {
                    Id = role.Id,
                    Name = role.Name,
               });
               return roles;
          }
     }
}
