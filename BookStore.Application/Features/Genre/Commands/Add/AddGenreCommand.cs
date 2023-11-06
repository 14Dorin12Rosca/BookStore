using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.Genre.Commands.Add
{
    public record AddGenreCommand(CreateGenreRequest Model) : IRequest<GenreDto?>
    {
        public string? Name = Model.Name;
        public string? Description = Model.Description;
    }

    public class AddGenreCommandHandler : IRequestHandler<AddGenreCommand, GenreDto?>
    {
        private readonly IAddEntity<Domain.Entities.Genre> _addEntity;
        private readonly IValidator<AddGenreCommand> _validator;

        public AddGenreCommandHandler(IAddEntity<Domain.Entities.Genre> addEntity, IValidator<AddGenreCommand> validator)
        {
            _addEntity = addEntity;
            _validator = validator;
        }

        public async Task<GenreDto?> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            var genre = new Domain.Entities.Genre
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };
            var result = await _addEntity.InsertAsync(genre);
               //to change
               if (result != null)
                    return new GenreDto
                    {
                         Id = result.Id,
                         Name = result.Name,
                         Description = result.Description,
                    };
               throw new DataBaseErrorException();
        }
    }
}
