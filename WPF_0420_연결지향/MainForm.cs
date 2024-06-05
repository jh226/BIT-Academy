using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0418_DB
{
    public partial class MainForm : Form
    {

        private WbDB1 db = WbDB1.Instance;
		private WbDB2 db2 = WbDB2.Instance;

		public Form_Select SelectForm { get; set; } = null;

        public MainForm()
        {
            InitializeComponent();
        }

        #region Form Load & Closed(DB연결 및 해제 처리)
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (db.Open() == true)
            {
                this.Text = "데이터 베이스에 연결되었습니다.....";
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (db.Close() == true)
            {
                this.Text = "데이터 베이스 연결이 종료되었습니다....";
            }
        }
        #endregion 


        #region 버튼 핸들러(기능)

        //저장
        private void button1_Click(object sender, EventArgs e)
        {
            Form_Insert frm = new Form_Insert();      
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string name     = frm.Member_Name;
                string phone    = frm.Member_Phone;
                int age         = frm.Member_Age;

                //테스트
                this.Text = name + ", " + phone + ", " + age;

                //DB전달
                if (db2.Insert_Member(name, phone, age) == false)
                    MessageBox.Show("저장 실패");

            }

        }

        //삭제
        private void button2_Click(object sender, EventArgs e)
        {
            Form_Delete frm = new Form_Delete();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string name = frm.Member_Name;

                //테스트
                this.Text = name;

                //DB전달
                if (db2.Delete_Member(name) == false)
                    MessageBox.Show("삭제 실패");

            }
        }

        //수정
        private void button3_Click(object sender, EventArgs e)
        {
            Form_Update frm = new Form_Update();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string name     = frm.Member_Name;
                string phone    = frm.Member_Phone;
                int age         = frm.Member_Age;

                //테스트
                this.Text = name + ", " + phone + ", " + age;

                //DB전달
                if (db2.Update_Member(name, phone, age) == false)
                    MessageBox.Show("수정 실패");
            }
        }

        //검색
        private void button4_Click(object sender, EventArgs e)
        {
            if (SelectForm == null)
            {
                SelectForm = new Form_Select();
                SelectForm.Main_Form = this;
                SelectForm.Show();
            }
            else
            {
                SelectForm.Focus();
            }
        }
        //모달리스(Select) 적용 호출
        
        public void Apply()
        {
            string name = SelectForm.Member_Name;

            //확인
            this.Text = name;

            //DB전달
            List<Member> members = db.SelectMember(name);
            if (members == null)
            {
                MessageBox.Show("검색 실패");
            }
            else
            {
                label2.Text = "이    름 : " + members[0].Name;
                label3.Text = "전화번호 : " + members[0].Phone;
                label4.Text = "나    이 : " + members[0].Age;
                label5.Text = "가입일자 : " + members[0].NetDateTime.ToShortDateString();
                label6.Text = "가입시간 : " + members[0].NetDateTime.ToShortTimeString();
            }
        }




		#endregion

		private void button5_Click(object sender, EventArgs e)
		{
            db2.FillTable();
            dataGridView1.DataSource = db2.dt;
		}

		private void button6_Click(object sender, EventArgs e)
		{
            db2.SQLUpdate();
		}
	}
}
