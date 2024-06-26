using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Bijington.Cupcakes.Customers;

public class CustomerRepository : ICustomerRepository
{
    private readonly IList<Customer> _customers = new List<Customer>();
    
    public CustomerRepository()
    {
        _customers.Add(new Customer { Name = "Shaun" });
    }
    
    public Task<IReadOnlyCollection<Customer>> GetCustomers()
    {
        return Task.FromResult<IReadOnlyCollection<Customer>>(_customers.ToImmutableList());
    }
    
    public Task Save(Customer customer)
    {
        _customers.Add(customer);
        
        customer.Id = _customers.Count;

        return Task.CompletedTask;
    }
}