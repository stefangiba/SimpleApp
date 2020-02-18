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
    public partial class Menu : ContentPage {
        
        public Menu()
        {
            InitializeComponent();
            using (SqlConnection con = new SqlConnection("server=dt-server.database.windows.net;database=DebugThugs;user=debugthugs;password=DTserver1"))
            {
                using (SqlCommand comand = new SqlCommand(@"select type, balance, cardnr, iban from debugthugs.dbo.cards where debugthugs.dbo.cards.userid = @userid", con))
                {
                    con.Open();
                    comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = User.Instance.Username;
                    SqlDataReader reader = comand.ExecuteReader();
                    reader.Read();
                    iban.Text = reader["iban"].ToString();
                    type.Text = reader["type"].ToString();
                    amount.Text = reader["balance"].ToString() + " RON";
                    cardid.Text = "**** **** **** " + reader["cardnr"].ToString().ElementAt(12) + reader["cardnr"].ToString().ElementAt(13) + reader["cardnr"].ToString().ElementAt(14) + reader["cardnr"].ToString().ElementAt(15);

                    reader.Read();
                    iban2.Text = reader["iban"].ToString();
                    type2.Text = reader["type"].ToString();
                    amount2.Text = reader["balance"].ToString() + " RON";
                    cardid2.Text = "**** **** **** " + reader["cardnr"].ToString().ElementAt(12) + reader["cardnr"].ToString().ElementAt(13) + reader["cardnr"].ToString().ElementAt(14) + reader["cardnr"].ToString().ElementAt(15);

                    con.Close();
                }
                using (SqlCommand comand = new SqlCommand(@"select firstname, lastname from debugthugs.dbo.users where userid = @userid", con))
                {
                    con.Open();
                    comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = User.Instance.Username;
                    SqlDataReader reader = comand.ExecuteReader();
                    reader.Read();
                    name.Text = reader["firstname"].ToString() + " " + reader["lastname"].ToString();

                    name2.Text = name.Text;

                    con.Close();
                }
            }
        }
        private void Home(object sender, EventArgs e) {
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
        private void NewTrans(object sender, EventArgs e)
        {
            App.Current.MainPage = new NewTransaction(iban.Text);
        }
    }

}