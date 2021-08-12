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
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace FestManagementApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (FestManagementApp.Properties.Settings.Default.ConnectionString != null && FestManagementApp.Properties.Settings.Default.Database != null)
            {
                MainContent mainContent = new MainContent();
                mainContent.Show();
            }
            else
            {
                MainWindow window = new MainWindow();
                window.Show();
            }
        }
    }
}
