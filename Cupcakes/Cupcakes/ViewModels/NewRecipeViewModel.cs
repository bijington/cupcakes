using System.Threading.Tasks;
using Cupcakes.Models;
using Cupcakes.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Cupcakes.ViewModels
{
    public class NewRecipeViewModel : BaseViewModel
    {
        private string description;
        private int makesQuantity;
        private string name;
        private Recipe recipe;

        public NewRecipeViewModel()
        {
            SaveCommand = new AsyncCommand(() => OnSave());//, (o) => ValidateSave(o));
            CancelCommand = new AsyncCommand(() => OnCancel());
            AddIngredientCommand = new MvvmHelpers.Commands.Command(() => OnAddIngredient());
            this.PropertyChanged +=
                (_, __) => SaveCommand.CanExecute(null);

            this.Recipe = new Recipe();
            this.OnAddIngredient();
        }

        public IDataStore<Recipe> DataStore => DependencyService.Get<IDataStore<Recipe>>();

        public Recipe Recipe
        {
            get => recipe;
            set => SetProperty(ref recipe, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int MakesQuantity
        {
            get => makesQuantity;
            set => SetProperty(ref makesQuantity, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public MvvmHelpers.Commands.Command AddIngredientCommand { get; }
        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }

        private void OnAddIngredient()
        {
            this.Recipe.Ingredients.Add(new Ingredient());
        }

        private async Task OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnSave()
        {
            var newRecipe = new Recipe()
            {
                Name = Name,
                Description = Description,
                MakesQuantity = makesQuantity
            };

            await DataStore.AddItemAsync(Recipe);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave(object o)
        {
            return !string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(description);
        }
    }
}
