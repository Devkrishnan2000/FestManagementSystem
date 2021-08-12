using System;
using System.Collections.Generic;
using System.Text;

namespace testapp2.Classes
{
   public class ParticipantTable
    {
        List<string> PName = new List<string>();
        List<string> Pphno = new List<string>();
        List<Int64> Ppno = new List<long>();
        public List<string> pname
        {
            get { return PName; }
            set { PName = value; }
        }

        public List<string> pphone
        {
            get { return Pphno; }
            set { Pphno = value; }
        }

        public List<Int64> ppno
        {
            get { return Ppno; }
            set { Ppno = value; }
        }
            

    }
}
