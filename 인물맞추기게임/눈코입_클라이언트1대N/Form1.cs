using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 눈코입_클라이언트
{
    public partial class Form1 : Form
    {
        private List<string> player_name = new List<string>();
        private WbClient client = null;
        public static string name;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "127.0.0.1";
            player_name.Add(" ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                client = new WbClient(textBox1.Text, 7000);
                this.Hide();

                name = textBox2.Text;
                //이름 중복 검사
                foreach (string values in player_name)
                {
                    if (name.Equals(values))
                    {
                        MessageBox.Show("이미 있는 이름입니다.");
                        return;
                    }
                    else
                    {
                        player_name.Add(name);
                        MessageBox.Show(name + "님, 환영합니다!");

                        Game form = new Game(client, string.Format(name + "님, 서버({0})에 연결되었습니다", textBox1.Text));
                        form.ShowDialog();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("서버 연결 실패");
            }
            this.Show();
        }
    }
}
