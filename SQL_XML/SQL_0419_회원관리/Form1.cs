using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _0418_DB
{
    public partial class Form1 : Form
    {
        public Select Select { get; set; } = null;
        public Form1()
        {
            InitializeComponent();

            listView1.View = View.Details;           //컬럼형식으로 변경            

            
        }

        #region DB연결
        private void Form1_Load(object sender, EventArgs e)
        {
            if (WbDB.Instance.Open() == true)
            {
                this.Text = "DB연결....";
                SelectALlAccount();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WbDB.Instance.Close() == true)
            {
                this.Text = "DB연결 해제....";
            }
        }
        #endregion

        //1) 저장
        private void button4_Click_1(object sender, EventArgs e)
        {
            Insert insert = new Insert();
            insert.Text = "저장";
            DialogResult result = insert.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (WbDB.Instance.InsertAccount( insert.Mem_Name, insert.Mem_Phone, 
                    insert.Mem_Age) == false)
                    MessageBox.Show("저장 실패");
            }

            else if (result == DialogResult.Cancel)
            {
                MessageBox.Show("저장 취소");
            }
            SelectALlAccount();
        }

        //2) 삭제
        private void button5_Click(object sender, EventArgs e)
        {
            Delete delete = new Delete();
            delete.Text = "삭제";
            DialogResult result = delete.ShowDialog();
            this.Text = delete.Mem_Name;

            if (result == DialogResult.OK)
            {
                if (WbDB.Instance.DeleteAccount(delete.Mem_Name) == true)
                    MessageBox.Show("삭제");
                else
                    MessageBox.Show("실패");
            }
            SelectALlAccount();
        }
        //3) 수정
        private void button6_Click(object sender, EventArgs e) 
        {
            Insert update = new Insert();
            update.Text = "수정";
            DialogResult result = update.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.Text = update.Mem_Name +" "+ update.Mem_Phone + " "+ update.Mem_Age;
                if (WbDB.Instance.UpdateAccount(update.Mem_Name, update.Mem_Phone,
                    update.Mem_Age) == false)
                    MessageBox.Show("수정 실패");
            }
            SelectALlAccount();
        }

        //4) 검색
        private void button7_Click(object sender, EventArgs e)
        {
            if (Select == null)
            {
                Select = new Select();
                Select.Parent_From = this;
                Select.Show();
            }
            else
            {
                Select.Focus();
            }
        }

        //모달리스 -> 검색
        public void select_acc()
        {
            List<Member> mem = WbDB.Instance.SelectAccount(Select.Mem_Name);
            if (mem[0] == null)
                MessageBox.Show("없다");
            else
            {
                textBox1.Text = mem[0].Id.ToString();
                textBox2.Text = mem[0].Name.ToString();
                textBox3.Text = mem[0].Phone.ToString();
                textBox4.Text = mem[0].Age.ToString();
                textBox5.Text = mem[0].Dt.ToString();
            }
        }

        //4) 전체출력
        private void SelectALlAccount()
        {
            listView1.FullRowSelect = true;          //Row 전체 선택

            listView1.Columns.Add("ID", 50);        //컬럼추가
            listView1.Columns.Add("NAME", 100);
            listView1.Columns.Add("PHONE", 100);
            listView1.Columns.Add("AGE", 50);
            listView1.Columns.Add("DATETIME", 150);
            listView1.BeginUpdate();

            List<Member> members = WbDB.Instance.SelectAllAccount();
            foreach (Member member in members)
            {
                ListViewItem item;

                item = new ListViewItem(member.Id.ToString());
                item.SubItems.Add(member.Name);
                item.SubItems.Add(member.Phone.ToString());
                item.SubItems.Add(member.Age.ToString());
                item.SubItems.Add(member.Dt.ToString());

                listView1.Items.Add(item);
            }
            textBox6.Text = "저장 갯수 : " + members.Count() + "개";
            listView1.EndUpdate();
        }
    }
}
