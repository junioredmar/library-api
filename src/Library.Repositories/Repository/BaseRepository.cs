using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Repository.Repository
{
    // Creates mocked Database
    public abstract class BaseRepository
    {
        protected IQueryable<Book> Books;

        protected BaseRepository()
        {
            SetupRepository();
        }

        // Simulates a DB Population
        private void SetupRepository()
        {
            Books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Isbn = "0525633758",
                    Authors = new List<string>{"Michelle Obama"},
                    Title = "Becoming",
                    Genre = "Biography",
                    IsBorrowed = false
                },
                new Book
                {
                    Id = 2,
                    Isbn = "838008215x",
                    Authors = new List<string>{"J.k. Rowling"},
                    Title = "Harry Potter 3",
                    Genre = "Fiction",
                    IsBorrowed = true,
                    BorrowedBy = "Edmar Junior",
                    Comment = "He said he will return it to me without any scratches",
                    ReturningDate = DateTime.UtcNow.AddDays(5)
                },
                new Book
                {
                    Id = 3,
                    Isbn = "9780345538",
                    Authors = new List<string>{"J. R. R. Tolkien"},
                    Title = "The Lord of the Rings",
                    Genre = "Fiction",
                    IsBorrowed = false
                },
                new Book
                {
                    Id = 4,
                    Isbn = "838845915p",
                    Authors = new List<string>{"Stephen King"},
                    Title = "The Institute: A Novel",
                    Genre = "Romance",
                    IsBorrowed = true,
                    BorrowedBy = "John Smith",
                    Comment = "He is my friend",
                    ReturningDate = DateTime.UtcNow.AddDays(5)
                },
                new Book
                {
                    Id = 5,
                    Isbn = "1250104211",
                    Authors = new List<string>{"Watt Key"},
                    Title = "Terror at Bottle Creek",
                    Genre = "Terror",
                    IsBorrowed = false
                },
            }.AsQueryable();
        }

        // Simulates a DB Insertion
        protected int Add(Book book)
        {
            var id = Books.Last().Id + 1;
            var updatedBooks = Books.AsEnumerable().Concat(new List<Book> { book });
            Books = updatedBooks.AsQueryable();
            return id;
        }

        // Simulates a DB Update
        protected void FindAndUpdate(int id, Book book)
        {
            var dbBooks = Books.AsEnumerable();
            var dbBook = dbBooks.FirstOrDefault(b => b.Id == id);
            dbBook = book;
        }
    }
}
