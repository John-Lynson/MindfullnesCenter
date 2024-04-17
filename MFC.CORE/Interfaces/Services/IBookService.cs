using MFC.CORE.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFC.CORE.Interfaces.Services
{
    public interface IBookService
    {
        Task<BookDto> GetBookByIdAsync(int id);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> AddBookAsync(BookDto book);
        Task UpdateBookAsync(BookDto book);
        Task DeleteBookAsync(int id);
    }
}

