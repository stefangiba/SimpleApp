using Rg.Plugins.Popup.Pages;
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
    public partial class ErrorPopup : PopupPage {
        public ErrorPopup(string text)
        {
            InitializeComponent();
            ErrorTextLabel.Text = text;
        }
        private void OnOkButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}