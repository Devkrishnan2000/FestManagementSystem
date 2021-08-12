using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestManagementApp.Classes
{
   public class ResultViewModel
    {
        string ename;
        string firstprize;
        string secondprize;
        string thirdprize;

        public string Ename
        {
            get { return ename; }
            set { ename = value; }
        }
        public string Firstprize
        {
            get { return firstprize; }
            set { firstprize = value; }
        }
        public string Secondprize
        {
            get { return secondprize; }
            set { secondprize = value; }
        }
        public string Thirdprize
        {
            get { return thirdprize; }
            set { thirdprize = value; }
        }
    }
}
