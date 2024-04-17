using Microsoft.AspNetCore.Mvc;
using MFC.CORE.DTOs;
using MFC.DAL.Services;
using MFC.CORE.Interfaces;
using System.Threading.Tasks;
using MFC.CORE.Interfaces.Services;
namespace MFC.WEB3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _booksService.GetAllBooks();
            return Ok(books);
        }
    }
}
