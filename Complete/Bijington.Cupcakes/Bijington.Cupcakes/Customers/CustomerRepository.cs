using System.Collections.Immutable;
using SQLite;

namespace Bijington.Cupcakes.Customers;

public class CustomerRepository : ICustomerRepository
{
    private readonly SQLiteAsyncConnection _connection;
    
    public CustomerRepository(IFileSystem fileSystem)
    {
        var dbPath = Path.Combine(fileSystem.AppDataDirectory, "cupcakes_sqlite.db");
    
        _connection = new SQLiteAsyncConnection(dbPath);
        _connection.CreateTableAsync<Customer>();
    }
    
    public async Task<IReadOnlyCollection<Customer>> GetCustomers()
    {
        return await _connection.Table<Customer>().ToListAsync();
    }
    
    public Task Save(Customer customer)
    {
        return _connection.InsertAsync(customer);
    }
}