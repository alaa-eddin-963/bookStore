using Models;
using System;
using System.Threading.Tasks;


namespace Interfaces
{
    public interface IOrderService
    {
        Task<Order> Checkout(Guid userId, string shippingAddress);
    }
}
