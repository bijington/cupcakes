using System;
using System.ComponentModel;
using Cupcakes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cupcakes.Views
{
    public partial class RecipeListPage : ContentPage
    {
        public RecipeListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ((RecipeListViewModel)this.BindingContext).OnAppearing();
        }
    }
}