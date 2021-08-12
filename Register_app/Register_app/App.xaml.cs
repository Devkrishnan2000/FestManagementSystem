using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Register_app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string conec = Register_app.Properties.Settings.Default.ConnectionString;
            string dat = Register_app.Properties.Settings.Default.Database;
            if (!string.IsNullOrEmpty(conec)&&!string.IsNullOrEmpty(dat))
            {
                MainWindow window = new MainWindow();
                window.Show();
                
            }
            else
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
        }
    }
}
