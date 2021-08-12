using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using FestManagementApp.Classes;

namespace FestManagementApp
{
    /// <summary>
    /// Interaction logic for DbSelection.xaml
    /// </summary>
    public partial class DbSelection : Window
    {
        private ObservableCollection<DblistModel> db;
        public ObservableCollection<DblistModel> Db
        {
            get { return db; }
            set { db = value; }
        }

        public   DbSelection()
        {
            InitializeComponent();
            DblistGen();
              
        }

        private void MiniBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async Task DblistGen()
        {
            List<string> Dblist = new List<string>();
            Database database = new Database();
            Db = new ObservableCollection<DblistModel>();
            try
            {
               Dblist =  await Task.Run(()=> database.GetDBList());
              
                foreach(string val in Dblist)
                {
                    Db.Add(new DblistModel() { Dbname = val });
                }
            
                DblistBox.ItemsSource = Db;
            }
            catch(SqlException ex)
            {

            }
            


        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DblistBox.SelectedIndex!=-1)
            {
                Database database = new Database();
                try
                {
                    string selectval = Db[DblistBox.SelectedIndex].Dbname;
                    database.CheckDB(selectval);
                    Properties.Settings.Default.Database = Db[DblistBox.SelectedIndex].Dbname;
                    Properties.Settings.Default.Save();
                    MainContent main = new MainContent();
                    this.Hide();
                    main.ShowDialog();
                    this.Close();
                }
                catch(SqlException ex)
                {
                    CustomDialogModel dialog = new CustomDialogModel();
                    dialog.Title = "Database inaccessible";
                    dialog.Info = ex.Message;
                    CustomDialog custom = new CustomDialog(dialog);
                    custom.ShowDialog();

                    custom.Close();
                }
            }
            
            
        }

        private void topbar_MouseMove(object sender, MouseEventArgs e)
        {

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
