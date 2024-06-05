using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0502_ImageClient
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

        #endregion

        #region(image)에 종속된 코드

        public bool InsertImage(string filename, string id, string share, byte[] data)
        {
            string sql = string.Format(
                "insert into image1 values('{0}', GETDATE(), '{1}', '{2}', CAST('{3}' as varBinary(MAX)));",
                filename, id, share, data);
            return ExcuteNonQuery(sql);
            
        }

        #endregion
    }
}
