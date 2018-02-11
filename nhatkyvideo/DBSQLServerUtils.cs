using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace nhatkyvideo
{
    public class DBSQLServerUtils
    {
        public static SqlConnection
              GetDBConnection()
        {
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\DPT\nhatkyvideo\nhatkyvideo\nhatkyvideo\data\nhatkivideo.mdf;Integrated Security=True;Connect Timeout=30
            // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
            //
            // string connString = @"Data Source=" + datasource + ";Initial Catalog="
            //          + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\DPT\nhatkyvideo\nhatkyvideo\nhatkyvideo\data\nhatkivideo.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }
        public static DataTable getdata(String sql)
        {
            SqlConnection cnn = GetDBConnection();
            SqlCommand com = new SqlCommand(sql, cnn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cnn.Close();
            return dt;
        }
        public static void setdata(String sql)
        {
            SqlConnection cnn = GetDBConnection();
            SqlCommand com = new SqlCommand(sql, cnn);
            com.CommandType = CommandType.Text;
            cnn.Close();
        }
    }
}
