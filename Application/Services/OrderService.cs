using Interfaces;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> Checkout(Guid userId, string shippingAddress)
        {
            // Load cart items with related books
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (cartItems.Count == 0)
                throw new Exception("Cart is empty");

            // Calculate total and prepare order items
            decimal total = 0m;
            var orderItems = new List<OrderItem>();

            foreach (var cartItem in cartItems)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == cartItem.BookId);

                if (book == null || !book.IsAvailable)
                    throw new Exception($"Book with ID {cartItem.BookId} is not available");

                var itemTotalPrice = cartItem.Quantity * book.Price;
                total += itemTotalPrice;

                // Optionally update book availability or stock here

                orderItems.Add(new OrderItem
                {
                    BookId = book.Id,
                    Quantity = cartItem.Quantity,
                    Price = book.Price
                });
            }

            var order = new Order
            {
                UserId = userId,
                ShippingAddress = shippingAddress,
                TotalAmount = total,
                CreatedAt = DateTime.UtcNow,
                Items = orderItems
            };

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Orders.Add(order);

                // Clear user's cart
                _context.CartItems.RemoveRange(cartItems);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return order;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
