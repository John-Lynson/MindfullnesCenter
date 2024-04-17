using Microsoft.AspNetCore.Mvc;
using MFC.CORE.DTOs;
using MFC.DAL.Services;
using MFC.CORE.Interfaces.Services;
using System.Threading.Tasks;

namespace MFC.WEB3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _booksService;

        public BooksController(IBookService booksService)
        {
            _booksService = booksService;
        }

        // Haalt alle boeken op
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _booksService.GetAllBooksAsync();
            return Ok(books);
        }

        // Haalt een specifiek boek op basis van ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _booksService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
    }
}
