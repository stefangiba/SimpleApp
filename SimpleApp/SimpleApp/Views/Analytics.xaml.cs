using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Analytics : ContentPage {
        public Analytics()
        {
            InitializeComponent();
        }

        private void Food(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InfoPopup("food"));
        }

        private void Shopping(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InfoPopup("shopping"));
        }

        private void Health(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InfoPopup("health"));
        }
        private void Transfer(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InfoPopup("transfer"));
        }

        private void Home(object sender, EventArgs e)
        {
            App.Current.MainPage = new Menu();
        }
        private void Transactions(object sender, EventArgs e)
        {
            App.Current.MainPage = new Transaction();
        }
        private void Analyticss(object sender, EventArgs e)
        {
            App.Current.MainPage = new Analytics();
        }
        private void Settings(object sender, EventArgs e)
        {
            App.Current.MainPage = new Settings();
        }
    }
}