using System.Numerics;
using Bijington.Cupcakes.Products;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bijington.Cupcakes.Orders.ViewModels;

public partial class OrderItemViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPrice))]
    private Product _product;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPrice))]
    private int _quantity;

    public decimal TotalPrice => (_product?.Price ?? 0M) * Quantity;
}