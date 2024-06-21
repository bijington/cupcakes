using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Bijington.Cupcakes.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly IList<Order> _orders = new List<Order>();
    
    public OrderRepository()
    {
        
    }
    
    public Task<IReadOnlyCollection<Order>> GetOrders()
    {
        return Task.FromResult<IReadOnlyCollection<Order>>(_orders.ToImmutableList());
    }
}