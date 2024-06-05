using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0420_메모리DB
{
    public partial class Form1 : Form
    {
        private WbMemoryDB db = new WbMemoryDB();
        public Form1()
        {
            InitializeComponent();
        }

        //Books 테이블 생성
        private void button1_Click(object sender, EventArgs e)
        {
            db.Create_BookTable();

            Print_BookTable();
        }
        
        //insert 저장
        private void button2_Click(object sender, EventArgs e)
        {
            string isbn = textBox2.Text;
            string title = textBox3.Text;
            string author = textBox4.Text;
            int price = int.Parse(textBox5.Text);

            if(db.Insert_Book(isbn, title, author, price) == false)
            {
                MessageBox.Show("저장 실패");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int idx = int.Parse(textBox6.Text);
            string data = db.Book_GetRowData(idx);

            textBox7.Text = data;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            db.Xml_Write();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            db.Xml_Read();
            Print_BookTable();
        }

        private void Print_BookTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("-----------------------------------\r\n");
            sb.Append("테이블명 : " + db.Book_TableName + "\r\n");
            sb.Append("-----------------------------------\r\n");
            foreach (DataColumn c in db.Book_Table.Columns)
            {
                sb.Append(" " + c.ColumnName + "\t" + c.DataType
                    + "\t" + c.AllowDBNull + "\r\n");
            }
            sb.Append("-----------------------------------\r\n");
            sb.Append(" * " + db.Book_Table.PrimaryKey[0].ColumnName + "\r\n");

            textBox1.Text = sb.ToString();

            dataGridView1.DataSource = db.Book_Table;
        }
    }
}
