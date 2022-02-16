using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BookWebApi.Tests
{
    public class BookControllerTests
    {
        BooksController _controller;
        IBookService _bookService;
        public BookControllerTests()
        {
            _bookService = new BookService();
            _controller = new BooksController(_bookService);
        }

        [Fact]
        public void GetAllTest()
        {
            //Act
            var result = _controller.Get();
            var actualResult = result.Result;
            //Assert
            Assert.IsType<OkObjectResult>(actualResult);

            var items = actualResult as OkObjectResult;
            Assert.IsType<List<Book>>(items.Value);

            var listItems = items.Value as List<Book>;
            Assert.Equal(5, listItems.Count);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200")]
        public void GetBookIdTest(Guid id)
        {
            //Act
            var result = _controller.Get(id);

            //Assert
            var actualResult = result.Result;
            Assert.IsType<OkObjectResult>(actualResult);

            var item = actualResult as OkObjectResult;
            Assert.IsType<Book>(item.Value);

            var book = item.Value as Book;
            Assert.Equal(id, book.Id);
            Assert.Equal("Managing Oneself", book.Title);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c111")]
        public void GetBookIdForFailureTest(Guid id)
        {
            //Act
            var result = _controller.Get(id);

            //Assert
            var actualResult = result.Result;
            Assert.IsType<NotFoundResult>(actualResult);
        }

        [Fact]
        public void AddBookTest()
        {
            //Arrange
            var entity = new Book()
            {
                Author = "Author",
                Title = "Title",
                Description = "Description",
            };

            //Act
            var result = _controller.Post(entity);

            //Assert
            Assert.IsType<CreatedAtActionResult>(result);

            var item = result as CreatedAtActionResult;
            Assert.IsType<Book>(item.Value);

            var BookItem = item.Value as Book;
            Assert.Equal(entity.Author, BookItem.Author);
            Assert.Equal(entity.Title, BookItem.Title);
            Assert.Equal(entity.Description, BookItem.Description);
        }

        [Fact]
        public void AddBookFailureTest()
        {
            //Arrange
            var entity = new Book() { Author = "Author", Description = "Description" };

            //Act
            _controller.ModelState.AddModelError("Title", "Title is a required filed");
            var result = _controller.Post(entity);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200")]
        public void RemoveBookByIdTest(Guid id)
        {
            //Act
            var result = _controller.Delete(id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(4, _bookService.GetAll().Count());
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c111")]
        public void RemoveBookByIdFailureTest(Guid id )
        {
            //Act
            var result = _controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }
}
