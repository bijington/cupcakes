using Cupcakes.Customers.ViewModels;

namespace Cupcakes.Customers.Pages;

public partial class AddCustomerPage : ContentPage
{
    public AddCustomerPage(AddCustomerPageViewModel addCustomerPageViewModel)
    {
        InitializeComponent();
        BindingContext = addCustomerPageViewModel;
    }
}