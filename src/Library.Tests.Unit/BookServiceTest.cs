using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Library.Core.Contracts;
using Library.Core.Entities;
using Library.Core.Services;
using Xunit;

namespace Library.Tests.Unit
{
    public class BookServiceTest
    {
        private readonly IQueryable<Book> _mockedBooks;

        private readonly Mock<IBookRepository> _bookRepository;

        public BookServiceTest()
        {
            // Arrange
            _mockedBooks = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Isbn = "9780345538",
                    Authors = new List<string>{"J. R. R. Tolkien"},
                    Title = "The Lord of the Rings",
                    Genre = "Fiction",
                    IsBorrowed = false
                },
                new Book
                {
                    Id = 2,
                    Isbn = "838845915p",
                    Authors = new List<string>{"Stephen King"},
                    Title = "The Institute: A Novel",
                    Genre = "Romance",
                    IsBorrowed = true,
                    BorrowedBy = "John Smith",
                    Comment = "He is my friend",
                    ReturningDate = DateTime.UtcNow.AddDays(5)
                }
            }.AsQueryable();

            _bookRepository = new Mock<IBookRepository>();
        }

        [Fact]
        public async Task GetAll_Should_Return_All_Books()
        { 
            _bookRepository.Setup(b => b.GetAll()).ReturnsAsync(_mockedBooks);
            var bookService = new BookService(_bookRepository.Object);

            // Act
            var books = await bookService.GetAll();

            // Assert
            books.Should().NotBeNull();
            books.Should().BeEquivalentTo(_mockedBooks);
        }

        [Fact]
        public async Task GetAll_Should_Return_Empty()
        {
            // Arrange
            var emptyBookList = new List<Book>().AsQueryable();
            _bookRepository.Setup(b => b.GetAll()).ReturnsAsync(emptyBookList);
            var bookService = new BookService(_bookRepository.Object);

            // Act
            var books = await bookService.GetAll();

            // Assert
            books.Should().NotBeNull();
            books.Should().BeEquivalentTo(emptyBookList);
        }

        [Fact]
        public async Task GetById_Should_Return_Book()
        {
            _bookRepository.Setup(b => b.GetAll()).ReturnsAsync(_mockedBooks);
            var bookService = new BookService(_bookRepository.Object);

            // Act
            var book = await bookService.GetById(1);

            // Assert
            book.Should().NotBeNull();
            book.Should().BeEquivalentTo(_mockedBooks.First());
        }

        [Fact]
        public async Task GetById_Should_Return_Not_Found_When_Non_Existent_Id()
        {
            _bookRepository.Setup(b => b.GetAll()).ReturnsAsync(_mockedBooks);
            var bookService = new BookService(_bookRepository.Object);

            try
            {
                // Act
                await bookService.GetById(999999);
            }
            catch (Exception e)
            {
                // Assert
                e.Should().BeOfType<IndexOutOfRangeException>();
            }
        }
    }
}
