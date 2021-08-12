using FestManagementApp.Classes;
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
using System.Data;
using System.Net.Mail;
using FestManagementApp.UserControls;
using System.Collections.ObjectModel;

namespace FestManagementApp
{
    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    public partial class MainContent : Window
    {
        Database database = new Database();
        string borchurepath;
        MailMessage mail = new MailMessage();
        DataTable search = new DataTable();
        private ObservableCollection<ResultViewModel> rv;

        public ObservableCollection<ResultViewModel> RV
        {
            get { return rv; }
            set { rv = value; }
        }

        public MainContent()
        {
            InitializeComponent();
            TableListGen(database);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MiniBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void TableListGen(Database db)
        {
            try
            {

                List<string> tablelist = db.TableList();
                foreach (string val in tablelist)
                {
                    TableCombobox.Items.Add(val);
                }
            }
            catch (Exception ex)
            {
                CustomDialogModel dialogmodel = new CustomDialogModel();
                dialogmodel.Title = "Table Retrival Failed";
                dialogmodel.Info = ex.Message;
                CustomDialog dialog = new CustomDialog(dialogmodel);
                dialog.ShowDialog();

            }

        }

        private void TableCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableCombobox.SelectedIndex != -1)
            {
                try
                {
                    DataTable dt = database.GetTable(TableCombobox.SelectedItem.ToString());
                    DisplayTable.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    CustomDialogModel dialogmodel = new CustomDialogModel();
                    dialogmodel.Title = "Table Retrieval Failed";
                    dialogmodel.Info = ex.Message;
                    CustomDialog dialog = new CustomDialog(dialogmodel);
                    dialog.ShowDialog();
                    dialog.Close();
                }
            }
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            if (TableCombobox.SelectedIndex != -1)
               SaveToDatabase(TableCombobox.SelectedItem.ToString(), DisplayTable);
        }

        private void SaveToDatabase(string tablename, DataGrid dataGrid)
        {
            DataTable dt = new DataTable();
            dt = ((DataView)dataGrid.ItemsSource).ToTable();

            try
            {
                database.GetTable(tablename, dt);
                MessageBox.Show("Sucess");
                dataGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                CustomDialogModel dialogmodel = new CustomDialogModel();
                dialogmodel.Title = "Table Updation Failed";
                dialogmodel.Info = ex.Message;
                CustomDialog dialog = new CustomDialog(dialogmodel);
                dialog.ShowDialog();
                dialog.Close();
            }
        }

        private void AddList_Click(object sender, RoutedEventArgs e)
        {

            EmailInput emailInput = new EmailInput();
            emailInput.ShowDialog();
            string emailval = emailInput.Email();
            if (!string.IsNullOrEmpty(emailval))
            {
                SenderList.Items.Add(emailval);
            }

        }

        private void SendCollege_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                List<string> emailist = database.GetCollegeEmail();
                if (emailist != null)
                {
                    foreach (string val in emailist)
                    {
                        SenderList.Items.Add(val);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomDialogModel dialogmodel = new CustomDialogModel();
                dialogmodel.Title = "Error";
                dialogmodel.Info = ex.Message;
                CustomDialog dialog = new CustomDialog(dialogmodel);
                dialog.ShowDialog();
                dialog.Close();
            }
        }

        private void RemovList_Click(object sender, RoutedEventArgs e)
        {
            if (SenderList.SelectedIndex != -1)
            {
                SenderList.Items.Remove(SenderList.SelectedItem);
            }
            else
            {
                CustomDialogModel dialogmodel = new CustomDialogModel();
                dialogmodel.Title = "Sender List";
                dialogmodel.Info = "Please Select an item to remove";
                CustomDialog dialog = new CustomDialog(dialogmodel);
                dialog.ShowDialog();
                dialog.Close();
            }
        }

        private void attachmentBtn_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDlg.ShowDialog();

