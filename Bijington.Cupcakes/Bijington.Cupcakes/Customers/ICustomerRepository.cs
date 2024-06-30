namespace Bijington.Cupcakes.Customers;

public interface ICustomerRepository
{
    Task<IReadOnlyCollection<Customer>> GetCustomers();
    
    Task Save(Customer order);
}