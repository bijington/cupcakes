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

namespace Bijington.Cupcakes.Orders.ViewModels;

public partial class AddOrderPageViewModel : ObservableObject, IQueryAttributable
{
    private readonly IProductRepository _productRepository;
    private readonly ISettingsRepository _settingsRepository;
    private readonly IOrderRepository _orderRepository;

    public AddOrderPageViewModel(
        IProductRepository productRepository,
        ISettingsRepository settingsRepository,
        IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _settingsRepository = settingsRepository;
        _orderRepository = orderRepository;
    }
    
    [ObservableProperty]
    private string _currency;

    [ObservableProperty] 
    private string _customerName;
    
    [ObservableProperty]
    private decimal _totalPrice;

    [ObservableProperty]
    private DateTime _orderDate = DateTime.Today;

    [ObservableProperty]
    private IReadOnlyCollection<Product> _availableProducts;
    
    public ObservableCollection<OrderItemViewModel> Items { get; } = [];

    [RelayCommand]
    void OnAddItem()
    {
        Items.Add(new OrderItemViewModel());
    }
    
    [RelayCommand]
    async Task OnSave()
    {
        var order = new Order
        {
            CustomerName = CustomerName,
            Date = OrderDate,
            Items = Items.Select(i => new ProductOrder { Product = i.Product, Quantity = i.Quantity }).ToList()
        };

        await _orderRepository.Save(order);

        await Shell.Current.GoToAsync("..");
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Currency = _settingsRepository.Currency;
        AvailableProducts = await _productRepository.GetProducts();
    }
}