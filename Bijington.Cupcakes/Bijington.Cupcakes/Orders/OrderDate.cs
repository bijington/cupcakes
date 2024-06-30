namespace Bijington.Cupcakes.Orders;

public class OrderDate : List<Order>
{
    public DateOnly Date { get; }
    
    public decimal Total { get; }

    public OrderDate(DateOnly date, IList<Order> orders) : base(orders)
    {
        Date = date;
        Total = orders.Select(o => o.TotalPrice).Sum();
    }
}