using Android.App;
using Android.Content;
using Android.Telephony;
using Java.Lang;
using Rg.Plugins.Popup.Services;
using SimpleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Provider.Settings;
using Plugin.Fingerprint;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Data.SqlClient;
using System.Data;

namespace SimpleApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn : ContentPage {
        bool login = false;
        public LogIn() {
            InitializeComponent();

            UsernameEntry.Completed += (e, s) => PasswordEntry.Focus();
            PasswordEntry.Completed += (e, s) => OnLoginButtonClicked(e, s);
            
            verify();
        }

        public async void verify() {

            string imei = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            var result = await CrossFingerprint.Current.AuthenticateAsync("\n\nPut your Finger On The FingerPrint.\n");

            using (SqlConnection con = new SqlConnection("server=dt-server.database.windows.net;database=DebugThugs;user=debugthugs;password=DTserver1"))
            {
                using(SqlCommand comand = new SqlCommand(@"select userid,status from DebugThugs.dbo.Users where imei = @imei",con))
                {
                    con.Open();
                    comand.Parameters.Add("@imei", SqlDbType.NVarChar).Value = imei;
                    SqlDataReader reader = comand.ExecuteReader();
                    while (reader.Read())
                    {
                        User.Instance.Username = reader["userid"].ToString();
                        if(reader["status"].ToString().Equals("Active"))
                        {
                                App.Current.MainPage = new Menu();
                        }
                       
                    }
                    con.Close();
                }
            }
        }

        private void OnLoginButtonClicked(object sender, EventArgs e) {

            if (string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text)) {
                string message = "Void Username/Password!";
                PopupNavigation.Instance.PushAsync(new ErrorPopup(message));
            }
            else if (!UsernameEntry.Text.Equals(User.Instance.Username))
            {
                PopupNavigation.Instance.PushAsync(new ErrorPopup("This user cann't login from this device"));
            }
            else
            {
                using (SqlConnection con = new SqlConnection("server=dt-server.database.windows.net;database=DebugThugs;user=debugthugs;password=DTserver1"))
                {
                    con.Open();
                    using (SqlCommand comand = new SqlCommand(@"select userid from DebugThugs.dbo.Users where userid = @userid and password = @password", con))
                    {
                        
                        comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = User.Instance.Username;
                        comand.Parameters.Add("@password", SqlDbType.NVarChar).Value = PasswordEntry.Text;
                        SqlDataReader reader = comand.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader["userid"] != null)
                            {
                                login = true;
                            }
                        }
                        
                        
                    }
                    con.Close();
                    if (login)
                    {
                        con.Open();
                        using (SqlCommand comand = new SqlCommand(@"update DebugThugs.dbo.Users set status = @status where userid = @userid", con))
                        {
                            comand.Parameters.Add("@userid", SqlDbType.NVarChar).Value = User.Instance.Username;
                            comand.Parameters.Add("@status", SqlDbType.NVarChar).Value = "Active";
                            comand.ExecuteNonQuery();
                        }
                        con.Close();
                        App.Current.MainPage = new Menu();
                    }
                    else
                    {
                        string message = "Wrong Username/Password !";
                        PopupNavigation.Instance.PushAsync(new ErrorPopup(message));
                    }



                }
            }
           
        }
        

    }
}