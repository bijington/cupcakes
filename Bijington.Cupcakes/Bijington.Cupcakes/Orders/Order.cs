using System;
using System.Collections.Generic;
using Bijington.Cupcakes.Products;

namespace Bijington.Cupcakes.Orders;

public class Order
{
    public DateOnly Date { get; set; }
    
    public TimeOnly Time { get; set; }
    
    public IList<Product> Products { get; set; }
}