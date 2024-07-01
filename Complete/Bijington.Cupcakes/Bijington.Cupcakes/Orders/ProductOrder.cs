using Bijington.Cupcakes.Products;

namespace Bijington.Cupcakes.Orders;

public class ProductOrder
{
    public Product Product { get; init; }
    
    public int Quantity { get; init; }
}