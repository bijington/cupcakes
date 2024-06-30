using Bijington.Cupcakes.Customers.ViewModels;

namespace Bijington.Cupcakes.Customers.Pages;

public partial class CustomersPage : ContentPage
{
    public CustomersPage(CustomersPageViewModel customersPageViewModel)
    {
        InitializeComponent();
        _customersPageViewModel = customersPageViewModel;
        BindingContext = customersPageViewModel;
    }
    
    private readonly CustomersPageViewModel _customersPageViewModel;

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        
        _customersPageViewModel.OnNavigatedTo();
    }
}