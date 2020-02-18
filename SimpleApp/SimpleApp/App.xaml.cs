using SimpleApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleApp {
    public partial class App : Application {
        public App() {
            MainPage = new LogIn();
        }

        protected override void OnStart() {}

        protected override void OnSleep() {}

        protected override void OnResume() {}
    }
}
