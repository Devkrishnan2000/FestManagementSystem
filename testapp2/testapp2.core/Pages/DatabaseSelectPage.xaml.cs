using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace testapp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatabaseSelectPage : ContentPage
    {
        private ObservableCollection<DbNames> DBlist;
        public ObservableCollection<DbNames> DBNameList
        {  
            get { return DBlist; }
            set { DBlist = value; }
        }
        
        public DatabaseSelectPage()
        {
            InitializeComponent();

            DbNamesGen();
        }

        private void DbNamesGen()
        {
            EDatabase eDatabase = new EDatabase();
            List<string> DBList = new List<string>();
            DBlist = new ObservableCollection<DbNames>();
            ListView listView = (ListView)FindByName("DataBaseListView");
             
            string connection_string = @"server = 192.168.29.247\SQLEXPRESS2019; User ID = atom; Password = 1234; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            DBList = eDatabase.GetDBList();
            foreach(string val in DBList)
            {
                DBlist.Add(new DbNames() { DBName = val });
            }

            listView.ItemsSource = DBlist;
            
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
        }

        private async void DataBaseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            string Dbname = DBlist[e.SelectedItemIndex].DBName.ToString();
            Preferences.Set("Database", Dbname);
            await Navigation.PushAsync(new EventSelectPage(Dbname));
        }
    }
}