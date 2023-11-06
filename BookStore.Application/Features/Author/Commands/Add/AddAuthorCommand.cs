using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.Author.Commands.Add
{
     public record AddAuthorCommand(AddAuthorRequest Model) :IRequest<AuthorDto?>
     {
          public string? FirstName = Model.FirstName;
          public string? LastName = Model.LastName;
     }
     public class AddAuthorCommandHandler :IRequestHandler<AddAuthorCommand, AuthorDto?>
     {
          private readonly IAddEntity<Domain.Entities.Author> _addEntity;
          private readonly IValidator<AddAuthorCommand> _validator;

          public AddAuthorCommandHandler(IAddEntity<Domain.Entities.Author> addEntity, IValidator<AddAuthorCommand> validator)
          {
               _addEntity = addEntity;
               _validator = validator;
          }

          public async Task<AuthorDto?> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
          {
               var validation = await _validator.ValidateAsync(request, cancellationToken);
               if (!validation.IsValid)
               {
                    throw new ValidationException(validation.Errors);
               }
               var genre = new Domain.Entities.Author()
               {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName
               };
               var result = await _addEntity.InsertAsync(genre);
               //to change
               if (result != null)
                    return new AuthorDto()
                    {
                         Id = result.Id,
                         FirstName = result.FirstName,
                         LastName = result.LastName,
                    };
               throw new DataBaseErrorException();
          }
     }
}
