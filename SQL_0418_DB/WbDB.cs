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
        static WbDB() { Instance = new WbDB();  }
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
        private List<Account> ExecuteReader(string sql)
        {
            List<Account> list = new List<Account>();

            try
            {
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text; //default
                SqlDataReader r = cmd.ExecuteReader();
                while(r.Read())
                {
                    list.Add(
                        new Account( int.Parse(r[0].ToString()),
                        r[1].ToString(), 
                        int.Parse(r[2].ToString()),
                        DateTime.Parse(r[3].ToString())));
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
        //insert into Accountvalues('김길동', 1000, '2023-4-18')
        public bool InsertAccount(string name, int money)
        {
            DateTime dt = DateTime.Now;

            string sql = string.Format("insert into Account values('{0}', '{1}', '{2}');",
                name, money, dt.ToShortDateString());
            
            return ExcuteNonQuery(sql);
        }

        //select max(accid) from account where name = '김길동';
        public int LastInsertAccid(string name)
        {
            string sql = string.Format("select max(accid) from account where name = '{0}';", name);
            try
            {
                object obj = ExecuteScalar(sql);
                return (int)obj;
            }
            catch(Exception)
            {
                return 0;
            }          
        }

        public List<Account> SelectAllAccount ()
        {
            string sql = string.Format("select * from Account;");

            return ExecuteReader(sql);
        }

        //delete from account where accid = 1010;
        public bool DeleteAccount(int accid)
        {
            string sql = string.Format("delete from account where accid = {0};", accid);

            return ExcuteNonQuery(sql);
        }

        //update account set name = '이름변경' where accid = 1030;
        public bool UpdateAccount(int accid, string upname)
        {
            string sql = string.Format(
                "update account set name = '{0}' where accid = {1};", 
                upname, accid);

            return ExcuteNonQuery(sql);
        }

        //update account set balance = balance + 1000 where accid = 1030;
        public bool UpdateInputAccount(int accid, int money)
        {
            string sql = string.Format(
                "update account set balance = balance + '{0}' where accid = {1};",
                money, accid);

            return ExcuteNonQuery(sql);
        }

        public bool UpdateOutputAccount(int accid, int money)
        {
            string sql = string.Format(
                "update account set balance = balance - '{0}' where accid = {1};",
                money, accid);

            return ExcuteNonQuery(sql);
        }

        #endregion 
    }
}
