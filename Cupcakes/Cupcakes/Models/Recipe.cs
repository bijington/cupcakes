using System.Collections.ObjectModel;
using SQLite;

namespace Cupcakes.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Decorations = new ObservableCollection<Ingredient>();
            Ingredients = new ObservableCollection<Ingredient>();
        }

        public string Description { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int MakesQuantity { get; set; }

        public string Name { get; set; }

        public ObservableCollection<Ingredient> Decorations { get; }

        public ObservableCollection<Ingredient> Ingredients { get; }
    }
}
