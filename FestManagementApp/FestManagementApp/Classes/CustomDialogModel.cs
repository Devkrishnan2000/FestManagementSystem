using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FestManagementApp.Classes
{
    public class CustomDialogModel
    {
        string title;
        string info;
        BitmapImage image;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        public BitmapImage Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
