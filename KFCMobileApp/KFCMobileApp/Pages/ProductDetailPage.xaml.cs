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
    public partial class ProductDetailPage : ContentPage
    {
        private int _productId;
        
        public ProductDetailPage(int productId)
        {
            InitializeComponent();
            GetProductDetails(productId);
            _productId = productId;
        }

        private async void GetProductDetails(int productId)
        {
            var product = await ApiService.GetProductById(productId);
            LblName.Text = product.name;
            LblDetail.Text = product.detail;
            ImgProduct.Source = product.FullImageUrl;
            LblPrice.Text = product.price.ToString();
            LblTotalPrice.Text = LblPrice.Text;
        }

        private void TapIncrement_Tapped(object sender, EventArgs e)
        {
            var i = Convert.ToInt16(LblQty.Text);
            i++;
            LblQty.Text = i.ToString();
            LblTotalPrice.Text = (Convert.ToInt16(LblQty.Text) * Convert.ToInt16(LblPrice.Text)).ToString();
        }

        private void TapDecrement_Tapped(object sender, EventArgs e)
        {
            var i = Convert.ToInt32(LblQty.Text);
            i--;
            if(i < 1)
            {
                // disabled TapDecrement_Tapped method
                return;
            }
            LblQty.Text = i.ToString();
            LblTotalPrice.Text = (Convert.ToInt16(LblQty.Text) * Convert.ToInt16(LblPrice.Text)).ToString();
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void BtnAddToCart_Clicked(object sender, EventArgs e)
        {
            var addToCart = new AddToCart();
            addToCart.qty = LblQty.Text;
            addToCart.price = LblPrice.Text;
            addToCart.total_amount = LblTotalPrice.Text;
            addToCart.product_id = _productId;
            addToCart.customer_id = Preferences.Get("userid", 0);
 
            var response = await ApiService.AddItemsInCart(addToCart);
            if(response)
            {
                await DisplayAlert("", "Seçtiğiniz ürün(ler) sepete eklendi", "Tammam");
            }
            else
            {
                await DisplayAlert("Eyvah!..", "Bişeyler ters gitti morukcum!..", "Çıkış");
            }
        }
    }
}