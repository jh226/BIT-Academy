using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _0503_IISClient.ServiceReference1;

namespace _0503_IISClient
{
    public partial class Form1 : Form, ICalCallback  //***** 이중인터페이스
    {
        private CalClient cal;

        public Form1()
        {
            InitializeComponent();

            //--------------------- proxy 객체 생성 --------------
            InstanceContext site = new InstanceContext(this);
            cal = new CalClient(site);
            //-------------------------------------------------------
        }

        #region 서비스 -> 클라이언트
        public void Result([MessageParameter(Name = "result")] float result1)
        {
            textBox3.Text = result1.ToString();
        }
        #endregion

        #region 동기방식(클라이언트 -> 서비스)
        private void button1_Click(object sender, EventArgs e)
        {
            int num1 = int.Parse(textBox1.Text);
            int num2 = int.Parse(textBox2.Text);
            switch( comboBox1.SelectedIndex )
            {
                case 0: cal.Add(num1, num2); break;
                case 1: cal.Sub(num1, num2); break;
                case 2: cal.Mul(num1, num2); break;
                case 3: cal.Div(num1, num2); break;
            }
        }
        #endregion

        #region 비동기방식(클라이언트 -> 서비스)
        private void button2_Click(object sender, EventArgs e)
        {
            int num1 = int.Parse(textBox4.Text);
            int num2 = int.Parse(textBox5.Text);
            switch (comboBox2.SelectedIndex)
            {
                case 0: cal.AddAsync(num1, num2); break;
                case 1: cal.SubAsync(num1, num2); break;
                case 2: cal.MulAsync(num1, num2); break;
                case 3: cal.DivAsync(num1, num2); break;
            }
        }
        #endregion 
    }
}
