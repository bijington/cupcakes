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
        private string name;

        public NewRecipeViewModel()
        {
            SaveCommand = new AsyncCommand(() => OnSave());//, (o) => ValidateSave(o));
            CancelCommand = new AsyncCommand(() => OnCancel());
            this.PropertyChanged +=
                (_, __) => SaveCommand.CanExecute(null);
        }

        public IDataStore<Recipe> DataStore => DependencyService.Get<IDataStore<Recipe>>();

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }

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
                Description = Description
            };

            await DataStore.AddItemAsync(newRecipe);

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
