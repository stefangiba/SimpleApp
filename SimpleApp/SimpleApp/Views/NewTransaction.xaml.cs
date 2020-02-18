using Rg.Plugins.Popup.Services;
using SimpleApp.Models;
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
    public partial class NewTransaction : ContentPage { 

        string iban= String.Empty;
        string cardid = String.Empty;
        public NewTransaction(string iban)
        {
            InitializeComponent();
            this.iban = iban;
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
            App.Current.MainPage = new Menu();
        }
        private void Settings(object sender, EventArgs e)
        {
            App.Current.MainPage = new Settings();
        }
        private void Send(object sender, EventArgs e)
        {
            string toCardId = "";
            string balance = "";
            string userid = "";

            string today = DateTime.Today.ToString();

            using (SqlConnection con = new SqlConnection("server=dt-server.database.windows.net;database=DebugThugs;user=debugthugs;password=DTserver1"))
            {
                using (SqlCommand comand = new SqlCommand(@"select cardid from debugthugs.dbo.cards where iban = @iban", con))
                {
                    con.Open();
                    comand.Parameters.Add("@iban", SqlDbType.NVarChar).Value = iban;
                    SqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        cardid = reader["cardid"].ToString();
                    }

                    con.Close();
                }


                string firstname = "";
                using (SqlCommand comand = new SqlCommand(@"select cardid,balance,userid, debugthugs.dbo.users.firstname from debugthugs.dbo.cards innerjoin debugthugs.dbo.users on debugthugs.dbo.users.userid = debugthugs.dbo.cards where iban = @iban", con))
                {
                    con.Open();
                    comand.Parameters.Add("@iban", SqlDbType.NVarChar).Value = numar.Text;
                    SqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        toCardId = reader["cardid"].ToString();
                        balance = reader["balance"].ToString();
                        userid = reader["userid"].ToString();
                        firstname = reader["firstname"].ToString();
                    }

                    con.Close();
                }

                using (SqlCommand comand = new SqlCommand(@"update debugthugs.dbo.cards set balance = @balance where cardid = @cardid", con))
                {
                    con.Open();
                    comand.Parameters.Add("@cardid", SqlDbType.NVarChar).Value = toCardId;
                    comand.Parameters.Add("@balance", SqlDbType.Float).Value = float.Parse(balance) + float.Parse(amount.Text);

                    comand.ExecuteNonQuery();
                    
                    con.Close();
                }

                using (SqlCommand comand = new SqlCommand(@"update debugthugs.dbo.cards set balance = @balance where iban = @iban", con))
                {
                    con.Open();
                    comand.Parameters.Add("@iban", SqlDbType.NVarChar).Value = iban;
                    comand.Parameters.Add("@balance", SqlDbType.Float).Value = float.Parse(balance) - float.Parse(amount.Text);

                    comand.ExecuteNonQuery();

                    con.Close();
                }


                using (SqlCommand comand = new SqlCommand(@"insert into debugthugs.dbo.transactions (amount,status,date,userid,cardid,tocardid,commerciant,category) values (@amount,@status,@date,@userid,@cardid,@tocardid,@commerciant,@category)", con))
                {
                    con.Open();
                    comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = User.Instance.Username;
                    comand.Parameters.Add("@amount", SqlDbType.Float).Value = -float.Parse(amount.Text);
                    comand.Parameters.Add("@status", SqlDbType.NVarChar).Value = "processed";
                    comand.Parameters.Add("@cardid", SqlDbType.NVarChar).Value = cardid;
                    comand.Parameters.Add("@tocardid", SqlDbType.NVarChar).Value = toCardId;
                    comand.Parameters.Add("@commerciant", SqlDbType.NVarChar).Value = firstname;
                    comand.Parameters.Add("@date", SqlDbType.NVarChar).Value = today;
                    comand.Parameters.Add("@category", SqlDbType.NVarChar).Value = "transactions";

                    comand.ExecuteNonQuery();

                    con.Close();
                }

                using (SqlCommand comand = new SqlCommand(@"insert into debugthugs.dbo.transactions (amount,status,date,userid,cardid,tocardid,commerciant,category) values (@amount,@status,@date,@userid,@cardid,@tocardid,@commerciant,@category)", con))
                {
                    con.Open();
                    comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = userid;
                    comand.Parameters.Add("@amount", SqlDbType.Float).Value = float.Parse(amount.Text);
                    comand.Parameters.Add("@status", SqlDbType.NVarChar).Value = "processed";
                    comand.Parameters.Add("@cardid", SqlDbType.NVarChar).Value = toCardId;
                    comand.Parameters.Add("@tocardid", SqlDbType.NVarChar).Value = "";
                    comand.Parameters.Add("@date", SqlDbType.NVarChar).Value = today;
                    comand.Parameters.Add("@commerciant", SqlDbType.NVarChar).Value = firstname;
                    comand.Parameters.Add("@category", SqlDbType.NVarChar).Value = "transactions";

                    comand.ExecuteNonQuery();
                    string message = "Done!";
                    PopupNavigation.Instance.PushAsync(new ErrorPopup(message));

                    con.Close();
                }

            }
            App.Current.MainPage = new Menu();
        }

        private void Request(object sender, EventArgs e)
        {
            string toCardId = "";
            string balance = "";
            string userid = "";

            string today = DateTime.Today.ToString();

            using (SqlConnection con = new SqlConnection("server=dt-server.database.windows.net;database=DebugThugs;user=debugthugs;password=DTserver1"))
            {
                using (SqlCommand comand = new SqlCommand(@"select cardid from debugthugs.dbo.cards where iban = @iban", con))
                {
                    con.Open();
                    comand.Parameters.Add("@iban", SqlDbType.NVarChar).Value = iban;
                    SqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        cardid = reader["cardid"].ToString();
                    }

                    con.Close();
                }



                using (SqlCommand comand = new SqlCommand(@"select cardid,balance,userid from debugthugs.dbo.cards where iban = @iban", con))
                {
                    con.Open();
                    comand.Parameters.Add("@iban", SqlDbType.NVarChar).Value = numar.Text;
                    SqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        toCardId = reader["cardid"].ToString();
                        balance = reader["balance"].ToString();
                        userid = reader["userid"].ToString();
                    }

                    con.Close();
                }


                using (SqlCommand comand = new SqlCommand(@"insert into debugthugs.dbo.transactions (amount,status,date,userid,cardid,tocardid,category) values (@amount,@status,@date,@userid,@cardid,@tocardid,@category)", con))
                {
                    con.Open();
                    comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = User.Instance.Username;
                    comand.Parameters.Add("@amount", SqlDbType.Float).Value = float.Parse(amount.Text);
                    comand.Parameters.Add("@status", SqlDbType.NVarChar).Value = "processing";
                    comand.Parameters.Add("@cardid", SqlDbType.NVarChar).Value = cardid;
                    comand.Parameters.Add("@tocardid", SqlDbType.NVarChar).Value = toCardId;
                    comand.Parameters.Add("@date", SqlDbType.NVarChar).Value = today;
                    comand.Parameters.Add("@category", SqlDbType.NVarChar).Value = "transactions";

                    comand.ExecuteNonQuery();

                    con.Close();
                }

                using (SqlCommand comand = new SqlCommand(@"insert into debugthugs.dbo.transactions (amount,status,date,userid,cardid,tocardid,category) values (@amount,@status,@date,@userid,@cardid,@tocardid,@category)", con))
                {
                    con.Open();
                    comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = userid;
                    comand.Parameters.Add("@amount", SqlDbType.Float).Value = -float.Parse(amount.Text);
                    comand.Parameters.Add("@status", SqlDbType.NVarChar).Value = "processing";
                    comand.Parameters.Add("@cardid", SqlDbType.NVarChar).Value = toCardId;
                    comand.Parameters.Add("@tocardid", SqlDbType.NVarChar).Value = "";
                    comand.Parameters.Add("@date", SqlDbType.NVarChar).Value = today;
                    comand.Parameters.Add("@category", SqlDbType.NVarChar).Value = "transactions";

                    comand.ExecuteNonQuery();
                    string message = "Done!";
                    PopupNavigation.Instance.PushAsync(new ErrorPopup(message));

                    con.Close();
                }
            }
                App.Current.MainPage = new Menu();
        }
        
    }
}