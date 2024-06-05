using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0418_DB
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

        private SqlConnection scon = null;
        private SqlCommand cmd = null;

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

                cmd = scon.CreateCommand();
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

        #region 명령객체(수정없이 사용)

        //저장, 수정, 삭제
        private bool ExcuteNonQuery(string sql)
        {
            try
            {
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text; //default
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
        private object ExecuteScalar(string sql)
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
        private string ExecuteReader(string sql)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader r = null;
            try
            {
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text; //default
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int i;
                    for(i=0; i< r.FieldCount-1; i++)
                    {
                        sb.Append(r[i].ToString() + "#");
                    }
                    sb.Append(r[i].ToString() + "@");
                }                
                return sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                r.Close();
            }
        }


        #endregion

        #region(Member)에 종속된 코드
        public bool InsertMember(string name, string phone, int age)
        {
            string sql = string.Format(
                "insert into Member values('{0}', '{1}', {2}, GETDATE());",
                name, phone, age);
            return ExcuteNonQuery(sql);
        }

        public bool DeleteMember(string name)
        {
            string sql = string.Format(
                "delete from Member where name = '{0}';", name);

            return ExcuteNonQuery(sql);
        }
        
        public bool UpdateMember(string name, string phone, int age)
        {
            string sql = string.Format(
                "update Member set phone = '{0}', age = {1} where name = '{2}';",
                                        phone, age, name);

            return ExcuteNonQuery(sql);
        }

        public List<Member> SelectMember(string name)
        {
            List<Member> members = new List<Member>();
            string sql = string.Format("select * from Member where name = '{0}';", name);
            string str = ExecuteReader(sql);

            string[] sp1 = str.Split('@');
            for(int i=0; i<sp1.Length-1; i++)
            {
                string[] sp2 = sp1[i].Split('#');
                Member member = new Member(
                    sp2[0],
                    sp2[1],
                    int.Parse(sp2[2].ToString()),
                    DateTime.Parse(sp2[3].ToString()));

                members.Add(member);
            }
            return members;
        }
                
        public List<Member> SelectAllMember()
        {
            List<Member> members = new List<Member>();
            string sql = string.Format("select * from Member;");
            string str = ExecuteReader(sql);

            string[] sp1 = str.Split('@');
            for (int i = 0; i < sp1.Length - 1; i++)
            {
                string[] sp2 = sp1[i].Split('#');
                Member member = new Member(
                    sp2[0],
                    sp2[1],
                    int.Parse(sp2[2].ToString()),
                    DateTime.Parse(sp2[3].ToString()));

                members.Add(member);
            }
            return members;
        }

        #endregion
    }
}
