using Bijington.Cupcakes.Products;

namespace Bijington.Cupcakes.Orders;

public class ProductOrder
{
    public Product Product { get; set; }
    
    public int Quantity { get; set; }
}