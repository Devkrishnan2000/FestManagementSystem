using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testapp2.Classes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomingMessage : ContentPage
    {
        public IncomingMessage()
        {
            InitializeComponent();
        }
    }
}