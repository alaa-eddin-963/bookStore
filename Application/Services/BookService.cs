using Microsoft.EntityFrameworkCore;
using Interfaces;
using Data;
using Models;

namespace Services
{
    
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAll() => await Task.FromResult(_context.Books.ToList());

        public async Task<Book?> GetById(Guid id) => await Task.FromResult(_context.Books.FirstOrDefault(b => b.Id == id));

        public async Task<Book> Add(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> Update(Guid id, Book updatedBook)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return null;

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.Price = updatedBook.Price;
            book.IsAvailable = updatedBook.IsAvailable;
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> Delete(Guid id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Book>> SearchBooks(string? title, string? author, string? genre)
        {
            IQueryable<Book> query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.Author.Contains(author));

            if (!string.IsNullOrWhiteSpace(genre))
                query = query.Where(b => b.Genre.Contains(genre));

            return await query.ToListAsync();
        }
    }
}

