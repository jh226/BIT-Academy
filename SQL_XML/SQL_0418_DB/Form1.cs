using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0418_DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region DB연결
        private void button1_Click(object sender, EventArgs e)
        {
            if (WbDB.Instance.Open() == true)
                this.Text = "DB연결....";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WbDB.Instance.Close() == true)
            {
                this.Text = "DB연결 해제....";
            }
        }
        #endregion

        //1) 저장
        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            int money = int.Parse(textBox2.Text);

            if (WbDB.Instance.InsertAccount(name, money) == false)
                MessageBox.Show("저장 실패");
        }

        //2) 검색 (하나의 정보)
        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text;

            int accid = WbDB.Instance.LastInsertAccid(name);
            if (accid == 0)
                MessageBox.Show("없다");
            else
                textBox4.Text = accid.ToString();
        }

        //3) 검색 - 전체 계좌
        private void button5_Click(object sender, EventArgs e)
        {
            List<Account> accounts = WbDB.Instance.SelectAllAccount();
            foreach (Account account in accounts)
            {
                listBox1.Items.Add(account);
            }
        }

        //4) 삭제
        private void button6_Click(object sender, EventArgs e)
        {
            int accid = int.Parse(textBox5.Text);

            if (WbDB.Instance.DeleteAccount(accid) == true)
                MessageBox.Show("삭제");
            else
                MessageBox.Show("실패");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int accid = int.Parse(textBox6.Text);
            string upname =textBox7.Text;

            if (WbDB.Instance.UpdateAccount(accid, upname) == true)
                MessageBox.Show("수정");
            else
                MessageBox.Show("실패");
        }

        //입금
        private void button8_Click(object sender, EventArgs e)
        {
            int accid = int.Parse(textBox8.Text);
            int money = int.Parse(textBox9.Text);

            if (WbDB.Instance.UpdateInputAccount(accid, money) == true)
                MessageBox.Show("입금성공");
            else
                MessageBox.Show("입금실패");
        }
        //출금
        private void button9_Click(object sender, EventArgs e)
        {
            int accid = int.Parse(textBox8.Text);
            int money = int.Parse(textBox9.Text);

            if (WbDB.Instance.UpdateOutputAccount(accid, money) == true)
                MessageBox.Show("출금성공");
            else
                MessageBox.Show("출금실패");
        }
    }
}
