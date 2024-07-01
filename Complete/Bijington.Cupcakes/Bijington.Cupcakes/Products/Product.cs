namespace Bijington.Cupcakes.Products;

public class Product
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; init; }
    
    public string ImagePath { get; set; }
}