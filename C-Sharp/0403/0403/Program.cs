using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0403
{
    class MyInt // 사용자 정의 클래스 - 참조 형식
    {
        public int Value { get; set; } //속성
        public MyInt(int value) //생성자
        {
            Value = value;
        }
        public override string ToString() //재정의
        {
            return Value.ToString();
        }
    }

   
    class Program
    {
        static void Exam1()   //page 8
        {
            int i = 1;
            int j = i;
            i++;
            Console.WriteLine("i:{0} j:{1}", i, j);
            MyInt a = new MyInt(1);
            MyInt b = a;
            a.Value++;
            Console.WriteLine("a:{0} b:{0}", a, b);
        }
        static void Exam2()   //page 10
        {
            int i = 0; //선언문
            Console.WriteLine("수를 입력하세요.."); //호출식(문)
            try //예외 처리문의 try 블록
            {
                i = int.Parse(Console.ReadLine());
                if ((i % 2) == 0) //조건문
                {
                    Console.WriteLine("입력한 수 {0}는 짝수입니다..", i);
                }
                else
                {
                    Console.WriteLine("입력한 수 {0}는 홀수입니다. ", i);
                }
                int sum = 0;
                for (int index = 1; index < i; index++) //반복문
                {
                    sum += index;
                }
                Console.WriteLine("1~{0}까지의 합은 {1}입니다.", 1, i, sum);
            }
            catch (Exception e) //예외 처리문의 catch 블록
            {
                Console.WriteLine("예외가 발생하였습니다. {0}", e.Message);
            }
        }
        static void Exam3()   //page 11
        {
            Console.WriteLine("이름을 입력하세요.");
            string name = Console.ReadLine();

            if (name != string.Empty) //이름을 입력하였을 때
            {
                Console.WriteLine("{0}의 주소를 입력하세요. ", name);
                string addr = Console.ReadLine();
                if (addr != string.Empty)//주소를 입력하였을 때
                {
                    Console.WriteLine("{0}의 주소는 {1}입니다. ", name, addr);
                }
            }

            else //이름을 입력하지 않았을 때
            {
                Console.WriteLine("이름을 입력하지 않았습니다.");
            }
        }
        static void Exam4()   //page 12
        {
            string name = string.Empty;
            Console.WriteLine("이름을 입력하세요..");
            name = Console.ReadLine();
            switch (name)
            {
                case "홍길동": Console.WriteLine("휘리릭~"); break;
                case "강감찬":
                case "을지문덕 ": Console.WriteLine("이랴~"); break;
                default: Console.WriteLine("음냐뤼~"); break;
            }

        }
        static void Exam5()   //page 13
        {
            int i = 0;
            while (i < 10)
            {
                Console.WriteLine("{0}", i);
                i++;
            }
            do
            {
                i++;
                Console.WriteLine("do-while:{0}", i);
            } while (i < 10);

        }
        static void Exam6()   //page 14
        {
            int[] arr = new int[] { 2, 3, 4, 5, 6 };
            foreach (int i in arr)
            {
                Console.WriteLine(i.ToString());
            }
        }
        static void Main(string[] args)
        {
            Exam6();
        }
    }
}
