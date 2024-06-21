using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

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
    async Task OnAddProduct()
    {
        await Shell.Current.GoToAsync(RouteNames.AddProduct);
    }

    async Task LoadProducts()
    {
        var products = await _productRepository.GetProducts();

        foreach (var product in products)
        {
            Products.Add(product);
        }
    }
}