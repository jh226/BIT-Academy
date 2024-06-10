using _0418_DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0418_DB
{
	internal class WbDB1
	{
		#region 싱글톤
		private WbDB1() { }
		public static WbDB1 Instance { get; private set; }
		static WbDB1() { Instance = new WbDB1(); }
		#endregion

		const string DB_NAME = "DESKTOP-ELKJL3J\\SQLEXPRESS";
		const string DB_DATABALSE = "SampleDB";
		const string DB_ID = "wb37_1";
		const string DB_PW = "1234";

		private SqlConnection scon = null;

		#region DB연결 및 해제
		public bool Open()
		{
			try
			{
				string constr =
					string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
										DB_NAME, DB_DATABALSE, DB_ID, DB_PW);

				scon = new SqlConnection(constr);
				scon.Open(); //연결 열기   

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public bool Close()
		{
			Console.WriteLine(scon.WorkstationId);
			Console.WriteLine(scon.ServerVersion);
			Console.WriteLine(scon.PacketSize);
			Console.WriteLine(scon.ConnectionTimeout);
			Console.WriteLine(scon.Database);
			Console.WriteLine(scon.DataSource);
			Console.WriteLine(scon.State);
			try
			{
				scon.Close(); //연결 닫기
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		#endregion

		#region 명령객체

		// 저장 , 수정 , 삭제
		private bool ExcuteNonQuery(SqlCommand cmd, string sql)
		{
			try
			{
				cmd.CommandText = sql;
				if (cmd.ExecuteNonQuery() <= 0)
					return false;
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		// 하나 검색
		//private object ExecuteScalar(string sql)
		//{
		//    try
		//    {
		//        cmd.CommandText = sql;
		//        cmd.CommandType = System.Data.CommandType.Text; //default
		//        return cmd.ExecuteScalar();
		//    }
		//    catch (Exception ex)
		//    {
		//        MessageBox.Show(ex.Message);
		//        return null;
		//    }
		//}

		private List<Member> ExecuteReader(SqlCommand cmd, string sql)
		{
			List<Member> list = new List<Member>();
			try
			{
				cmd.CommandText = sql;
				SqlDataReader r = cmd.ExecuteReader();
				while (r.Read())
				{
					list.Add(
						new Member(r[0].ToString(), r[1].ToString(), int.Parse(r[2].ToString()), DateTime.Parse(r[3].ToString()))
					);
				}
				r.Close();
				return list;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
		}

		//저장
		public bool InsertMember(string name, string phone, int age)
		{

			string sql = "InsertMember";
			SqlCommand cmd = scon.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			DateTime dt = DateTime.Now;  //현재 날짜와 시간

			SqlParameter param_name = new SqlParameter("@NAME", name);
			cmd.Parameters.Add(param_name);

			SqlParameter param_phone = new SqlParameter("@PHONE", phone);
			cmd.Parameters.Add(param_phone);

			SqlParameter param_age = new SqlParameter("@AGE", age);
			param_age.SqlDbType = System.Data.SqlDbType.Int;
			cmd.Parameters.Add(param_age);

			SqlParameter param_date = new SqlParameter("@NEWDATE", dt.ToShortDateString());
			cmd.Parameters.Add(param_date);

			SqlParameter param_time = new SqlParameter("@DATETIME", dt.ToString("HH:mm:ss"));
			cmd.Parameters.Add(param_time);

			bool b = ExcuteNonQuery(cmd, sql);
			cmd.Dispose();
			return b;
		}

		//삭제
		public bool DeleteMember(string name)
		{
			string sql = "DeleteMember";
			SqlCommand cmd = scon.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			SqlParameter param_name = new SqlParameter("@Name", name);
			cmd.Parameters.Add(param_name);

			bool b = ExcuteNonQuery(cmd, sql);
			cmd.Dispose();
			return b;
		}

		//수정
		public bool UpdateMember(string name, string phone, int age)
		{
			string sql = "UpdateMember";
			SqlCommand cmd = scon.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			SqlParameter param_name = new SqlParameter("@Name", name);
			cmd.Parameters.Add(param_name);

			SqlParameter param_phone = new SqlParameter("@Phone", phone);
			cmd.Parameters.Add(param_phone);

			SqlParameter param_age = new SqlParameter("@Age", age);
			param_age.SqlDbType = System.Data.SqlDbType.Int;
			cmd.Parameters.Add(param_age);

			bool b = ExcuteNonQuery(cmd, sql);
			cmd.Dispose();
			return b;
		}

		public List<Member> SelectMember(string name)
		{
			string sql = "SelectMember";
			SqlCommand cmd = scon.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			SqlParameter param_name = new SqlParameter("@Name", name);
			cmd.Parameters.Add(param_name);

			return ExecuteReader(cmd, sql);
		}

		public List<Member> SelectAllMember()
		{
			string sql = "SelectMemberAll";
			SqlCommand cmd = scon.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			return ExecuteReader(cmd, sql);
		}


		#endregion
	}
}