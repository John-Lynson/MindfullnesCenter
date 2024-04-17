using MFC.CORE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFC.CORE.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
