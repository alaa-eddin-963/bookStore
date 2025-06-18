using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICartService
    {
        Task<List<CartItem>> GetCart(Guid userId);
        Task AddToCart(Guid userId, Guid bookId, int quantity);
        Task RemoveFromCart(Guid userId, Guid bookId);
        Task ClearCart(Guid userId);
    }

}
