namespace Bijington.Cupcakes.Products;

public interface IProductRepository
{
    Task<IReadOnlyCollection<Product>> GetProducts();

    Task Save(Product product);
}