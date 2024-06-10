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

namespace _0420_미션
{
    public partial class Form1 : Form
    {
        public Select Select { get; set; } = null;
        DataSet Ds { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        //Fill 
        private void button1_Click(object sender, EventArgs e)
        {
            Ds = WbDB.Instance.Open();
            ViewDataSet(Ds);
        }

        //전체 출력
        private void ViewDataSet(DataSet ds)
        {
            dataGridView1.DataSource = ds.Tables[0];
        }

        //검색
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
            Ds = WbDB.Instance.SelectAccount(Select.Mem_Name);
            if (Ds == null)
                MessageBox.Show("없다");
            else
            {
                textBox1.Text = Ds.Tables[0].Rows[0].ToString();
                textBox2.Text = Ds.Tables[0].ToString();
                textBox3.Text = Ds.Tables[0].ToString();
                textBox4.Text = Ds.Tables[0].ToString();
                textBox5.Text = Ds.Tables[0].ToString();
            }
        }

        //저장
        private void button2_Click(object sender, EventArgs e)
        {
            Insert insert = new Insert();
            insert.Text = "저장";
            DialogResult result = insert.ShowDialog();

            if (result == DialogResult.OK)
            {
                Ds = WbDB.Instance.InsertAccount(insert.Mem_Name, insert.Mem_Phone, insert.Mem_Age);
                ViewDataSet(Ds);
            }

            else if (result == DialogResult.Cancel)
            {
                MessageBox.Show("저장 취소");
            }            
        }

        //삭제
        private void button5_Click(object sender, EventArgs e)
        {
            Delete delete = new Delete();
            delete.Text = "삭제";
            DialogResult result = delete.ShowDialog();
            this.Text = delete.Mem_Name;

            if (result == DialogResult.OK)
            {
                Ds = WbDB.Instance.DeleteAccount(delete.Mem_Name);
            }
            ViewDataSet(Ds);
        }
    }
}
