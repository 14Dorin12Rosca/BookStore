using MediatR;

namespace BookStore.Application.Features.Book.Commands.Buy
{
     public record BuyBookCommand(Guid BookId, string UserEmail) :IRequest<bool>
     {
          public Guid BookId = BookId;
          public string UserEmail = UserEmail;
     }
     public class BuyBookCommandHandler:IRequestHandler<BuyBookCommand,bool>
     {
          public Task<bool> Handle(BuyBookCommand request, CancellationToken cancellationToken)
          {
               throw new NotImplementedException();
          }
     }
}
