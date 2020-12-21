namespace Cupcakes.Models
{
    public class Ingredient
    {
        public bool IsAtomic { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public string Unit { get; set; }
    }
}
