using FluentValidation;

namespace BookStore.Application.Features.Role.Queries.Get
{
     internal class GetRoleQueryValidator:AbstractValidator<GetRoleQuery>
     {
          public GetRoleQueryValidator()
          {
               RuleFor(r => r.Id)
                    .NotNull().WithMessage("The Role id is required")
                    .NotEmpty().WithMessage("The Role id cannot be empty");
          }
     }
}
