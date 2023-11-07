using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.Book.Commands.Update
{
    public record UpdateBookCommand(UpdateBookRequest Model) : IRequest<BookDto?>
    {
         public Guid? Id = Model.Id;
         public string? Title = Model.Title;
         public string? Description = Model.Description;
         public double? Price = Model.Price;
         public int? Quantity = Model.Quantity;
         public Guid? AuthorId = Model.AuthorId;
         public Guid? GenreId =Model.GenreId;
     }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto?>
    {
        private readonly IUpdateEntity<Domain.Entities.Book> _updateEntity;
        private readonly IValidator<UpdateBookCommand> _validator;


        public UpdateBookCommandHandler(IUpdateEntity<Domain.Entities.Book> updateEntity, IValidator<UpdateBookCommand> validator)
        {
             _updateEntity = updateEntity;
             _validator = validator;
        }

        public async Task<BookDto?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            var book = new Domain.Entities.Book
            {
                 Id = request.Id,
                 Title = request.Title,
                 Description = request.Description,
                 Price = request.Price,
                 Quantity = request.Quantity,
                 AuthorId = request.AuthorId,
                 GenreId = request.GenreId
            };
            var result = await _updateEntity.UpdateAsync(book);
            //to change
            if (result)
                return new BookDto()
                {
                     Id = request.Id,
                     Title = book.Title,
                     Description = book.Description,
                     Price = book.Price,
                     Quantity = book.Quantity,
                     AuthorId = book.AuthorId,
                     GenreId = book.GenreId
                };
            throw new DataBaseErrorException();
        }
    }
}
