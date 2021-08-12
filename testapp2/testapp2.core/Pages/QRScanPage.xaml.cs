using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace testapp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScanPage : ContentPage
    {
        public string ConnectionString = Preferences.Get("ConnectionString", "NULL");
        public string Database = Preferences.Get("Database", "NULL");
        public string eventname = Preferences.Get("EventName", "CoordinatorApp");

        public QRScanPage()
        {
            InitializeComponent();

            Initial();
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
             string ms=Admitfun(result.Text);
                DependencyService.Get<ToastA>().ShortAlert(ms);
                Vibration.Vibrate();
            });
                
        }

        private void ScanBtn_Clicked(object sender, EventArgs e)
        {
           
        }

        private string Admitfun(string res)
        {
            EDatabase eDatabase = new EDatabase();
            try
            {
                int stage = RoundPicker.SelectedIndex + 1;
                eDatabase.AdmitDB(res, ConnectionString, Database,eventname,stage);
                return "Sucess";
            }
            catch(System.Exception ex)
            {
                return ex.Message;
            }
        }

        private void RoundInfo()
        {
            try
            {
                EDatabase eDatabase = new EDatabase();
                int rounds = eDatabase.NoOfRound(eventname);
                for (int i = 1; i <= rounds; ++i)
                {
                    RoundPicker.Items.Add("Round " + i);
                }
                RoundPicker.SelectedIndex = 0;
            }
            catch (System.Exception ex)
            {

            }
        }

        private async Task Initial()
        {
            await Task.Run(RoundInfo);
        }
    }
}