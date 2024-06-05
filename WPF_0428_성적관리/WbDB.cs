using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0428_성적
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
        string comtxt_member = "select * from Student;";
        DataSet ds = null;
        public DataTable dt { get; set; }
        public DataTable senddt { get; set; }

        SqlDataAdapter adapter = null;
        public void FillTable()
        {
            ds = new DataSet("Student");

            using (SqlConnection con = new SqlConnection(constr))
            {
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(comtxt_member, con);
                adapter.Fill(ds, "Student");
                dt = ds.Tables["Student"];
            }

        }

        public bool Insert_Student(string name, int id, string subject, string grade)
        {
            try
            {
                DataTable tempdt = dt;
                DataRow dr = tempdt.NewRow();

                dr["name"] = name;
                dr["id"] = id;
                dr["subject"] = subject;
                dr["grade"] = grade;

                tempdt.Rows.Add(dr);
                senddt = tempdt;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to insert Member for {0}", name);
                Console.WriteLine("Reason: {0}", e.Message);
                return false;
            }
        }

        public bool Update_Student(int id, string newgrade)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if ((int)dr["id"] == id)
                    {
                        dr["grade"] = newgrade;
                    }
                }
                return true;
            }
            catch (Exception es)
            {
                Console.WriteLine("Failed to update Member for {0}", id);
                Console.WriteLine("Reason: {0}", es.Message);
                return false;
            }
        }

        public bool Delete_Student(string id)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["id"].ToString() == id)
                    {
                        dr.Delete();
                        return true;
                    }
                }
                Console.WriteLine("Member {0} not found", id);
                return false;


            }
            catch (Exception ea)
            {
                Console.WriteLine("Failed to delete Member for {0}", id);
                Console.WriteLine("Reason: {0}", ea.Message);
                return false;
            }
        }

        public void SQLUpdate()
        {
            string constr = string.Format("Data Source= {0};Initial Catalog={1}; User ID= {2}; Password={3}", DB_NAME, DB_DATABALSE, DB_ID, DB_PW);
            SqlConnection con = new SqlConnection(constr);
            adapter.UpdateCommand = MakeUpdateCommand(con);
            adapter.DeleteCommand = MakeDeleteCommand(con);
            adapter.InsertCommand = MakeInsertCommand(con);

            adapter.UpdateCommand.Connection = con;

            adapter.Update(ds, "Student");
        }


        public SqlCommand MakeUpdateCommand(SqlConnection con)
        {
            string cmd_txt = "update Student set grade = @grade where (id = @id)";
            SqlCommand cmd = new SqlCommand(cmd_txt, con);
            cmd.Parameters.Add("@id", SqlDbType.Int, 4, "id");
            cmd.Parameters.Add("@grade", SqlDbType.VarChar, 50, "grade");

            return cmd;
        }

        public SqlCommand MakeInsertCommand(SqlConnection con)
        {
            string cmd_txt = "insert into Student values (@name ,@id,@subject, @grade)";
            SqlCommand cmd = new SqlCommand(cmd_txt, con);
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 50, "name");
            cmd.Parameters.Add("@id", SqlDbType.Int, 4, "id");
            cmd.Parameters.Add("@subject", SqlDbType.VarChar, 50, "subject");
            cmd.Parameters.Add("@grade", SqlDbType.VarChar, 50, "grade");
            return cmd;
        }
        public SqlCommand MakeDeleteCommand(SqlConnection con)
        {
            string cmd_txt = "delete from Member where (id = @id)";
            SqlCommand cmd = new SqlCommand(cmd_txt, con);
            cmd.Parameters.Add("@id", SqlDbType.Int, 4, "id");
            return cmd;
        }
    }
}
