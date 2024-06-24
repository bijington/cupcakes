using System.Collections.Immutable;

namespace Bijington.Cupcakes.Products;

public class ProductRepository : IProductRepository
{
    private readonly IList<Product> _products = new List<Product>();
    
    public ProductRepository()
    {
        for (var i = 1; i < 2; i++)
        { 
            _products.Add(new Product { Id = i, Name = "Rainbow Cake", Price = 12.50M, ImagePath = "https://www.thecomfortofcooking.com/wp-content/uploads/2020/06/Easy_Rainbow_Cake-3-scaled.jpg" });   
        }
    }
    
    public Task<IReadOnlyCollection<Product>> GetProducts()
    {
        return Task.FromResult<IReadOnlyCollection<Product>>(_products.ToImmutableList());
    }

    public Task Save(Product product)
    {
        _products.Add(product);
        
        product.Id = _products.Count;

        return Task.CompletedTask;
    }
}