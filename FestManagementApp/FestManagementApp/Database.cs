using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using FestManagementApp.Classes;

namespace FestManagementApp
{
   public class Database
    {
        public List<String> GetDBList()
        {
            string ConnectionString = Properties.Settings.Default.ConnectionString;
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

        public void CheckDB(string dbname)
        {
            string ConnectionString = Properties.Settings.Default.ConnectionString;
            using (SqlConnection conec = new SqlConnection(ConnectionString))
            {
                conec.Open();
                using (SqlCommand cmd = new SqlCommand("use "+dbname+"", conec))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> TableList()
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            string cmd1 = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            List<string> tablelist = new List<string>();
            using (SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand(cmd1, conec);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        tablelist.Add(reader[0].ToString());
                    }
                }
            }
            return tablelist;
        }

        public DataTable GetTable(string tablename)
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            string cmd1 = "SELECT * FROM " + tablename + ""; 
            DataTable dt = new DataTable();
            using(SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand(cmd1, conec);
                using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GetTable(string tablename,DataTable dx)
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            string cmd1 = "SELECT * FROM " + tablename + "";
            DataTable dt = new DataTable();
            using (SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand(cmd1, conec);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Fill(dt);

                    if(dx.Rows.Count>dt.Rows.Count)
                    {
                        for(int i=0;i<(dx.Rows.Count-dt.Rows.Count)+1;++i)
                        {
                            dt.Rows.Add();
                        }
                      
                    }
                    else
                    {
                        // deletion
                    }
                   
                    for (int i=0;i<dx.Rows.Count;++i)
                    {
                       
                            
                        for(int j=0;j<dx.Columns.Count;++j)
                        {
                            dt.Rows[i][j] = dx.Rows[i][j];
                        }
                    }

                    adapter.UpdateCommand = sqlCommandBuilder.GetUpdateCommand();
                    adapter.Update(dt);
                  
                   
                }
            }

            return dx;
        }

        public void RemoveRow(string tablename,int index)
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            string cmd1 = "SELECT * FROM " + tablename + "";
            DataTable dt = new DataTable();
            using (SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand(cmd1, conec);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    
                    //   dataAdapt.UpdateCommand = cb.GetUpdateCommand(true);
                    //   dataAdapt.InsertCommand = cb.GetInsertCommand(true);
                    adapter.Fill(dt);
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dt.Rows[i];
                        if (i == index)
                            dr.Delete();
                       
                    }

