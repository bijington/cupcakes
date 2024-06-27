namespace Bijington.Cupcakes.Customers;

public class Customer
{
    public string Address { get; init; } = string.Empty;
    
    public string Name { get; init; } = string.Empty;
    
    public string PhoneNumber { get; init; } = string.Empty;
    
    public int Id { get; set; }
}