using Cupcakes.Customers.ViewModels;

namespace Cupcakes.Customers.Pages;

public partial class CustomerDetailsPage : ContentPage
{
    public CustomerDetailsPage(CustomerDetailsPageViewModel customerDetailsPageViewModel)
    {
        InitializeComponent();
        BindingContext = customerDetailsPageViewModel;
    }
}