                    adapter.DeleteCommand = sqlCommandBuilder.GetDeleteCommand(true);
                    adapter.UpdateCommand = sqlCommandBuilder.GetUpdateCommand();
                    adapter.Update(dt);
                }
            }
        }

        public List<string> GetCollegeEmail()
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            string cmd1 = "SELECT cmail FROM college";
            List<string> emaillist = new List<string>();
            using (SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmd = new SqlCommand(cmd1, conec);
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        emaillist.Add(reader[0].ToString());
                    }
                }

            }
            return emaillist;
        }

        public void InsertIntoTable(string inscmd)
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            using(SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand(inscmd, conec);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable DisplayTable(string tablename)
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            DataTable dt = new DataTable();
            using (SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand("Select * from " + tablename, conec);
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                {
                    sqlDataAdapter.Fill(dt);
                }

            }
            return dt;
        }

        public List<string> Geteventlist()
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            List<string> eventlist = new List<string>();
            using(SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand("Select ename from event" , conec);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        eventlist.Add(reader[0].ToString());
                    }
                }
            }
            return eventlist;
        }

        public void CreateCordAccount(string coname, string ename)
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            using (SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand createlogin = new SqlCommand(" use "+database+" ; CREATE LOGIN " + coname + " WITH PASSWORD='" + coname + "'; use fest ; CREATE USER " + coname + " FOR LOGIN " + coname + ";", conec);
                createlogin.ExecuteNonQuery();
                if (ename == "Registration")
                {
                    //new SqlCommand("grant select on event to @dev; grant insert, select, update on particpant to @dev; grant insert, select on grp_mem_details to @dev; grant insert, select on event_details to @dev;  grant select, update on coordinator to @dev; ");
                    using(SqlCommand setpermissions = new SqlCommand())
                    {
                        setpermissions.Connection = conec;
                        setpermissions.CommandType = CommandType.Text;
                        setpermissions.CommandText = "use " + database + "; grant select on event to " + coname+" ; grant insert, select, update on particpant to "+coname+" ; grant insert, select on grp_mem_details to "+coname+" ; grant insert, select on event_details to "+coname+" ;  grant select, update on coordinator to "+coname+ " ; grant select on college to " + coname + "  ";
                        setpermissions.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (SqlCommand setpermissions = new SqlCommand())
                    {
                        //grant select on event to dev;grant select, insert, update, delete on event_details to dev; grant select on particpant to dev; grant insert, update on result to dev;

                        setpermissions.Connection = conec;
                        setpermissions.CommandType = CommandType.Text;
                        setpermissions.CommandText = "use " + database + "; grant select on event to " + coname + " ; grant insert, select, update on event_details to " + coname + " ; grant insert, select on grp_mem_details to " + coname + " ; grant select on particpant to " + coname + " ;  grant insert, update on result to " + coname + " ; grant update on coordinator to " + coname + "  ";
                        setpermissions.ExecuteNonQuery();
                    }
                }
                
            }
        }
        public void Delaccnt(string usrname)
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            using (SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand delusr = new SqlCommand("use " + database + "; DROP USER " + usrname + "",conec);
                delusr.ExecuteNonQuery();
                SqlCommand delogin = new SqlCommand("use " + database + "; DROP LOGIN " + usrname + "", conec);
                delogin.ExecuteNonQuery();
            }
        }

        public int participantcount()
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            int cnt = -1;
            using(SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand cmdc = new SqlCommand("select count(pname) from particpant", conec);
                using(SqlDataReader reader = cmdc.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        cnt =int.Parse( reader[0].ToString());
                    }
                }
                
            }
            return cnt;
        }

        public List<StatModel> popcollege(string columnname,string tablename)
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            List<StatModel> itemlist = new List<StatModel>();
            
            using (SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
              
                cmdb.ExecuteNonQuery();
                SqlCommand cmdc = new SqlCommand("SELECT count("+columnname+"),  "+columnname+" FROM "+tablename+" GROUP BY "+columnname+" HAVING COUNT("+columnname+") >= 1 order by count("+columnname+") DESC; ", conec);
                using (SqlDataReader reader = cmdc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StatModel item = new StatModel();
                        item.Count = int.Parse(reader[0].ToString());
                        item.Itemname = reader[1].ToString();
                        itemlist.Add(item);
                    }
                }
               
            }
            foreach(StatModel stat in itemlist)
            {
                System.Diagnostics.Debug.WriteLine(stat.Itemname);
            }
            return itemlist;

           
        }

        public List<Results> getResults()
        {
            string connectionstring = Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            List<Results> res = new List<Results>(); 
            using (SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();

                SqlCommand cmdr = new SqlCommand("select * from result",conec);
                using(SqlDataReader reader = cmdr.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Results entry = new Results();
                        entry.Ename = reader[0].ToString();
                        entry.first = reader[1].ToString();
                        entry.second = reader[2].ToString();
                        entry.third = reader[3].ToString();
                        res.Add(entry);
                    }
                }
                foreach(Results ent in res)
                {
                    SqlCommand cmdf = new SqlCommand("select pname from particpant where pno ="+ent.first+"",conec);
                    ent.first = (string)cmdf.ExecuteScalar();
                    SqlCommand cmds = new SqlCommand("select pname from particpant where pno =" + ent.second + "",conec);
                    ent.second = (string)cmds.ExecuteScalar();
                    SqlCommand cmdt = new SqlCommand("select pname from particpant where pno =" + ent.third + "",conec);
                    ent.third = (string)cmdt.ExecuteScalar();
                }
            }

            return res;

        }

       

    }

}
