using Xamarin.Forms;
using Cupcakes.Services;

namespace Cupcakes
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();
            DependencyService.Register<RecipeDataStore>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
