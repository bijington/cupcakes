using Bijington.Cupcakes.Products.ViewModels;

namespace Bijington.Cupcakes.Products.Pages;

public partial class ProductsPage : ContentPage
{
    public ProductsPage(ProductsPageViewModel productsPageViewModel)
    {
        InitializeComponent();
        _productsPageViewModel = productsPageViewModel;
        BindingContext = productsPageViewModel;
    }

    private readonly ProductsPageViewModel _productsPageViewModel;

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        
        _productsPageViewModel.OnNavigatedTo();
    }
}