using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _0410_Form기반
{
    public partial class Form7_Paint : Form
    {
        //컬렉션 소유
        Form7_Draw draw = new Form7_Draw();

        //현재 설정된 도형 타입
        private Shape cur_Shape;

        public Form7_Paint()
        {
            InitializeComponent();

            cur_Shape = new Shape(Shape_Type.RECTANGLE,
               new Point(10, 10), new Size(100, 100), Color.Orange);

            comboBox1.Items.Add("RECTANGLE");
            comboBox1.Items.Add("ELLIPSE");
        }

        //저장!
        private void Form7_Paint_MouseUp(object sender, MouseEventArgs e)
        {
            cur_Shape.ShapePoint = new Point((int)e.X, (int)e.Y);

            //******************************
            draw.Insert(new Shape(cur_Shape.ShapeType, cur_Shape.ShapePoint, 
                                     cur_Shape.ShapeSize, cur_Shape.ShapeColor));

            this.Text = String.Format("저장개수 : {0}", draw.Shapes.Count);

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach(Shape s in draw.Shapes)
            {
                PrintShape(e.Graphics, s);
            }
        }

        private void PrintShape(Graphics g, Shape s)
        {
            Brush br = new SolidBrush(s.ShapeColor);
            Point pt = s.ShapePoint;
            Size size = s.ShapeSize;

            if (s.ShapeType == Shape_Type.RECTANGLE)
            {
                g.FillRectangle(br, pt.X, pt.Y, size.Width, size.Height);
            }
            else if (s.ShapeType == Shape_Type.ELLIPSE)
            {
                g.FillEllipse(br, pt.X, pt.Y, size.Width, size.Height);
            }            
        }

        private void Form7_Paint_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  // 삭제
        {
            draw.Shapes.RemoveAt(int.Parse(textBox1.Text)-1);
            this.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)  // 검색
        {
            int idx = int.Parse(textBox1.Text) - 1;
            Point pt = draw.Shapes[idx].ShapePoint;

            Graphics g = this.CreateGraphics();

            if (draw.Shapes[idx].ShapeType == Shape_Type.RECTANGLE)
                g.DrawRectangle(new Pen(Color.Red, 3), pt.X, pt.Y,
                draw.Shapes[idx].ShapeSize.Width, draw.Shapes[idx].ShapeSize.Height);

            else if (draw.Shapes[idx].ShapeType == Shape_Type.ELLIPSE)
                g.DrawEllipse(new Pen(Color.Red, 3), pt.X, pt.Y,
                draw.Shapes[idx].ShapeSize.Width, draw.Shapes[idx].ShapeSize.Height);
        }

        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //현재 모양
        {
            if(comboBox1.SelectedIndex == 0)
            {
                this.cur_Shape.ShapeType = Shape_Type.RECTANGLE;
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                this.cur_Shape.ShapeType = Shape_Type.ELLIPSE;
            }
        }

        private void button3_Click(object sender, EventArgs e) //수정
        {
            int idx = int.Parse(textBox1.Text) - 1;
            if(draw.Shapes[idx].ShapeType == Shape_Type.RECTANGLE)
                draw.Shapes[idx].ShapeType = Shape_Type.ELLIPSE;
            else if (draw.Shapes[idx].ShapeType == Shape_Type.ELLIPSE)
                draw.Shapes[idx].ShapeType = Shape_Type.RECTANGLE;

            this.Invalidate();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
