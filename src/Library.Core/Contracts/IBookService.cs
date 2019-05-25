using Library.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();

        Task<Book> GetById(int id);

        Task<int> Create(Book book);

        Task Update(int id, Book book);

        Task<IEnumerable<Book>> Search(string query);
    }
}
