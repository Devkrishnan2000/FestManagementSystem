using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FestManagementApp.Classes
{
   public class DblistModel
    {
        string dbname;
        BitmapImage dblogo;

        public string Dbname
        {
            get { return dbname; }
            set { dbname = value; }
        }

        public BitmapImage Dblogo
        {
            get { return dblogo; }
            set { dblogo = value; }
        }
    }
}
