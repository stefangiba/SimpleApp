using Rg.Plugins.Popup.Services;
using SimpleApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Transaction : ContentPage {
        private readonly ObservableCollection<Models.Transaction> transactionCollection = new ObservableCollection<Models.Transaction>();
        public Transaction()
        {
            InitializeComponent();
            using (SqlConnection con = new SqlConnection("server=dt-server.database.windows.net;database=DebugThugs;user=debugthugs;password=DTserver1"))
            {
                using (SqlCommand comand = new SqlCommand(@"select transactionid,amount,debugthugs.dbo.transactions.status,commerciant,cardnr,date,category from debugthugs.dbo.transactions
                                                            inner join debugthugs.dbo.cards on debugthugs.dbo.cards.cardid = debugthugs.dbo.transactions.cardid
                                                            where debugthugs.dbo.cards.userid = @userid", con))
                {
                    con.Open();
                    comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = User.Instance.Username;
                    SqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        string cardnr = reader["cardnr"].ToString();
                        string cifre = "" + cardnr.ElementAt(12) + cardnr.ElementAt(13) + cardnr.ElementAt(14) + cardnr.ElementAt(15);
                        string stare = "#ffd692";
                        if (reader["status"].ToString().Equals("processed"))
                        {
                            stare = "#dddddd";
                        }
                        else if (reader["status"].ToString().Equals("declined"))
                        {
                            stare = "#ffcccc";
                        }
                        transactionCollection.Add(new Models.Transaction(reader["transactionid"].ToString(), reader["commerciant"].ToString(), reader["status"].ToString(), reader["date"].ToString(), cifre, reader["amount"].ToString(), 
                                                                        reader["category"].ToString(), stare));
                    }
                    con.Close();
                }
            }
            TransactionsList.ItemsSource = transactionCollection;
        }

        private void Home(object sender, EventArgs e)
        {
            App.Current.MainPage = new Menu();
        }
        private void Transactions(object sender, EventArgs e)
        {
            App.Current.MainPage = new Transaction();
        }
        private void Analytics(object sender, EventArgs e)
        {
            App.Current.MainPage = new Analytics();
        }
        private void Settings(object sender, EventArgs e)
        {
            App.Current.MainPage = new Settings();
        }

        private void Select(object sender, SelectedItemChangedEventArgs e)
        {
            var t = (Models.Transaction)e.SelectedItem;
            PopupNavigation.Instance.PushAsync(new InfoPopup(t.Id, t.Categorie, t.Stare, t.Data, t.Comerciant, t.Cifre, t.Suma));
        }
    }
}