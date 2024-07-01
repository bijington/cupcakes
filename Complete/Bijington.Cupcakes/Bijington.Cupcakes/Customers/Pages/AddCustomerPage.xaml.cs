using Bijington.Cupcakes.Customers.ViewModels;

namespace Bijington.Cupcakes.Customers.Pages;

public partial class AddCustomerPage : ContentPage
{
    public AddCustomerPage(AddCustomerPageViewModel addCustomerPageViewModel)
    {
        InitializeComponent();
        BindingContext = addCustomerPageViewModel;
    }
}