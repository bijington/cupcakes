using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Bijington.Cupcakes.Customers;
using Bijington.Cupcakes.Products;
using Bijington.Cupcakes.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace Bijington.Cupcakes.Orders.ViewModels;

public partial class AddOrderPageViewModel : ObservableObject, IQueryAttributable
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISettingsRepository _settingsRepository;
    private readonly IOrderRepository _orderRepository;

    public AddOrderPageViewModel(
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        ISettingsRepository settingsRepository,
        IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _settingsRepository = settingsRepository;
        _orderRepository = orderRepository;
    }
    
    [ObservableProperty]
    private string _currency;

    [ObservableProperty] 
    private Customer _customer;
    
    [ObservableProperty]
    private decimal _totalPrice;

    [ObservableProperty]
    private DateTime _orderDate = DateTime.Today;

    // TODO: Explain why we can use a list here.
    [ObservableProperty]
    private IReadOnlyCollection<Customer> _availableCustomers;
    
    [ObservableProperty]
    private IReadOnlyCollection<Product> _availableProducts;
    
    public ObservableCollection<OrderItemViewModel> Items { get; } = [];

    [RelayCommand]
    private void OnAddItem()
    {
        var item = new OrderItemViewModel();
        item.PropertyChanged += ItemOnPropertyChanged;
        Items.Add(item);
    }

    private void ItemOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        TotalPrice = Items.Select(i => (i.Product?.Price ?? 0M) * i.Quantity).Sum();
    }

    [RelayCommand]
    private async Task OnSave()
    {
        var order = new Order
        {
            Customer = Customer,
            Date = OrderDate,
            Items = Items.Select(i => new ProductOrder { Product = i.Product, Quantity = i.Quantity }).ToList()
        };

        await _orderRepository.Save(order);

        foreach (var item in Items)
        {
            item.PropertyChanged -= ItemOnPropertyChanged;
        }

        await Shell.Current.GoToAsync("..");
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Currency = _settingsRepository.Currency;
        AvailableProducts = await _productRepository.GetProducts();
        AvailableCustomers = await _customerRepository.GetCustomers();
    }
}