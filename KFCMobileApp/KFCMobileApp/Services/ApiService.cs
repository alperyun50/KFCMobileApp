using KFCMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnixTimeStamp;
using Xamarin.Essentials;

namespace KFCMobileApp.Services
{
    public static class ApiService
    {
        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password
            };

            // necessary object for restful api
            var httpclient = new HttpClient();
            // register object converted to json object
            var json = JsonConvert.SerializeObject(register);
            // supports for sending different language forms
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpclient.PostAsync(AppSettings.apiUrl + "api/Accounts/Register", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            {
                Email = email,
                Password = password
            };

            var httpclient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpclient.PostAsync(AppSettings.apiUrl + "api/Accounts/Login", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            // fetching access token
            var jsonresult = await response.Content.ReadAsStringAsync();
            // convert access token to csharp object
            var result = JsonConvert.DeserializeObject<Token>(jsonresult);
            Preferences.Set("accesstoken", result.access_token);
            Preferences.Set("userid", result.user_id);
            Preferences.Set("username", result.user_name);
            Preferences.Set("tokenExpirationTime", result.expiration_time);
            // hold current time with preferences
            Preferences.Set("currentTime", UnixTime.GetCurrentTime());

            return true;
        }

        public static async Task<List<Category>> GetCategories()
        {
            await TokenValidator.CheckTokenValidity();
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.GetStringAsync(AppSettings.apiUrl + "api/Categories");
            // convert response json data to csharp object
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public static async Task<Product> GetProductById(int productId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.GetStringAsync(AppSettings.apiUrl + "api/Products/" + productId);
            // convert response json data to csharp object
            return JsonConvert.DeserializeObject<Product>(response);
        }

        public static async Task<List<ProductByCategory>> GetProductByCategoty(int categoryId)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.GetStringAsync(AppSettings.apiUrl + "Products/ProductsByCategory/" + categoryId);
            // convert response json data to csharp object
            return JsonConvert.DeserializeObject<List<ProductByCategory>>(response);
        }

        public static async Task<List<PopularProduct>> GetPopularProducts()
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.GetStringAsync(AppSettings.apiUrl + "Products/PopularProducts");
            // convert response json data to csharp object
            return JsonConvert.DeserializeObject<List<PopularProduct>>(response);
        }

        public static async Task<bool> AddItemsInCart(AddToCart addToCart)
        {
            // necessary object for restful api
            var httpclient = new HttpClient();
            // register object converted to json object
            var json = JsonConvert.SerializeObject(addToCart);
            // supports for sending different language forms
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.PostAsync(AppSettings.apiUrl + "api/ShoppingCartItems", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<CartSubTotal> GetCartSubTotal(int userId)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.GetStringAsync(AppSettings.apiUrl + "api/ShoppingCartItems/SubTotal/" + userId);
            // convert response json data to csharp object
            return JsonConvert.DeserializeObject<CartSubTotal>(response);
        }

        public static async Task<List<ShoppingCartItem>> GetShoppingCartItems(int userId)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.GetStringAsync(AppSettings.apiUrl + "api/ShoppingCartItems/" + userId);
            // convert response json data to csharp object
            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
        }

        public static async Task<TotalCartItem> GetTotalCartItems(int userId)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.GetStringAsync(AppSettings.apiUrl + "api/ShoppingCartItems/TotalItems/" + userId);
            // convert response json data to csharp object
            return JsonConvert.DeserializeObject<TotalCartItem>(response);
        }

        public static async Task<bool> ClearShoppingCart(int userId)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.DeleteAsync(AppSettings.apiUrl + "api/ShoppingCartItems/" + userId);
            
            if(!response.IsSuccessStatusCode)
            {
                return false;
            }
            else 
            {
                return true;
            }
            
        }

        public static async Task<OrderResponse> PlaceOrder(Order order)
        {
            // necessary object for restful api
            var httpclient = new HttpClient();
            // register object converted to json object
            var json = JsonConvert.SerializeObject(order);
            // supports for sending different language forms
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.PostAsync(AppSettings.apiUrl + "api/Orders", content);
            // fetching access token
            var jsonresult = await response.Content.ReadAsStringAsync();
            // convert access token to csharp object
            return JsonConvert.DeserializeObject<OrderResponse>(jsonresult);
        }

        public static async Task<List<OrderByUser>> GetOrdersByUser(int userId)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.GetStringAsync(AppSettings.apiUrl + "api/Orders/OrdersByUser/" + userId);
            // convert response json data to csharp object
            return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
        }

        public static async Task<List<Order>> GetOrderDetails(int orderId)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpclient.GetStringAsync(AppSettings.apiUrl + "api/Orders/OrderDetails/" + orderId);
            // convert response json data to csharp object
            return JsonConvert.DeserializeObject<List<Order>>(response);
        }
    }

    public static class TokenValidator
    {
        public async static Task CheckTokenValidity()
        {
            var expirationTime = Preferences.Get("tokenExpirationTime", 0);
            Preferences.Set("currentTime", UnixTime.GetCurrentTime());
            var currentTime = Preferences.Get("CurrentTime", 0);

            if(expirationTime < currentTime)
            {
                var email = Preferences.Get("email", string.Empty);
                var password = Preferences.Get("password", string.Empty);
                await ApiService.Login(email, password);
            }
        }
    }
}
