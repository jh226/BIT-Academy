using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0421
{
    internal class WbDB
    {
        #region 싱글톤
        public WbDB() { }
        public static WbDB Instance { get; private set; }
        static WbDB() { Instance = new WbDB(); }
        #endregion
        public DataTable Member_Table { get; set; } = null;


        const string DB_NAME = "DESKTOP-0I86BTV\\SQLEXPRESS";
        const string DB_DATABALSE = "SampleDB";
        const string DB_ID = "wb37";
        const string DB_PW = "1234";

        string constr = string.Format("Data Source= {0};Initial Catalog={1}; User ID= {2}; Password={3}", DB_NAME, DB_DATABALSE, DB_ID, DB_PW);
        string comtxt_member = "select * from Image;";
        DataSet ds = null;
       
        public DataTable dt { get; set; }
        public DataTable senddt { get; set; }

        SqlDataAdapter adapter = null;

        public void FillTable()
        {
            ds = new DataSet("Image");

            using (SqlConnection con = new SqlConnection(constr))
            {
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(comtxt_member, con);
                adapter.Fill(ds, "Image");
                dt = ds.Tables["Image"];
            }

        }

        public bool Insert_Image(string title, byte[] data, int size)
        {
            try
            {
                DataTable tempdt = dt;
                DataRow dr = tempdt.NewRow();

                dr["title"] = title;
                dr["data"] = data;
                dr["size"] = size;

                tempdt.Rows.Add(dr);
                senddt = tempdt;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to insert Member for {0}", title);
                Console.WriteLine("Reason: {0}", e.Message);
                return false;
            }
        }

        public bool Update_Image(string title, byte[] data, int size)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["title"].ToString() == title)
                    {
                        dr["data"] = data;
                        dr["size"] = size;
                    }
                }
                return true;
            }
            catch (Exception es)
            {
                Console.WriteLine("Failed to update Image for {0}", title);
                Console.WriteLine("Reason: {0}", es.Message);
                return false;
            }
        }
        public bool Delete_Member(string title)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["title"].ToString() == title)
                    {
                        dr.Delete();
                        return true;
                    }
                }
                Console.WriteLine("Image {0} not found", title);
                return false;


            }
            catch (Exception ea)
            {
                Console.WriteLine("Failed to delete Image for {0}", title);
                Console.WriteLine("Reason: {0}", ea.Message);
                return false;
            }
        }

        public void SQLUpdate()
        {            
            SqlConnection con = new SqlConnection(constr);
           
            adapter.UpdateCommand = MakeUpdateCommand(con);
            adapter.DeleteCommand = MakeDeleteCommand(con);
            adapter.InsertCommand = MakeInsertCommand(con);

            adapter.UpdateCommand.Connection = con;

            adapter.Update(ds, "Image");
        }

        public SqlCommand MakeUpdateCommand(SqlConnection con)
        {
            string cmd_txt = "update Image set data = @data, size = @size where (title = @title)";
            SqlCommand cmd = new SqlCommand(cmd_txt, con);
            cmd.Parameters.Add("@title", SqlDbType.VarChar, 50, "title");
            cmd.Parameters.Add("@data", SqlDbType.Binary, 20, "data");
            cmd.Parameters.Add("@size", SqlDbType.Int, 4, "size");

            return cmd;
        }

        public SqlCommand MakeInsertCommand(SqlConnection con)
        {
            string cmd_txt = "insert into Image values (@title ,@data,@size)";
            SqlCommand cmd = new SqlCommand(cmd_txt, con);
            cmd.Parameters.Add("@title", SqlDbType.VarChar, 50, "title");
            cmd.Parameters.Add("@data", SqlDbType.Binary, 20, "data");
            cmd.Parameters.Add("@size", SqlDbType.Int, 4, "size");
            return cmd;
        }

        public SqlCommand MakeDeleteCommand(SqlConnection con)
        {
            string cmd_txt = "delete from Image where (title = @title)";
            SqlCommand cmd = new SqlCommand(cmd_txt, con);
            cmd.Parameters.Add("@title", SqlDbType.VarChar, 50, "title");
            return cmd;
        }
    }
}
