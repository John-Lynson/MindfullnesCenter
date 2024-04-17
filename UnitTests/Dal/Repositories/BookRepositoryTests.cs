using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using MFC.CORE.Interfaces.Repositories;
using MFC.DAL.Repositories;
using MFC.DAL.Database;
using MFC.CORE.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests.Dal.Repositories
{
    public class BookRepositoryTests
    {
        private readonly MFCContext _context;
        private readonly BookRepository _repository;

        public BookRepositoryTests()
        {
            _context = CreateContext();
            _repository = new BookRepository(_context);
        }

        private MFCContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<MFCContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            var newContext = new MFCContext(options);
            SeedBooks(newContext);
            newContext.SaveChanges();
            return newContext;
        }

        private static void SeedBooks(MFCContext context)
        {
            context.Database.EnsureDeleted();  
            context.Database.EnsureCreated();  

            var books = new List<Book>
    {
        new Book { Id = 1, Title = "Book 1", Author = "Author A", Description = "Description A", PublishedYear = 2001 },
        new Book { Id = 2, Title = "Book 2", Author = "Author B", Description = "Description B", PublishedYear = 2002 },
        new Book { Id = 3, Title = "Book 3", Author = "Author C", Description = "Description C", PublishedYear = 2003 }
    };

            context.Books.AddRange(books);
            context.SaveChanges();
        }



        [Fact]
        public async Task GetAllBooksAsync_ReturnsAllBooks()
        {
            // Act
            var result = await _repository.GetAllBooksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetBookByIdAsync_ReturnsBook_WhenBookExists()
        {
            // Act
            var result = await _repository.GetBookByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Book 1", result.Title);
        }

        [Fact]
        public async Task GetBookByIdAsync_ReturnsNull_WhenBookDoesNotExist()
        {
            // Act
            var result = await _repository.GetBookByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateBookAsync_UpdatesExistingBook()
        {
            // Arrange
            var bookToUpdate = await _context.Books.FirstAsync();
            bookToUpdate.Title = "Updated Title";

            // Act
            await _repository.UpdateBookAsync(bookToUpdate);
            var updatedBook = await _context.Books.FindAsync(bookToUpdate.Id);

            // Assert
            Assert.Equal("Updated Title", updatedBook.Title);
        }

        [Fact]
        public async Task DeleteBookAsync_DeletesBook()
        {
            // Arrange
            var bookIdToDelete = 3;

            // Act
            await _repository.DeleteBookAsync(bookIdToDelete);
            var deletedBook = await _context.Books.FindAsync(bookIdToDelete);

            // Assert
            Assert.Null(deletedBook);
        }
    }
}
