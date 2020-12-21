using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cupcakes.Models;
using Cupcakes.Services;
using Cupcakes.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Cupcakes.ViewModels
{
    public class RecipeListViewModel : BaseViewModel
    {
        public RecipeListViewModel()
        {
            Title = "Recipes";
            Recipes = new ObservableCollection<Recipe>();

            AddRecipeCommand = new AsyncCommand(() => OnAddRecipe());
            RecipeTappedCommand = new AsyncCommand<Recipe>((r) => OnRecipeTapped(r));
        }

        public IDataStore<Recipe> DataStore => DependencyService.Get<IDataStore<Recipe>>();

        public AsyncCommand AddRecipeCommand { get; }

        public AsyncCommand<Recipe> RecipeTappedCommand { get; }

        public ObservableCollection<Recipe> Recipes { get; }

        private Task OnAddRecipe()
        {
            return Shell.Current.GoToAsync(nameof(NewRecipePage));
        }

        private Task OnRecipeTapped(Recipe recipe)
        {
            return Shell.Current.GoToAsync(nameof(NewRecipePage));
        }

        public async Task OnAppearing()
        {
            this.Recipes.Clear();
            var recipes = await DataStore.GetItemsAsync();

            foreach (var recipe in recipes)
            {
                this.Recipes.Add(recipe);
            }
        }
    }
}