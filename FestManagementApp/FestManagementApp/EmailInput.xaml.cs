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

namespace FestManagementApp
{
    /// <summary>
    /// Interaction logic for EmailInput.xaml
    /// </summary>
    public partial class EmailInput : Window
    {
        string email;

        public string Email()
        {
            { return email; }
        }

        public EmailInput()
        {
            InitializeComponent();
        }

        private void MiniBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            warningtxt.Visibility = Visibility.Collapsed;
            if(!string.IsNullOrEmpty(Emailtxt.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(Emailtxt.Text);
                    email = Emailtxt.Text;
                    this.Close();
                }
                catch
                {
                    warningtxt.Visibility = Visibility.Visible;
                }
            }
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
