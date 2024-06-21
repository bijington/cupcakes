using System.Collections.ObjectModel;
using Bijington.Cupcakes.Models;
using Bijington.Cupcakes.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bijington.Cupcakes.ViewModels;

public partial class ProductsPageViewModel : ObservableObject
{
    public ProductsPageViewModel()
    {
        for (int i = 0; i < 10; i++)
        {
            Products.Add(new Product { Name = "Cake", ImagePath = "https://www.thecomfortofcooking.com/wp-content/uploads/2020/06/Easy_Rainbow_Cake-3-scaled.jpg" });   
        }
    }
    public ObservableCollection<Product> Products { get; } = 
    [
        
    ];

    [RelayCommand]
    void OnAddProduct()
    {
        _ = Shell.Current.GoToAsync(nameof(AddProductPage));
    }
}