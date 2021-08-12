using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace testapp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParticipantListPage : ContentPage
    {
        private ObservableCollection<Participant> participants = new ObservableCollection<Participant>();
        public ObservableCollection<Participant> Participants { get { return participants; }  }
        string eventname = Preferences.Get("EventName", "NULL");
        List<string> admitparticpantlist = new List<string>();

        public ParticipantListPage()
        {
            InitializeComponent();
            RoundInfo(RoundPickerP);
            ParticipantListView.RefreshCommand = new Command(() =>
            {
                ParticipantListGen();
                ParticipantListView.IsRefreshing = false;
            }
           );
        

        }

        public async Task RoundInfo(Picker RoundPicker)
        {
            try
            {
                EDatabase eDatabase = new EDatabase();
                int rounds =await Task.Run(()=> eDatabase.NoOfRound(eventname));
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

        private async Task ParticipantListGen()
        {
           
            int stage = RoundPickerP.SelectedIndex+1;
            Classes.ParticipantTable pt = new Classes.ParticipantTable();
            List<string> admitparticpantlist = new List<string>();
            EDatabase eDatabase = new EDatabase();
            try
            {
                pt = null;
                admitparticpantlist = null;
                participants.Clear();
                pt = await Task.Run(() => eDatabase.GetParticipantInfo(eventname, stage));
                admitparticpantlist = await Task.Run(() => eDatabase.AdmittedParticpantNames(eventname, stage));
                DependencyService.Get<ToastA>().ShortAlert("sucess");
            }
            catch(System.Exception ex)
            {
                DependencyService.Get<ToastA>().ShortAlert(ex.Message);
            }



            for (int index = 0; index < pt.pname.Count; ++index)
            {
                string name = pt.pname[index];
                string phone = pt.pphone[index];
                string pno = pt.ppno[index].ToString();
                if (admitparticpantlist.Contains(name))
                    participants.Add(new Participant { ParticipantName = name, imageSource = "green.png", ParticipantPhone = phone , ParticipantNumber = pno });

                else
                    participants.Add(new Participant { ParticipantName = name, imageSource = "grey.png", ParticipantPhone = phone, ParticipantNumber = pno });

            }

            ParticipantListView.ItemsSource = participants;


        }

        

        private void ParticipantListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string pname = Participants[e.SelectedItemIndex].ParticipantName.ToString();
            string pho = Participants[e.SelectedItemIndex].ParticipantPhone.ToString();
            AlertOkbutton(pname, pho);
        }

        async void AlertOkbutton(string pname,string pho)
        {
            bool response = await DisplayAlert("Call Participant", "Call "+pname, "Yes", "No");
            if(response==true)
            {
                try
                {
                    PhoneDialer.Open(pho);
                }
                catch(Exception ex)
                {
                    DependencyService.Get<ToastA>().ShortAlert("Error Occured While Opening Dialer");
                }
               
            }
        }

        private async void RoundPickerP_SelectedIndexChanged(object sender, EventArgs e)
        {
           await ParticipantListGen();
        }
    }
}