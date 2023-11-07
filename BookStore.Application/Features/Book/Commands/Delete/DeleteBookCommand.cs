using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.Book.Commands.Delete
{
    public record DeleteBookCommand(Guid Id) : IRequest<bool>
    {
         public Guid Id = Id;
    }

    public class AddGenreCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IDeleteEntity<Domain.Entities.Book> _deleteEntity;
        private readonly IGetEntityById<Domain.Entities.Book> _entityById;
        private readonly IValidator<DeleteBookCommand> _validator;

        public AddGenreCommandHandler(IValidator<DeleteBookCommand> validator, IDeleteEntity<Domain.Entities.Book> deleteEntity, IGetEntityById<Domain.Entities.Book> entityById)
        {
             _validator = validator;
             _deleteEntity = deleteEntity;
             _entityById = entityById;
        }


        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var book = await _entityById.GetAsync(request.Id);
            if (book == null)
            {
                 throw new NotFoundException();
            }
            var result = await _deleteEntity.DeleteAsync(book);
            if(result)
                 return true;
            throw new DataBaseErrorException();
        }
    }
}
