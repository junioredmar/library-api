using Library.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Contracts
{
    public interface IBookRepository
    {
        Task<IQueryable<Book>> GetAll();

        Task<int> Create(Book book);

        Task Update(int id, Book book);
    }
}
