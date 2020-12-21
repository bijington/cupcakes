using Cupcakes.Views;
using Xamarin.Forms;

namespace Cupcakes
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewRecipePage), typeof(NewRecipePage));
        }

    }
}
