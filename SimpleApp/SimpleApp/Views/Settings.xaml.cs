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
    public partial class Settings : ContentPage {
        public Settings()
        {
            InitializeComponent();
            using (SqlConnection con = new SqlConnection("server=dt-server.database.windows.net;database=DebugThugs;user=debugthugs;password=DTserver1"))
            {
                using (SqlCommand comand = new SqlCommand(@"select username,email,phonenr,firstname,lastname from debugthugs.dbo.users where userid = @userid", con))
                {
                    con.Open();
                    comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = User.Instance.Username;
                    SqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        username.Text = reader["username"].ToString();
                        password.Placeholder = "Cann't print the password";
                        passwordconfirm.Placeholder = "Cann't print the password";
                        email.Text = reader["email"].ToString();
                        phonenr.Text = reader["phonenr"].ToString();
                        firstname.Text = reader["firstname"].ToString();
                        lastname.Text = reader["lastname"].ToString();
                    }
                    con.Close();
                }
            }
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
        private void Setting(object sender, EventArgs e)
        {
            App.Current.MainPage = new Settings();
        }
        private void Save(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("server=dt-server.database.windows.net;database=DebugThugs;user=debugthugs;password=DTserver1"))
            {
                using (SqlCommand comand = new SqlCommand(@"update debugthugs.dbo.users set username = @username,password = @password,email = @email,
                                                           firstname = @firstname, lastname= @lastname,phonenr = @phonenr where userid = @userid", con))
                {
                    con.Open();
                    comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = User.Instance.Username;
                    comand.Parameters.Add("@username", SqlDbType.NVarChar).Value = username.Text;                   
                    comand.Parameters.Add("@email", SqlDbType.NVarChar).Value = email.Text;
                    comand.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = firstname.Text;
                    comand.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = lastname.Text;
                    comand.Parameters.Add("@phonenr", SqlDbType.NVarChar).Value = phonenr.Text;
                    comand.Parameters.Add("@password", SqlDbType.NVarChar).Value = password.Text;
                    if(password.Text.Equals(passwordconfirm.Text))
                    {
                        comand.ExecuteNonQuery();
                        App.Current.MainPage = new Settings();
                    }
                    else
                    {
                        string message = "Passwords don't match";
                        PopupNavigation.Instance.PushAsync(new ErrorPopup(message));
                    }
                    con.Close();
                }
            }
        }
    }
}