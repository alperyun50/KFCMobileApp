using KFCMobileApp.Models;
using KFCMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KFCMobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceOrderPage : ContentPage
    {
        private double _totalPrice;

        public PlaceOrderPage(double totalPrice)
        {
            InitializeComponent();
            _totalPrice = totalPrice;
        }

        private async void BtnPlaceOrder_Clicked(object sender, EventArgs e)
        {
            var order = new Order();
            order.full_name = EntName.Text;
            order.phone = EntPhone.Text;
            order.address = EntAddress.Text;
            order.user_id = Preferences.Get("userid", 0);
            order.order_total = _totalPrice;  // 65.error
            
            var response = await ApiService.PlaceOrder(order);
            if(response != null)
            {
                await DisplayAlert("", "Sipariş numaranız " + response.order_id, "ok");
                // order completed return homepage
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                await DisplayAlert("Eyvah!..", "Biyerde hata oluştu hacıt!..", "Çıkış");
            }
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            //previous page
            Navigation.PopModalAsync();
        }
    }
}