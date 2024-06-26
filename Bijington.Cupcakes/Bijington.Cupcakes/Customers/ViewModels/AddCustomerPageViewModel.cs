using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Bijington.Cupcakes.Products;
using Bijington.Cupcakes.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace Bijington.Cupcakes.Customers.ViewModels;

public partial class AddCustomerPageViewModel : ObservableObject
{
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerPageViewModel(
        ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [ObservableProperty] 
    private string _customerName;
    
    [RelayCommand]
    async Task OnSave()
    {
        var customer = new Customer
        {
            Name = CustomerName
        };

        await _customerRepository.Save(customer);

        await Shell.Current.GoToAsync("..");
    }
}