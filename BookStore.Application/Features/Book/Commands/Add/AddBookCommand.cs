using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.Book.Commands.Add
{
    public record AddBookCommand(CreateBookRequest Model) : IRequest<BookDto?>
    {
         public string? Title = Model.Title;
         public string? Description = Model.Description;
         public double? Price = Model.Price;
         public int? Quantity = Model.Quantity;
         public Guid? AuthorId = Model.AuthorId;
         public Guid? GenreId =Model.GenreId;
     }

    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, BookDto?>
    {
        private readonly IAddEntity<Domain.Entities.Book> _addEntity;
        private readonly IValidator<AddBookCommand> _validator;

        public AddBookCommandHandler(IAddEntity<Domain.Entities.Book> addEntity, IValidator<AddBookCommand> validator)
        {
            _addEntity = addEntity;
            _validator = validator;
        }

        public async Task<BookDto?> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            var genre = new Domain.Entities.Book
            {
                 Id = Guid.NewGuid(),
                 Title = request.Title,
                 Description = request.Description,
                 Price = request.Price,
                 Quantity = request.Quantity,
                 AuthorId = request.AuthorId,
                 GenreId = request.GenreId
            };
            var result = await _addEntity.InsertAsync(genre);
            //to change
            if (result != null)
                return new BookDto()
                {
                     Id = Guid.NewGuid(),
                     Title = result.Title,
                     Description = result.Description,
                     Price = result.Price,
                     Quantity = result.Quantity,
                     AuthorId = result.AuthorId,
                     GenreId = result.GenreId
                };
            throw new DataBaseErrorException();
        }
    }
}
