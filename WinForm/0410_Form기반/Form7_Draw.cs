using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//1. 타입 정의
//2. 저장 변수
//3. 삽입(LButtonUp)
//. 삭제(버튼)
//, 검색(버튼)-->List에 저장된 정보 획득(해당 객체를 얻어왔다면, Pen으로 진하게 칠함)
//               다른 검색을 수행하면 원상복구
//, 출력(OnPaint)
//, 수정(키보드 입력을 통해 수정 처리), 컨트롤을 출가해서 수정)

namespace _0410_Form기반
{
    internal enum Shape_Type { NONE, RECTANGLE, ELLIPSE};

    internal class Shape
    {
        public Shape_Type ShapeType { get; set; }
        public Point ShapePoint { get; set; }
        public Size ShapeSize { get; set; }
        public Color ShapeColor { get; set; }

        public Shape(Shape_Type st, Point p, Size s, Color c)
        {
            ShapeType   = st;
            ShapePoint  = p;
            ShapeSize   = s;
            ShapeColor  = c;
        }

        public override string ToString()
        {
            return ShapeType + "\t" + ShapePoint + "\t" + ShapeSize + "\t" + ShapeColor;
        }
    }

    internal class Form7_Draw
    {
        private List<Shape> _shapes = new List<Shape>();

        public List<Shape> Shapes {  get { return _shapes; } }

        public Shape this[int idx]  {   get { return _shapes[idx]; }        }

        public void Insert(Shape s)
        {
            _shapes.Add(s);
        }

        public void Delete(Shape s)
        {
            _shapes.Remove(s);
        }

        public void UpdateType(Shape s, Shape_Type st)
        {
            s.ShapeType = st;
        }

        public void UpdateColor(Shape s, Color c)
        {
            s.ShapeColor = c;
        }
    }
}
