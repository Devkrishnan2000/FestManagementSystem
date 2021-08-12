using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testapp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSelectPage : ContentPage
    {
        private ObservableCollection<EventNames> EventNameList;
        public ObservableCollection<EventNames> EvenNameList
        {
            get { return EventNameList; }
            set { EventNameList = value; }
        } 
        public EventSelectPage(string dbname) 
        {
            InitializeComponent();

            EvenListGen(dbname);
        }

        private void EvenListGen(string dbname)
        {
            EDatabase eDatabase = new EDatabase();
            List<string> EventList = new List<string>();
            EventNameList = new ObservableCollection<EventNames>();
            ListView listView = (ListView)FindByName("EventListView");
            Label Erlabel = (Label)FindByName("ErrorMsg");
            Image Erimage = (Image)FindByName("ErrorImg");
            Frame frame = (Frame)FindByName("login_cont");
            try
            {
                EventList = eDatabase.GetEventList();
            }
            catch(System.Exception ex)
            {
                listView.IsVisible = false;
                frame.IsVisible = false;
                Erlabel.IsVisible = true;
                Erimage.IsVisible = true;
            }
            foreach(string val in EventList)
            {
                EventNameList.Add(new EventNames() { EventName = val });
            }
            listView.ItemsSource = EventNameList;

          
           
            
        }

        private async void EventListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string ename = EventNameList[e.SelectedItemIndex].EventName.ToString();
           
            Preferences.Set("EventName", ename);
            await Navigation.PushAsync(new TabbedPage1()
            {
                BarBackgroundColor = Color.FromHex("#5352ed"),
                BarTextColor = Color.White,
            });
                
        }
    }
}