using KFCMobileApp.Models;
using KFCMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFCMobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<PopularProduct> ProductCollection;
        public ObservableCollection<Category> CategoriesCollection;

        public HomePage()
        {
            InitializeComponent();
            ProductCollection = new ObservableCollection<PopularProduct>();
            CategoriesCollection = new ObservableCollection<Category>();
            GetPopularProducts();
            GetCategories();

            LblUserName.Text = Preferences.Get("username", string.Empty);
        }

        private async void GetCategories()
        {
            var categories = await ApiService.GetCategories();
            foreach(var category in categories)
            {
                CategoriesCollection.Add(category);
            }
            CvCategories.ItemsSource = CategoriesCollection;
        }

        private async void GetPopularProducts()
        {
            var products = await ApiService.GetPopularProducts();
            foreach(var product in products)
            {
                ProductCollection.Add(product);
            }
            CvProducts.ItemsSource = ProductCollection;
        }

        private async void ImgMenu_Tapped(object sender, EventArgs e)
        {
            GridOverlay.IsVisible = true;
            // animation for menu tap
            await SlMenu.TranslateTo(0, 0, 400, Easing.Linear);
        }

        private async void TapCloseMenu_Tapped(object sender, EventArgs e)
        {
            await SlMenu.TranslateTo(-250, 0, 400, Easing.Linear);
            GridOverlay.IsVisible = false;
        }

        // execute constructor more than once
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var response = await ApiService.GetTotalCartItems(Preferences.Get("userid", 0));
            LblTotalItems.Text = response.total_items.ToString();
        }

        private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentselection = e.CurrentSelection.FirstOrDefault() as Category;
            // just check if current selection is already null then dont execute any code
            if (currentselection == null) return;
            Navigation.PushModalAsync(new ProductListPage(currentselection.id, currentselection.name));
            //selecteditem highligt locking error solved
            ((CollectionView)sender).SelectedItem = null;
        }

        private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as PopularProduct;
            if (currentSelection == null) return;
            Navigation.PushModalAsync(new ProductDetailPage(currentSelection.id));
            // so we can choose the same product again
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}