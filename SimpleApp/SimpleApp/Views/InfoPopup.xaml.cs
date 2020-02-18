using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPopup : PopupPage {
        public InfoPopup(string Id, string Categorie, string Stare, string Data, string Comerciant, string Cifre, string Suma)
        {
            InitializeComponent();
            infoTextLabel.Text = "Transaction ID:  " + Id + "\n\n" +
                                 "Category:             " + Categorie + "\n\n" +
                                 "Status:                    " + Stare + "\n\n" +
                                 "Date:                        " + Data + "\n\n" +
                                 "Commerciant:     " + Comerciant + "\n\n" +
                                 "Card Number:     **** **** **** " + Cifre + "\n\n\n\n" +
                                 "Amount:          " + Suma + " RON";
        }

        public InfoPopup(string Categorie)
        {
            InitializeComponent();
            Titlu.Text = Categorie.ToUpper() + "\n\n";
            if(Categorie.CompareTo("food") == 0)
                infoTextLabel.Text = "Total Amount For Last Mounth:  \n\n" +
                                    "    523.87 RON\n\n\n" +
                                    "Total Amount For This Mounth:  \n\n" +
                                    "    353.24 RON\n\n\n" +
                                    "Total Amount For This Day:  \n\n" +
                                    "    62.43 RON\n\n\n";
            if (Categorie.CompareTo("shopping") == 0)
                infoTextLabel.Text = "Total Amount For Last Mounth:  \n\n" +
                                    "    1083.217 RON\n\n\n" +
                                    "Total Amount For This Mounth:  \n\n" +
                                    "    872.76 RON\n\n\n" +
                                    "Total Amount For This Day:  \n\n" +
                                    "    0.00 RON\n\n\n";
            if (Categorie.CompareTo("health") == 0)
                infoTextLabel.Text = "Total Amount For Last Mounth:  \n\n" +
                                    "    234.98 RON\n\n\n" +
                                    "Total Amount For This Mounth:  \n\n" +
                                    "    0.00 RON\n\n\n" +
                                    "Total Amount For This Day:  \n\n" +
                                    "    0.00 RON\n\n\n";
            if (Categorie.CompareTo("transfer") == 0)
                infoTextLabel.Text = "Total Amount For Last Mounth:  \n\n" +
                                    "    0.00 RON\n\n\n" +
                                    "Total Amount For This Mounth:  \n\n" +
                                    "    20.00 RON\n\n\n" +
                                    "Total Amount For This Day:  \n\n" +
                                    "    20.00 RON\n\n\n";


        }
            private void OnOkButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}