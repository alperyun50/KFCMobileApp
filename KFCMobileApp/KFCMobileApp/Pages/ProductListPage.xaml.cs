using KFCMobileApp.Models;
using KFCMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFCMobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        public ObservableCollection<ProductByCategory> ProductByCategoryCollection;

        public ProductListPage(int categoryId, string categoryName)
        {
            InitializeComponent();
            LblCategoryName.Text = categoryName;
            ProductByCategoryCollection = new ObservableCollection<ProductByCategory>();
            GetProducts(categoryId);
        }

        private async void GetProducts(int id)
        {
            var products = await ApiService.GetProductByCategoty(id);
            foreach(var product in products)
            {
                ProductByCategoryCollection.Add(product);
            }
            CvProducts.ItemsSource = ProductByCategoryCollection;
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            // backarrow back tap action 
            Navigation.PopModalAsync();
        }

        private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as ProductByCategory;
            if (currentSelection == null) return;
            Navigation.PushModalAsync(new ProductDetailPage(currentSelection.id));
            // so we can choose the same product again
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}