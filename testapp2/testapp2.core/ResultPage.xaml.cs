using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Net.Mail;

namespace testapp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        private ObservableCollection<Participant> participants = new ObservableCollection<Participant>();
        public ObservableCollection<Participant> Participants { get { return participants; } }
        string eventname = Preferences.Get("EventName", "NULL");
        List<Int64> promotionlist = new List<Int64>();
        Classes.ParticipantTable pt = new Classes.ParticipantTable();
        public ResultPage()
        {
            InitializeComponent();

            RoundInfo(RoundPickerR);
            FinalResult.IsVisible = false;

            ResultListView.RefreshCommand = new Command(() =>
            {
                ResultListGen();
                ResultListView.IsRefreshing = false;

            });

        }

        public async Task RoundInfo(Picker RoundPicker)
        {
            try
            {
                EDatabase eDatabase = new EDatabase();
                int rounds = await Task.Run(() => eDatabase.NoOfRound(eventname));
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



        private async Task  ResultListGen()
        {
            int stage = RoundPickerR.SelectedIndex + 1;
            List<string> admitparticpantlist = new List<string>();
           
            EDatabase eDatabase = new EDatabase();
            try
            {
                admitparticpantlist = null;
                participants.Clear();
                admitparticpantlist = await Task.Run(() => eDatabase.AdmittedParticpantNames(eventname, stage));
                pt = await Task.Run(() => eDatabase.GetParticipantInfo(eventname, stage));
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
                    participants.Add(new Participant { ParticipantName = name, imageSource = "grey.png", ParticipantNumber = pno });
            }
                ResultListView.ItemsSource = participants;

          
        }

        private async void RoundPickerR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RoundPickerR.SelectedIndex == RoundPickerR.Items.Count-1)
            {
                ResultListView.IsVisible = false;
                FinalResult.IsVisible = true;
                Resultbtn.Text = "PUBLISH RESULT";
                EDatabase eDatabase = new EDatabase();
                List<string> AdmitparticipantList = await Task.Run(() => eDatabase.AdmittedParticpantNames(eventname, RoundPickerR.SelectedIndex+1));
                foreach(string val in AdmitparticipantList)
                {
                    FirstPrizePicker.Items.Add(val);
                    SecondPrizePicker.Items.Add(val);
                    ThirdPrizePicker.Items.Add(val);
                }
            }
            else
            {
                FirstPrizePicker.Items.Clear();
                SecondPrizePicker.Items.Clear();
                ThirdPrizePicker.Items.Clear();
                FinalResult.IsVisible = false;
                Resultbtn.Text = "Promote to Next Round";
                ResultListView.IsVisible = true;

                await ResultListGen();

            }
        }

        private void ResultListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string pname =Participants[e.SelectedItemIndex].ParticipantName;
            string pno = Participants[e.SelectedItemIndex].ParticipantNumber;
            
            //changes in listview
            Participant participant = Participants[e.SelectedItemIndex];
            int newindex = Participants.IndexOf(participant);
            participants.Remove(participant);

            Participant new_participant = new Participant();
            new_participant.ParticipantName = pname;
            new_participant.imageSource = "green.png";
            new_participant.ParticipantNumber = pno;
            participants.Add(new_participant);
            int oldindex = Participants.IndexOf(new_participant);
            participants.Move(oldindex, newindex);

            //add to participant name to list
            promotionlist.Add(Int64.Parse(pno));

           
           Resultbtn.IsEnabled = true;
            
        }

        private async void Resultbtn_Clicked(object sender, EventArgs e)
        {
            if (RoundPickerR.SelectedIndex == RoundPickerR.Items.Count - 1)
            {
                if(FirstPrizePicker.SelectedIndex==-1|| SecondPrizePicker.SelectedIndex==-1||ThirdPrizePicker.SelectedIndex==-1)
                {
                 await   DisplayAlert("Empty Values", "Please Add the names of The winners to their coresponding fields","cancel");
                }
                else
                {
                    Int64 firstpno =0;
                    Int64 secondpno =0;
                    Int64 thirdpno =0;
                    string firstprize = FirstPrizePicker.SelectedItem.ToString();
                    string secondprize = SecondPrizePicker.SelectedItem.ToString();
                    string thirdrpize = ThirdPrizePicker.SelectedItem.ToString();
             
                     if(pt!=null)
                    {
                        for (int index = 0; index < pt.pname.Count; ++index)
                        {
                            if (pt.pname[index] == firstprize)
                            {
                                firstpno = pt.ppno[index];
                            }
                            else if (pt.pname[index] == secondprize)
                            {
                                secondpno = pt.ppno[index];
                            }
                            else if (pt.pname[index] == thirdrpize)
                            {
                                thirdpno = pt.ppno[index];
                            }

                        }
                        EDatabase eDatabase = new EDatabase();
                        try
                        {
                            eDatabase.InsResult(firstpno, secondpno, thirdpno, eventname);
                        }
                        catch(SqlException ex)
                        {
                           await DisplayAlert("Error Occured", "It seems like the Result for this event is already published. Contact your Admin to make any changes to the Result.", "cancel");
                        }

                        }
                    }
                }
            else
            {
                if(promotionlist!=null)
                {
                    EDatabase eDatabase = new EDatabase();
                    List<string> ToAddressList = new List<string>();
                    foreach(Int64 val in promotionlist)
                    {

                      ToAddressList.Add(await Task.Run(()=> eDatabase.QualifyParticipant(val, RoundPickerR.SelectedIndex + 1, eventname)));

                    }
                     await SendMail("Sample Fest", "You have been Promoted to next round ", ToAddressList);
                    DependencyService.Get<ToastA>().ShortAlert("Email Send to Participants");

                }    
               
            }
              
            }

        private bool checkpickers()
        {
            if (FirstPrizePicker.SelectedIndex == -1 || SecondPrizePicker.SelectedIndex == -1 || ThirdPrizePicker.SelectedIndex == -1)
                return false;
            else
                return true;
        }

        private async Task SendMail(string subject,string content, List<string> toaddress)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("vedk919@gmail.com");
                mail.Subject = subject;
                mail.Body = content;
                foreach(string val in toaddress)
                {
                    mail.To.Add(val);
                }
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("vedk919@gmail.com", "d@1#4%6&8asw");
                client.EnableSsl = true;
               await client.SendMailAsync(mail);
            }
        }

        private void FirstPrizePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkpickers())
                Resultbtn.IsEnabled = true;
        }

        private void SecondPrizePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkpickers())
                Resultbtn.IsEnabled = true;
        }

        private void ThirdPrizePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkpickers())
                Resultbtn.IsEnabled = true;
        }
    }
    }
