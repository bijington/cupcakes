namespace Bijington.Cupcakes.Orders;

public interface IOrderRepository
{
    Task<IReadOnlyCollection<Order>> GetOrders();
    
    Task Save(Order order);
}