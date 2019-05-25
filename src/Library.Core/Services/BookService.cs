using Library.Core.Contracts;
using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var books = await _repository.GetAll();
            return books.ToList();
        }

        public async Task<Book> GetById(int id)
        {
            var books = await _repository.GetAll();
            if (!books.Any(b => b.Id == id))
            {
                throw new IndexOutOfRangeException(nameof(id));
            }

            return books.FirstOrDefault(b => b.Id == id);
        }

        public async Task<int> Create(Book book)
        {
            var books = await _repository.GetAll();
            if (books.Any(b => b.Isbn == book.Isbn))
            {
                throw new ArrayTypeMismatchException(nameof(book));
            }

            var id = await _repository.Create(book);
            return id;
        }

        public async Task Update(int id, Book book)
        {
            var books = await _repository.GetAll();
            if (!books.Any(b => b.Id == id))
            {
                throw new IndexOutOfRangeException(nameof(book));
            }

            await _repository.Update(id, book);
        }

        /// <summary>
        /// Search the book shelf by Authors, Title, Genre, ISBN
        /// </summary>
        /// <param name="query">query to be searched by</param>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> Search(string query)
        {
            query = query ?? string.Empty;
            var books = await _repository.GetAll();
            var queriedBooks = books.Where(b => b.Title.ToLower().Contains(query.ToLower()) ||
                                    b.Genre.ToLower().Contains(query.ToLower()) ||
                                    b.Isbn.ToLower().Contains(query.ToLower()) ||
                                    b.Authors.Any(a => a.ToLower().Contains(query.ToLower()))).ToList();
            if (queriedBooks == null || !queriedBooks.Any())
            {
                throw new IndexOutOfRangeException(nameof(query));
            }

            return queriedBooks;
        }
    }
}
