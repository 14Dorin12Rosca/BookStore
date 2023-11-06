namespace BookStore.Application.Exceptions
{
     internal class NotFoundException : Exception
     {
          public NotFoundException() :base("The Requested resources were not found")
          {
               
          }
          public NotFoundException(string message) : base(message)
          {
          }
     }
}
