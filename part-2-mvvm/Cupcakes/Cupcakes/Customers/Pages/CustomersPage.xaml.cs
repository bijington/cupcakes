using Cupcakes.Customers.ViewModels;

namespace Cupcakes.Customers.Pages;

public partial class CustomersPage : ContentPage
{
    public CustomersPage(CustomersPageViewModel customersPageViewModel)
    {
        InitializeComponent();
        BindingContext = customersPageViewModel;
        _customersPageViewModel = customersPageViewModel;
    }

    private readonly CustomersPageViewModel _customersPageViewModel;

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _customersPageViewModel.OnNavigatedTo();
    }
}