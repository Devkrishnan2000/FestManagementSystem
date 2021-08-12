using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Data.SqlClient;

namespace testapp2
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
           
            InitializeComponent();


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            EDatabase eDatabase = new EDatabase();
            Label label = (Label)FindByName("ConectStatus");
            label.Text = "Connecting";
            //  string connection_string = @"server = 192.168.29.247\SQLEXPRESS2019; User ID = atom; Password = 1234; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            string connection_string = @"server = "+servname_txt.Text+"; User ID = "+usrname_txt.Text+"; Password = "+pswrd_txt.Text+"; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            label.Text = await Task.Run(() => open_conec(connection_string));
            if (label.Text == "Connected Sucessfully")
                await Navigation.PushAsync(new DatabaseSelectPage());
        }

        private  string open_conec(string conection_string)
        {
            SqlConnection conec = new SqlConnection();
            conec.ConnectionString = conection_string;
            try
            {
                conec.Open();
                Preferences.Set("ConnectionString", conection_string);
                Preferences.Set("UserName", usrname_txt.Text);
                return "Connected Sucessfully";
            }
            catch(System.Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
