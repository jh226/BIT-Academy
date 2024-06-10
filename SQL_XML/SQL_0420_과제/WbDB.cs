using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _0420_미션
{
    internal class WbDB
    {
        #region 싱글톤
        private WbDB() { }
        public static WbDB Instance { get; private set; }
        static WbDB() { Instance = new WbDB(); }
        #endregion

        const string DB_NAME = "DESKTOP-0I86BTV\\SQLEXPRESS";
        const string DB_DATABALSE = "SampleDB";
        const string DB_ID = "wb37";

        const string DB_PW = "1234";

        private SqlConnection con = null;
        private SqlDataAdapter adapter = null;
        DataSet ds;
        DataTable Dt { get; set;}
        DataTable Senddt { get; set;}

        #region DB연결
        public DataSet Open()
        {
            string constr = string.Format(@"Data Source={0};Initial Catalog={1}; User ID={2};Password={3}"
                                , DB_NAME, DB_DATABALSE, DB_ID, DB_PW);
            string sql = "select * from TestAccount;";

            ds = new DataSet("Accounts");
            con = new SqlConnection(constr);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter = new SqlDataAdapter();

            adapter.SelectCommand = new SqlCommand(sql, con);
            adapter.Fill(ds, "account");
            adapter.Dispose();

            Dt = ds.Tables["account"];
            return ds;
        }
        #endregion

        #region 명령객체

        //저장, 수정, 삭제
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

        //하나 검색
        private object ExecuteScalar(string sql, SqlCommand cmd)
        {
            try
            {
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text; //default
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //다중 검색
        private List<Member> ExecuteReader(SqlCommand cmd, string sql)
        {
            List<Member> list = new List<Member>();

            try
            {
                cmd.CommandText = sql;
                SqlDataReader r = cmd.ExecuteReader(); //쿼리 실행 결과 저장할 객체 생성
                while (r.Read()) //남은 행이 있는 동안 반복 읽기
                {
                    list.Add(
                        new Member(int.Parse(r[0].ToString()),
                                    r[1].ToString(),
                                    int.Parse(r[2].ToString()),
                                    int.Parse(r[3].ToString()),
                                    DateTime.Parse(r[4].ToString())
                                    )
                        ); // 리스트에 추가
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

        #endregion

        #region 기능 (사용시 마다 수정되는 코드)

        //저장
        public bool InsertAccount(string name, int phone, int age)
        {
            DateTime dt = DateTime.Now;
            try
            {
                DataTable tempdt = ds.Tables["account"];
                DataRow dr = tempdt.NewRow();
                dr["name"] = name;
                dr["phone"] = phone;
                dr["age"] = age;
                dr["datetime"] = dt.ToString("yyyy-MM-dd HH:mm:ss");

                ds.Tables[0].Rows.Add(dr);

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to insert");
                return false;
            }
        }

        //삭제
        public bool DeleteAccount(string name)
        {
                         
        }

        //수정
        public bool UpdateAccount(string name, int phone, int age)
        {
            try
            {
                foreach(DataRow row1 in Dt.Rows)
                {
                    if (row1["name"].ToString() == name)
                    {
                        row1["phone"] = phone;
                        row1["age"] = age;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to Update");
                return false;
            }
        }

        //한개 검색
        public DataSet SelectAccount(string name)
        {
            string sql = String.Format("select * from Account where name = {0}", name);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //---파라미터 처리
            SqlParameter param_name = new SqlParameter("@NAME", name);
            cmd.Parameters.Add(param_name);
            //-----

            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(ds, "accounts");

            return ds;
        }
        #endregion
    }
}
