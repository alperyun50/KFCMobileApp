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
    public partial class CartPage : ContentPage
    {
        public ObservableCollection<ShoppingCartItem> ShoppingCartCollection;

        public CartPage()
        {
            InitializeComponent();
            ShoppingCartCollection = new ObservableCollection<ShoppingCartItem>();
            GetShoppingCartItems();
            GetTotalPrice();
        }

        private async void GetTotalPrice()
        {
            var totalPrice = await ApiService.GetCartSubTotal(Preferences.Get("userid", 0));
            LblTotalPrice.Text = totalPrice.subtotal.ToString();
        }

        private async void GetShoppingCartItems()
        {
            var shoppingCartItems = await ApiService.GetShoppingCartItems(Preferences.Get("userid", 0));
            foreach(var shoppingCart in shoppingCartItems)
            {
                ShoppingCartCollection.Add(shoppingCart);
            }

            LvShoppingCart.ItemsSource = ShoppingCartCollection;
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void TapClearCart_Tapped(object sender, EventArgs e)
        {
            var response = await ApiService.ClearShoppingCart(Preferences.Get("userid", 0));
            if(response)
            {
                await DisplayAlert("", "Sepetindeki ürünler silindi hafz..", "Ok");
                LvShoppingCart.ItemsSource = null;
                LblTotalPrice.Text = "0";
            }
            else
            {
                await DisplayAlert("", "Bişeyler ters gitti moruk!..", "Çıkış");
            }
        }

        private void BtnProceed_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PlaceOrderPage(Convert.ToDouble(LblTotalPrice.Text)));
        }
    }
}