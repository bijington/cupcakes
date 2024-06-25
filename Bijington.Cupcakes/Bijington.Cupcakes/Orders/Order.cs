using System;
using System.Collections.Generic;

namespace Bijington.Cupcakes.Orders;

public class Order
{
    public string CustomerName { get; set; }
    
    public DateTime Date { get; set; }
    
    public int Id { get; set; }
    
    // TODO: Add in TotalPrice here. Explain why breaking normalisation can be a good thing.
    
    public IList<ProductOrder> Items { get; set; }
}