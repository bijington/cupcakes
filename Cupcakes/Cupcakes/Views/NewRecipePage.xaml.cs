using Xamarin.Forms;

using Cupcakes.Models;
using Cupcakes.ViewModels;

namespace Cupcakes.Views
{
    public partial class NewRecipePage : ContentPage
    {
        public NewRecipePage()
        {
            InitializeComponent();
            BindingContext = new NewRecipeViewModel();
        }
    }
}