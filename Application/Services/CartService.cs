using Interfaces;
using Data;
using Models;

namespace Services
{
    
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItem>> GetCart(Guid userId) =>
            await Task.FromResult(_context.CartItems.Where(c => c.UserId == userId).ToList());

        public async Task AddToCart(Guid userId, Guid bookId, int quantity)
        {
            var existing = _context.CartItems.FirstOrDefault(c => c.UserId == userId && c.BookId == bookId);
            if (existing != null) existing.Quantity += quantity;
            else _context.CartItems.Add(new CartItem { UserId = userId, BookId = bookId, Quantity = quantity });

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCart(Guid userId, Guid bookId)
        {
            var item = _context.CartItems.FirstOrDefault(c => c.UserId == userId && c.BookId == bookId);
            if (item != null) _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task ClearCart(Guid userId)
        {
            var items = _context.CartItems.Where(c => c.UserId == userId);
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}

