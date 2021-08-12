using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestManagementApp.Classes
{
   public class StatModel
    {
        int count;
        string itemname;
        
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public string Itemname
        {
            get { return itemname; }
            set { itemname = value; }
        }
    }
}
