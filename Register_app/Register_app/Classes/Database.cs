using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Register_app.Properties;
namespace Register_app
{
    public class Database
    {
        public List<String> GetDBList()
        {
            string ConnectionString = Register_app.Properties.Settings.Default.ConnectionString;
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
            string ConnectionString = Register_app.Properties.Settings.Default.ConnectionString;
            using (SqlConnection conec = new SqlConnection(ConnectionString))
            {
                conec.Open();
                using (SqlCommand cmd = new SqlCommand("use " + dbname + "", conec))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> TableList()
        {
            string connectionstring = Register_app.Properties.Settings.Default.ConnectionString;
            string database = Properties.Settings.Default.Database;
            string cmd1 = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            List<string> tablelist = new List<string>();
            using (SqlConnection conec = new SqlConnection(connectionstring))
            {
                conec.Open();
                SqlCommand cmdb = new SqlCommand("use " + database, conec);
                cmdb.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand(cmd1, conec);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tablelist.Add(reader[0].ToString());
                    }
                }
            }
            return tablelist;
        }
    }

       

}
