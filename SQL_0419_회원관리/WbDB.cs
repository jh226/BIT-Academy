using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _0418_DB
{
    internal class WbDB
    {
        #region 싱글톤
        private WbDB() { }
        public static WbDB Instance { get; private set; }
        static WbDB() { Instance = new WbDB(); }
        #endregion

        const string DB_NAME        = "DESKTOP-0I86BTV\\SQLEXPRESS";
        const string DB_DATABALSE   = "SampleDB";
        const string DB_ID          = "wb37";
        const string DB_PW          = "1234";

        private SqlConnection scon = null;
        private SqlCommand cmd     = null;

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        #endregion

        #region 명령객체

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
        private List<Member> ExecuteReader(string sql)
        {
            List<Member> list = new List<Member>();

            try
            {
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text; //default
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
            
            string sql = string.Format("insert into TestAccount values('{0}', '{1}', '{2}', '{3}');",
                name, phone, age, dt.ToString("yyyy-MM-dd HH:mm:ss"),dt.ToShortDateString());
            
            return ExcuteNonQuery(sql);
        }

        //삭제
        public bool DeleteAccount(string name)
        {
            string sql = string.Format("delete from TestAccount where name = '{0}';", name);

            return ExcuteNonQuery(sql);
        }

        //수정
        public bool UpdateAccount(string name, int phone, int age)
        {
            string sql = string.Format(
                "update Testaccount set phone = {0}, age = {1} where name = '{2}';",
                phone, age, name);

            return ExcuteNonQuery(sql);
        }

        //한개 검색
        public List<Member> SelectAccount(string name)
        {
            string sql = string.Format("select * from TestAccount where name = '{0}';", name);

            return ExecuteReader(sql);
        }

        //전체 검색
        public List<Member> SelectAllAccount()
        {
            string sql = string.Format("select * from TestAccount;");

            return ExecuteReader(sql);
        }
        #endregion
    }
}
