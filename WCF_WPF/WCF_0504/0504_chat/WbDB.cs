using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _0503_chat
{
    internal class WbDB
    {
        #region 싱글톤
        private WbDB() { }
        public static WbDB Instance { get; private set; }
        static WbDB() { Instance = new WbDB(); }
        #endregion

        const string DB_NAME = "DESKTOP-0I86BTV\\SQLEXPRESS";
        const string DB_DATABALSE = "Chat";
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
                // MessageBox.Show(ex.Message);
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
                //MessageBox.Show(ex.Message);
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
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
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
                //MessageBox.Show(ex.Message);
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
                    for (i = 0; i < r.FieldCount - 1; i++)
                    {
                        sb.Append(r[i] + "#");
                    }
                    sb.Append(r[i].ToString() + "@");
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                r.Close();
            }
        }

        #endregion

        #region Chat종속코드
        public bool UpdateMember(string id, int s_num, bool islogin)
        {
            string sql = string.Format(
                "update Member set is_login = '{0}', s_number = {2} where userid = '{1}';",
                                        islogin, id, s_num);
            return ExcuteNonQuery(sql);
        }

        public bool InsertLongmessage( string id, string msg)
        {
            string sql = string.Format(
                "insert into LongMessage values('{0}', '{1}', GETDATE());",
                id, msg);
            return ExcuteNonQuery(sql);
        }
        public bool InsertShortmessage(string id, string msg)
        {
            string sql = string.Format(
                "insert into ShortMessage values('{0}', '{1}', GETDATE());",
                id, msg);
            return ExcuteNonQuery(sql);
        }

        public bool InsertFileMessage(string id, string filename)
        {
            string sql = string.Format(
                "insert into LongMessage values('{0}', '{1}', GETDATE());",
                id, filename);
            return ExcuteNonQuery(sql);
        }

        /*
        public bool SaveImage(string filename, byte[] data, string id, bool shared)
        {
            string sql = string.Format(
                "insert into Image values(@FILENAME, GETDATE(), @USERID, @SHARD, @DATA);");

            SqlCommand cmd = scon.CreateCommand();//*********************************            

            //---파라미터 처리
            SqlParameter param_filename = new SqlParameter("@FILENAME", filename);
            cmd.Parameters.Add(param_filename);

            SqlParameter param_id = new SqlParameter("@USERID", id);
            cmd.Parameters.Add(param_id);

            SqlParameter param_shared = new SqlParameter("@SHARD", shared);
            param_shared.SqlDbType = System.Data.SqlDbType.Bit;
            cmd.Parameters.Add(param_shared);

            SqlParameter param_data = new SqlParameter("@DATA", data);
            param_data.SqlDbType = System.Data.SqlDbType.Image;
            cmd.Parameters.Add(param_data);
            //-----

            bool b = ExcuteNonQuery(cmd, sql);

            cmd.Dispose();      //*********************************

            return b;
        }

        public List<ImageData> GetImageList()
        {
            List<ImageData> imageDatas = new List<ImageData>();
            string sql = string.Format("select * from Image;");
            string str = ExecuteReader(sql);

            string[] sp1 = str.Split('@');
            for (int i = 0; i < sp1.Length - 1; i++)
            {
                string[] sp2 = sp1[i].Split('#');
                ImageData imagedata = new ImageData(
                    sp2[1],
                    Encoding.UTF8.GetBytes(sp2[5]),
                    sp2[3],
                    DateTime.Parse(sp2[2].ToString()));
                imageDatas.Add(imagedata);
            }
            return imageDatas;
        }

        public ImageData GetImage(string filename)
        {
            string sql = string.Format("select * from Image where filename='{0}';", filename);
            ImageData data = ExecuteReader1(sql);
            return data;
        }
        */
        #endregion
    }
}
