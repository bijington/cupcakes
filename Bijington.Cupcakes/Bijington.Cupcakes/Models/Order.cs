namespace Bijington.Cupcakes.Models;

public class Order
{
    public DateOnly Date { get; set; }
    
    public TimeOnly Time { get; set; }
    
    public IList<Product> Products { get; set; }
}