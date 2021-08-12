using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Xamarin.Essentials;
namespace testapp2
{
   public  class EDatabase
    {
        public string connect(string ConnectionString)
        {
            SqlConnection conec = new SqlConnection();
            conec.ConnectionString = ConnectionString;
            try
            {
                conec.Open();
                conec.Close();
                return "Connected Sucessfully";
                
            }
            catch (System.Exception ex)
            {
                conec.Close();
                return ex.Message.ToString();
            }
           
        }

        public  List<string> GetEventList()
        {
            string ConnectionString = Preferences.Get("ConnectionString", "NULL");
            string dbname = Preferences.Get("Database", "NULL");
            List<String> EventList = new List<string>();
            SqlConnection conec = new SqlConnection();
            conec.ConnectionString = ConnectionString;
            conec.Open();
            SqlCommand slec = new SqlCommand("use "+dbname, conec);
            slec.ExecuteNonQuery();
            SqlCommand sqlCommand = new SqlCommand("select ename from event",conec);
             using(SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while(reader.Read())
                {
                    EventList.Add(reader[0].ToString());
                }
            }
            conec.Close();
            return EventList;
        }

        public List<String> GetDBList()
        {
            string ConnectionString = Preferences.Get("ConnectionString", "NULL");
            List<String> DBList = new List<String>();
            using (SqlConnection conec = new SqlConnection(ConnectionString))
            {
                conec.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", conec))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DBList.Add(dr[0].ToString());
                        }
                    }
                }
            }

            return DBList;
        }

        public void AdmitDB(string id, string ConnectionString,string Database,string ename,int stage)
        {
            int pno = int.Parse(id);
            int val = 1;
            string dbcmd = "use " + Database;
            string admitcmd = "update event_details set admit=" + val + " where pno=" + pno + " AND ename='"+ename+"' AND stage="+stage+"";
            using(SqlConnection conec = new SqlConnection())
            {
                conec.ConnectionString = ConnectionString;
                conec.Open();
                SqlCommand cmd = new SqlCommand(admitcmd, conec);
                SqlCommand cmdd = new SqlCommand(dbcmd, conec);
                cmdd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
            }
        }

        public int NoOfRound(string ename)
        {
            int rno = -1;
            string database = Preferences.Get("Database", "NULL");
            string dbcmd = "use " + database;
            string rcmd = "Select erounds from event where ename='" + ename + "'";
            using (SqlConnection conec = new SqlConnection())
            {
                conec.ConnectionString = Preferences.Get("ConnectionString", "NULL");
                conec.Open();
                SqlCommand cmd1 = new SqlCommand(dbcmd, conec);
                SqlCommand cmd2 = new SqlCommand(rcmd, conec);
                cmd1.ExecuteNonQuery();
                using(SqlDataReader reader = cmd2.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        rno = int.Parse(reader[0].ToString());
                    }
                }
            }

            return rno;
        }

        public List<string> ParticipantNames(string ename,int stage)
        {
            List<string> pname = new List<string>();
            string database = Preferences.Get("Database", "NULL");
            string dbcmd = "use " + database;
            string rcmd = "Select pname from event_details where ename='" + ename + "' AND stage="+stage+"";
            using (SqlConnection conec = new SqlConnection())
            {
                conec.ConnectionString = Preferences.Get("ConnectionString", "NULL");
                conec.Open();
                SqlCommand cmd1 = new SqlCommand(dbcmd, conec);
                SqlCommand cmd2 = new SqlCommand(rcmd, conec);
                cmd1.ExecuteNonQuery();
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pname.Add(reader[0].ToString());
                    }
                }
            }
            return pname;
        }

        public List<String> AdmittedParticpantNames(string ename,int stage)
        {
            List<string> pname = new List<string>();
            string database = Preferences.Get("Database", "NULL");
            string dbcmd = "use " + database;
            int admit_val = 1;
            string rcmd = "Select pname from event_details where ename='" + ename + "' AND stage=" + stage + " AND admit="+admit_val+" ";
            using (SqlConnection conec = new SqlConnection())
            {
                conec.ConnectionString = Preferences.Get("ConnectionString", "NULL");
                conec.Open();
                SqlCommand cmd1 = new SqlCommand(dbcmd, conec);
                SqlCommand cmd2 = new SqlCommand(rcmd, conec);
                cmd1.ExecuteNonQuery();
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pname.Add(reader[0].ToString());
                    }
                }
            }
            return pname;
        }

        public Classes.ParticipantTable GetParticipantInfo(string ename,int stage)
        {
            Classes.ParticipantTable pt = new Classes.ParticipantTable();
            List<string> pname = new List<string>();
            string database = Preferences.Get("Database", "NULL");
            string dbcmd = "use " + database;
            string rcmd = "select distinct particpant.pname ,particpant.pphone ,particpant.pno from particpant,event_details where event_details.pno=particpant.pno AND event_details.ename ='"+ename+"' AND event_details.stage ="+stage+";";
            using (SqlConnection conec = new SqlConnection())
            {
                conec.ConnectionString = Preferences.Get("ConnectionString", "NULL");
                conec.Open();
                SqlCommand cmd1 = new SqlCommand(dbcmd, conec);
                SqlCommand cmd2 = new SqlCommand(rcmd, conec);
                cmd1.ExecuteNonQuery();
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pt.pname.Add(reader[0].ToString());
                        pt.pphone.Add(reader[1].ToString());
                        pt.ppno.Add(int.Parse(reader[2].ToString()));
                    }
                }
            }
            return pt;
        }

          public void InsResult(Int64 first, Int64 second,Int64 third, string ename)
        {
            string database = Preferences.Get("Database", "NULL");
            string dbcmd = "use " + database;
            string icmd = "insert into result values('" + ename + "'," + first + "," + second + "," + third + ")";
            using(SqlConnection conec = new SqlConnection())
            {
                conec.ConnectionString = Preferences.Get("ConnectionString", "NULL");
                conec.Open();
                SqlCommand cmd1 = new SqlCommand(dbcmd, conec);
                SqlCommand cmd2 = new SqlCommand(icmd, conec);
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }
           
        }

        public string QualifyParticipant(Int64 pno,int CurrentStage,string ename)
        {
            string database = Preferences.Get("Database", "NULL");
            string email = null;
            string dbcmd = "use " + database;
            string ucmd = "update event_details set qualify=1 where pno=" + pno + "AND stage=" + CurrentStage + "";
            string mcmd = "select particpant.pmail from particpant,event_details where particpant.pno = event_details.pno AND event_details.ename='" + ename + "' AND event_details.stage =" + CurrentStage + " AND event_details.qualify=1  ";
            using(SqlConnection conec = new SqlConnection())
            {
                conec.ConnectionString = Preferences.Get("ConnectionString", "NULL");
                conec.Open();
                SqlCommand cmd1 = new SqlCommand(dbcmd, conec);
                SqlCommand cmd2 = new SqlCommand(ucmd, conec);
                SqlCommand cmd3 = new SqlCommand(mcmd, conec);
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                using(SqlDataReader reader = cmd3.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        email = reader[0].ToString();
                    }
                }
            }
            return email;
        }
       }
}