            if (result == true)
            {
                borchurepath = openFileDlg.FileName;

            }
        }

        private async void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SenderList.HasItems && !string.IsNullOrEmpty(subjecttxt.Text) && !string.IsNullOrEmpty(Bodytxt.Text))
            {
                foreach (string val in SenderList.Items)
                {
                    mail.To.Add(val);
                }
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("vedk919@gmail.com");
                mail.Subject = subjecttxt.Text;
                mail.Body = Bodytxt.Text;
                if (!string.IsNullOrEmpty(borchurepath))
                {
                    mail.Attachments.Add(new Attachment(borchurepath));
                }

                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("vedk919@gmail.com", "d@1#4%6&8asw");
                client.EnableSsl = true;
                SendBtn.Content = "SENDING..";
                SendBtn.IsEnabled = false;
                await client.SendMailAsync(mail);
                SendBtn.Content = "SEND INVITE";
                SendBtn.IsEnabled = true;
                MessageBox.Show("Mail send ", "sucess");
            }
        }



        private void CollegeTab_Selected(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
                try
                {
                    DataTable dt = database.GetTable("college");
                    DisplayCollegeTable.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    CustomDialogModel dialogmodel = new CustomDialogModel();
                    dialogmodel.Title = "Table Retrieval Failed";
                    dialogmodel.Info = ex.Message;
                    CustomDialog dialog = new CustomDialog(dialogmodel);
                    dialog.ShowDialog();
                    dialog.Close();
                }
            }
        }

        private void InscollegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CollegeNametxt.Text) && !string.IsNullOrEmpty(CollegeMailtxt.Text) && !string.IsNullOrEmpty(CollegeLoctxt.Text))
            {
                try
                {
                    var addr = new MailAddress(CollegeMailtxt.Text);
                    DataTable dt = new DataTable();
                    string insrtcmd = "insert into college (cname,cloc,cmail) values('" + CollegeNametxt.Text + "','" + CollegeLoctxt.Text + "','" + CollegeMailtxt.Text + "')";
                    database.InsertIntoTable(insrtcmd);
                    CollegeNametxt.Clear();
                    CollegeLoctxt.Clear();
                    CollegeMailtxt.Clear();
                    dt = database.DisplayTable("college");
                    DisplayCollegeTable.ItemsSource = dt.DefaultView;

                }
                catch (Exception ex)
                {
                    CustomDialogModel dialogmodel = new CustomDialogModel();
                    dialogmodel.Title = "Error";
                    dialogmodel.Info = ex.Message;
                    CustomDialog dialog = new CustomDialog(dialogmodel);
                    dialog.ShowDialog();
                    dialog.Close();
                }
            }
        }

        private void searchTable(string tablename, DataGrid dataGrid, TextBox searchbox, string columnname)
        {
            search = database.DisplayTable(tablename);
            if (CollegeSearchBox.Text == null)
            {
                dataGrid.ItemsSource = search.DefaultView;
            }
            else
            {
                search.DefaultView.RowFilter = "" + columnname + " LIKE '%" + searchbox.Text + "%'";
                dataGrid.ItemsSource = search.DefaultView;
            }
        }

        private void SearchBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Name == "CollegeSearchBox")
                searchTable("college", DisplayCollegeTable, CollegeSearchBox, "CNAME");
            else if (textBox.Name == "CordSearchBox")
                searchTable("coordinator", DisplayCordTable, CordSearchBox, "CONAME");
            else if (textBox.Name == "EventSearchBox")
                searchTable("event", DisplayEventTable, EventSearchBox, "ENAME");
            else if(textBox.Name =="SearchBoox" && TableCombobox.SelectedIndex!=-1)
            {

            }


        }

        private void SaveToDbBtn(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Name == "CordSaveBtn")
                SaveToDatabase("coordinator", DisplayCordTable);
            else if (button.Name == "CollegeSaveBtn")
                SaveToDatabase("college", DisplayCollegeTable);
            else if (button.Name == "EventSaveBtn")
                SaveToDatabase("event", DisplayEventTable);

        }



        private void InscordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CoordinatorNametxt.Text) && !string.IsNullOrEmpty(CoEventtxt.Text) && !string.IsNullOrEmpty(Phonetxt.Text))
            {
                try
                {

                    DataTable dt = new DataTable();
                    string insrtcmd = "insert into coordinator (coname,ename,cpno) values('" + CoordinatorNametxt.Text + "','" + CoEventtxt.Text + "','" + Phonetxt.Text + "')";
                    database.InsertIntoTable(insrtcmd);
                    database.CreateCordAccount(CoordinatorNametxt.Text, CoEventtxt.Text);
                    CoordinatorNametxt.Clear();
                    CoEventtxt.SelectedIndex = -1;
                    Phonetxt.Clear();
                    dt = database.DisplayTable("coordinator");
                    DisplayCordTable.ItemsSource = dt.DefaultView;

                }
                catch (Exception ex)
                {
                    CustomDialogModel dialogmodel = new CustomDialogModel();
                    dialogmodel.Title = "Error";
                    dialogmodel.Info = ex.Message;
                    CustomDialog dialog = new CustomDialog(dialogmodel);
                    dialog.ShowDialog();
                    dialog.Close();
                }
            }
        }

        private void CordTab_Selected(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
                try
                {
                    CoEventtxt.Items.Clear();
                    DataTable dt = database.GetTable("coordinator");
                    DisplayCordTable.ItemsSource = dt.DefaultView;
                    List<string> eventlist = new List<string>();
                    eventlist = database.Geteventlist();
                    foreach(string val in eventlist)
                    {
                        CoEventtxt.Items.Add(val);

                    }
                    CoEventtxt.Items.Add("Registration");
                }
                catch (Exception ex)
                {
                    CustomDialogModel dialogmodel = new CustomDialogModel();
                    dialogmodel.Title = "Table Retrieval Failed";
                    dialogmodel.Info = ex.Message;
                    CustomDialog dialog = new CustomDialog(dialogmodel);
                    dialog.ShowDialog();
                    dialog.Close();
                }
            }
        }
        private void DelSelRow(string tablename, DataGrid dataGrid)
        {
            if (dataGrid.SelectedIndex != -1)
            {

                DataTable dt = new DataTable();
                dt = ((DataView)dataGrid.ItemsSource).ToTable();
                
                int index = dataGrid.SelectedIndex;
                if (dataGrid.Name == "DisplayCordTable")
                {
                    string usr = dt.Rows[index][1].ToString();
                    database.Delaccnt(usr);
                }
                dt.Rows.Remove(dt.Rows[index]);
                dataGrid.ItemsSource = dt.DefaultView;
                database.RemoveRow(tablename, index);
            }
        }

        private void DeleteRow(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Name == "CordDelBtn")
                DelSelRow("coordinator", DisplayCordTable);
            else if (button.Name == "CollegeDelBtn")
                DelSelRow("college", DisplayCollegeTable);
            else if (button.Name == "EventDelBtn")
                DelSelRow("event", DisplayEventTable);
            else if (button.Name == "DelBtn"&& TableCombobox.SelectedIndex!=-1)
                DelSelRow(TableCombobox.SelectedItem.ToString(), DisplayTable);
        }



        private void EventTab_Selected_1(object sender, RoutedEventArgs e)
        {
            var tab = sender as TabItem;
            if (tab != null)
            {
                try
                {
                    DataTable dt = database.GetTable("event");
                    DisplayEventTable.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    CustomDialogModel dialogmodel = new CustomDialogModel();
                    dialogmodel.Title = "Table Retrieval Failed";
                    dialogmodel.Info = ex.Message;
                    CustomDialog dialog = new CustomDialog(dialogmodel);
                    //dialog.ShowDialog(); // causing  error system.invalid operation
                    //dialog.Close();
                }
            }
        }

        private void InsEventBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EventNametxt.Text) && !string.IsNullOrEmpty(Desctxt.Text) && !string.IsNullOrEmpty(Venuetxt.Text)&& !string.IsNullOrEmpty(GroupMemtxt.Text) && !string.IsNullOrEmpty(Costtxt.Text) && !string.IsNullOrEmpty(NoRoundtxt.Text))
            {
                try
                {

                    DataTable dt = new DataTable();
                    string insrtcmd = "insert into event (ename,edesc,evenue,ereqmember,ecost,erounds) values('" + EventNametxt.Text + "','" + Desctxt.Text + "','" + Venuetxt.Text + "','"+GroupMemtxt.Text+"','"+Costtxt.Text+"','"+NoRoundtxt.Text+"')";
                    database.InsertIntoTable(insrtcmd);
                    EventNametxt.Clear();
                    Desctxt.Clear();
                    Venuetxt.Clear();
                    GroupMemtxt.Clear();
                    Costtxt.Clear();
                    NoRoundtxt.Clear();
                    dt = database.DisplayTable("event");
                    DisplayEventTable.ItemsSource = dt.DefaultView;
                    

                }
                catch (Exception ex)
                {
                    CustomDialogModel dialogmodel = new CustomDialogModel();
                    dialogmodel.Title = "Error";
                    dialogmodel.Info = ex.Message;
                    CustomDialog dialog = new CustomDialog(dialogmodel);
                    dialog.ShowDialog();
                    dialog.Close();
                }

            }
        }

        private void StackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        

        private void StatsTab_Selected(object sender, RoutedEventArgs e)
        {
            List<StatModel> itemlist = new List<StatModel>();
            StatModel item = new StatModel();
            try
            {
                partnum.Content = database.participantcount();

                itemlist = database.popcollege("pcollege", "particpant");
                item = itemlist[0];
                mostcollege.Content = item.Itemname.Substring(0, item.Itemname.IndexOf(','));
                itemlist = database.popcollege("ename", "event_details");
                item = itemlist[0];
               mostevent.Content = item.Itemname.ToString();
               
            }
            catch(Exception ex)
            {
             string hr=   ex.Message;
            }
        }

        private void result_tab_Selected(object sender, RoutedEventArgs e)
        {
            RV = new ObservableCollection<ResultViewModel>();
            
                List<Results> res = new List<Results>();
                 res =database.getResults();
            foreach(Results ent in res)
            {
                RV.Add(new ResultViewModel { Ename = ent.Ename, Firstprize = ent.first, Secondprize = ent.second, Thirdprize = ent.third });
            }
            ResultBox.ItemsSource = RV;
               
           
        }
    }
}
