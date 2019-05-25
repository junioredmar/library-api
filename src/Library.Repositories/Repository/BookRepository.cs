using Library.Core.Contracts;
using Library.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository.Repository
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public async Task<IQueryable<Book>> GetAll()
        {
            return await Task.FromResult(Books);
        }

        public async Task<int> Create(Book book)
        {
            var id = Add(book);
            return await Task.FromResult(id);
        }

        public Task Update(int id, Book book)
        {
            FindAndUpdate(id, book);
            return Task.FromResult(true);
        }
    }
}
