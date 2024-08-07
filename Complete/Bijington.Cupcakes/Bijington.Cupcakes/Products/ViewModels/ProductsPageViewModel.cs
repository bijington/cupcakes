using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bijington.Cupcakes.Products.ViewModels;

public partial class ProductsPageViewModel : ObservableObject
{
    private readonly IProductRepository _productRepository;

    public ProductsPageViewModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public ObservableCollection<Product> Products { get; } = [];
    
    public void OnNavigatedTo()
    {
        Task.Run(async () => await LoadProducts());
    }

    [RelayCommand]
    private async Task OnAddProduct()
    {
        await Shell.Current.GoToAsync(RouteNames.AddProduct);
    }

    private async Task LoadProducts()
    {
        var products = await _productRepository.GetProducts();

        foreach (var product in products)
        {
            if (Products.Any(p => p.Id == product.Id))
            {
                continue;
            }
            
            Products.Add(product);
        }
    }
}