using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KFCMobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            // we are holding EntUserName text data in "username"
            Preferences.Set("username", EntUserName.Text);
        }

        private void btnRetrieve_Clicked(object sender, EventArgs e)
        {
            var response = Preferences.Get("username", string.Empty);
            lblUserName.Text = response;
        }
    }
}
