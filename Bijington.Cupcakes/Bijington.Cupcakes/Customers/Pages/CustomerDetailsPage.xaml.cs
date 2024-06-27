using Bijington.Cupcakes.Customers.ViewModels;

namespace Bijington.Cupcakes.Customers.Pages;

public partial class CustomerDetailsPage : ContentPage
{
    public CustomerDetailsPage(CustomerDetailsPageViewModel customerDetailsPageViewModel)
    {
        InitializeComponent();
        BindingContext = customerDetailsPageViewModel;
    }
}