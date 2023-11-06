namespace BookStore.Application.Exceptions
{
     internal class DataBaseErrorException :Exception
     {
          public DataBaseErrorException() : base("DataBase error")
          {

          }
          public DataBaseErrorException(string message) : base(message)
          {
          }
     }
}
