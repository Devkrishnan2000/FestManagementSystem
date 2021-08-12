using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestManagementApp.Classes
{
   public class ChatViewModel
    {
        string usrname;
        string rank;
        string message;
        string reciver;
        string time;

        public string Usrname
        {
            get { return usrname; }
            set { usrname = value; }
        }

        public string Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public string Reciver
        {
            get { return reciver; }
            set { reciver = value; }
        }
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        
    }
}
