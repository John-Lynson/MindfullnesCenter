using MFC.CORE.Models;
using MFC.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFC.DAL.Services
{
    public class BooksService : IBooksService
    {
        private readonly MFCContext _context;

        public BooksService(MFCContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }
    }
}
