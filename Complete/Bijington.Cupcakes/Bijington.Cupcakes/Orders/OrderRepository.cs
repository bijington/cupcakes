using System.Collections.Immutable;

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
    
    public Task Save(Order order)
    {
        _orders.Add(order);
        
        order.Id = _orders.Count;

        return Task.CompletedTask;
    }
}