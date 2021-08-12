using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Opificio_regular.ttf", Alias = "Optificio")]
[assembly: ExportFont("Medel.ttf", Alias = "Medel")]
[assembly: ExportFont("OpenSans_Regular.ttf", Alias = "OpenSans")]
namespace testapp2
{
    public partial class App : Application
    {
        public App()

        {
            Device.SetFlags(new string[] { "AppTheme_Experimental" });
            InitializeComponent();
            var StagePageLocation = Preferences.Get("ConnectionString", "NULL");
            if (StagePageLocation == "NULL")
                MainPage = new NavigationPage(new MainPage());
            else
                MainPage = new NavigationPage(new TabbedPage1())
                {
                    BarBackgroundColor = Color.FromHex("#5352ed"),
                    BarTextColor = Color.White,

                };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
