using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Register_app
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MiniBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private async void CntBtn_Click(object sender, RoutedEventArgs e)
        {
            loading.Visibility = Visibility.Visible;
            loadingtxt.Visibility = Visibility.Visible;
            CntInfoCont.Visibility = Visibility.Collapsed;
            if (string.IsNullOrEmpty(ServerName.Text) || string.IsNullOrEmpty(UserName.Text) || string.IsNullOrEmpty(Passwrd.Password))
            {

                CntInfoCont.Visibility = Visibility.Visible;
                CntInfoCont.Background = (Brush)new BrushConverter().ConvertFrom("#e74c3c");
                CntInfo.Content = "One or More Fields Are Empty";
                CntInfo.Foreground = Brushes.White;

            }
            else
            {
                CntInfoCont.Background = Brushes.White;
                CntInfo.Foreground = Brushes.Black;
                CntInfo.Content = "Connecting...";
                string ConnectionString = @"Data Source = " + ServerName.Text + "; User ID =" + UserName.Text + "; Password =" + Passwrd.Password.ToString() + "";
                Int32 code = await Task.Run(() => set_db(ConnectionString));
                if (code == 0)
                {
                    Properties.Settings.Default.ConnectionString = ConnectionString;
                    Properties.Settings.Default.Chatname = UserName.Text + "+Registration Dept";
                    Properties.Settings.Default.Save();
                    this.Hide();
                    DbSelection db = new DbSelection();
                    db.ShowDialog();
                    this.Close();

                }
                else
                {
                    CntInfoCont.Visibility = Visibility.Visible;
                    CntInfoCont.Background = (Brush)new BrushConverter().ConvertFrom("#e74c3c");
                    CntInfo.Content = "Error Occured (" + code + ")";
                    CntInfo.Foreground = Brushes.White;
                }
            }

            loadingtxt.Visibility = Visibility.Collapsed;
            loading.Visibility = Visibility.Collapsed;
        }

        private void Db_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private Int32 set_db(string connection_string)
        {

            SqlConnection conect = new SqlConnection();
            conect.ConnectionString = connection_string;
            try
            {
                conect.Open();
                return 0;
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }

        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
