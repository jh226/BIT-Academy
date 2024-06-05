using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace _0418_DB
{
	internal class WbDB2
	{
		#region 싱글톤
		public WbDB2() { }
		public static WbDB2 Instance { get; private set; }
		static WbDB2() { Instance = new WbDB2(); }
		#endregion
		public DataTable Member_Table { get; set; } = null;


		const string DB_NAME = "DESKTOP-ELKJL3J\\SQLEXPRESS";
		const string DB_DATABALSE = "SampleDB";
		const string DB_ID = "wb37_1";
		const string DB_PW = "1234";

		string constr = string.Format("Data Source= {0};Initial Catalog={1}; User ID= {2}; Password={3}", DB_NAME, DB_DATABALSE, DB_ID, DB_PW);
		string comtxt_member = "select * from Member;";
		DataSet ds = null;
		public DataTable dt {get; set;}
		public DataTable senddt{get; set;}

		SqlDataAdapter adapter = null;
		public void FillTable()
		{
			ds = new DataSet("Library");

			using (SqlConnection con = new SqlConnection(constr))
			{
				adapter = new SqlDataAdapter();
				adapter.SelectCommand = new SqlCommand(comtxt_member, con);
				adapter.Fill(ds, "Member");
				dt = ds.Tables["Member"];
			}

		}

		public bool Insert_Member(string name, string phone, int age)
		{
			try
			{
				DataTable tempdt = dt;
				DataRow dr = tempdt.NewRow();

				dr["name"] = name;
				dr["phone"] = phone;
				dr["age"] = age;
				dr["newdate"] = DateTime.Now;
				dr["datetime"] = DateTime.Now.TimeOfDay;

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

		public bool Update_Member(string name, string newPhone, int newAge)
		{
			try
			{
				foreach (DataRow dr in dt.Rows)
				{
					if (dr["name"].ToString() == name)
					{
						dr["phone"] = newPhone;
						dr["age"] = newAge;
					}
				}
				return true;
			}
			catch (Exception es)
			{
				Console.WriteLine("Failed to update Member for {0}", name);
				Console.WriteLine("Reason: {0}", es.Message);
				return false;
			}
		}
		public bool Delete_Member(string name)
		{
			try
			{
				foreach (DataRow dr in dt.Rows)
				{
					if (dr["name"].ToString() == name)
					{
						dr.Delete();
						return true;
					}
				}
				Console.WriteLine("Member {0} not found", name);
				return false;


			}
			catch (Exception ea)
			{
				Console.WriteLine("Failed to delete Member for {0}", name);
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

			adapter.UpdateCommand.Connection= con;

			adapter.Update(ds,"Member");
		}

		public SqlCommand MakeUpdateCommand(SqlConnection con)
		{
			string cmd_txt = "update Member set phone = @phone, age = @age where (name = @name)";
			SqlCommand cmd = new SqlCommand(cmd_txt, con);
			cmd.Parameters.Add("@name", SqlDbType.VarChar, 50, "name");
			cmd.Parameters.Add("@phone", SqlDbType.VarChar, 20, "phone");
			cmd.Parameters.Add("@age", SqlDbType.Int, 4, "age");

			return cmd;
		}

		public SqlCommand MakeInsertCommand(SqlConnection con)
		{
			string cmd_txt = "insert into Member values (@name ,@phone,@age, @newdate, @datetime)";
			SqlCommand cmd = new SqlCommand(cmd_txt, con);
			cmd.Parameters.Add("@name", SqlDbType.VarChar, 50, "name");
			cmd.Parameters.Add("@phone", SqlDbType.VarChar, 20, "phone");
			cmd.Parameters.Add("@age", SqlDbType.Int, 4, "age");
			cmd.Parameters.Add("@newdate", SqlDbType.Date, 3, "newdate");
			cmd.Parameters.Add("@datetime", SqlDbType.Time, 5, "datetime");
			return cmd;
		}
		public SqlCommand MakeDeleteCommand(SqlConnection con)
		{
			string cmd_txt = "delete from Member where (name = @name)";
			SqlCommand cmd = new SqlCommand(cmd_txt, con);
			cmd.Parameters.Add("@name", SqlDbType.VarChar, 50, "name");
			return cmd;
		}

	}
}
