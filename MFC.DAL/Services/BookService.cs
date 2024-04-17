using MFC.CORE.DTOs;
using MFC.CORE.Interfaces.Repositories;
using MFC.CORE.Interfaces.Services;
using MFC.CORE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFC.DAL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                PublishedYear = book.PublishedYear
            };
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                PublishedYear = b.PublishedYear
            });
        }

        public async Task<BookDto> AddBookAsync(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Description = bookDto.Description,
                PublishedYear = bookDto.PublishedYear
            };
            await _bookRepository.AddBookAsync(book);
            bookDto.Id = book.Id;
            return bookDto;
        }

        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookDto.Id);
            if (book != null)
            {
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.Description = bookDto.Description;
                book.PublishedYear = bookDto.PublishedYear;
                await _bookRepository.UpdateBookAsync(book);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }
    }
}
