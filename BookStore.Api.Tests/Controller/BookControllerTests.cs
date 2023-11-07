using BookStore.Api.Book;
using BookStore.Application.Features.Book;
using BookStore.Application.Features.Book.Commands.Add;
using BookStore.Application.Features.Book.Queries;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Tests.Controller
{
     public class BookControllerTests
     {
          private readonly IMediator _mediator;

          public BookControllerTests()
          {
               _mediator = A.Fake<IMediator>();
          }

          [Fact]
          public async Task BookController_Get_ReturnsOk()
          {
               // Arrange
               var books = new List<BookDto>
               {
                    new BookDto { Id = Guid.NewGuid(), Title = "Book 1" },
                    new BookDto { Id = Guid.NewGuid(), Title = "Book 2" }
               };

               A.CallTo(() => _mediator.Send(A<GetBooksQuery>._, default)).Returns(books);

               var controller = new BookController(_mediator);

               // Act
               var result = await controller.Get();

               // Assert
               var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
               var model = okResult.Value.Should().BeOfType<List<BookDto>>().Subject;

               model.Should().NotBeNull();
               model.Should().BeEquivalentTo(books);
          }

          [Fact]
          public async Task BookController_Create_ReturnsOk()
          {
               // Arrange
               var request = new CreateBookRequest
               {    
                    Title = "New Book",
               };

               var bookDto = new BookDto
               {
                    Id = Guid.NewGuid(),
                    Title = request.Title
               };

               A.CallTo(() => _mediator.Send(A<AddBookCommand>._, default)).Returns(bookDto);

               var controller = new BookController(_mediator);

               // Act
               var result = await controller.Create(request);

               // Assert
               var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
               var model = okResult.Value.Should().BeOfType<BookDto>().Subject;

               model.Should().NotBeNull();
               model.Should().BeEquivalentTo(bookDto);
          }
     }
}