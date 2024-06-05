using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0411
{
    internal class Program
    {
        #region Exam1 파일처리(텍스트파일)
        static void Exam1() 
        {
            // *파일처리의 3단계
            // 파일 열기 --> 파일읽기/쓰기(반복) --> 파일 닫기
            // * 파일의 종류 : 텍스트 파일, 바이너리 파일
            // * 텍스트 파일 : 메모장에서 열어서 글자처럼 보임.
            //      --> 1바이트가 의미가 있음.
            // * 바이너리 : 비트가 의미. 2비트 색상, 3비트 크기,7 비트.....
            string fullName = "data1.txt";
            string writeName = "data2.txt";
            StreamReader sr
              = new StreamReader(new FileStream(fullName, FileMode.Open));
            StreamWriter sw
              = new StreamWriter(new FileStream(writeName, FileMode.Create));

            string line1;
            while (!sr.EndOfStream)
            {
                line1 = sr.ReadLine();
                Console.WriteLine(line1);
                sw.WriteLine(line1);
            }
            sr.Close();
            sw.Close();
        }
        #endregion

        #region Exam2 파일처리(바이너리파일)
        static void printImage(byte[,] img) //Exam2
        {
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    Console.Write("{0:d3} ", img[i, k]);
                }
                Console.WriteLine();
            }
        }
        static void Exam2() 
        {
            /// 1단계 : 파일 열기
            string fullName = "lena.raw";

            BinaryReader br = new BinaryReader(File.Open(fullName, FileMode.Open));
            // 2단계 : 파일 처리하기... 내맘대로.. 
            // 파일 --> 메모리(배열)
            int ROW = 512, COL = 512;
            byte[,] image = new byte[ROW, COL];
            for (int i = 0; i < ROW; i++)
            {
                for (int k = 0; k < COL; k++)
                {
                    image[i, k] = br.ReadByte();
                }
            }

            printImage(image);

            // 3단계 : 파일 닫기
            br.Close();
        }
        #endregion

        #region Exam3 그림파일 알고리즘    

        //사진이 어둡다. 50만큼 밝게하고 출력하기
        static void LightVolumUp(byte[,] img, int ROW, int COL)
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int k = 0; k < COL; k++)
                {
                    if (img[i, k] + 50 > 255)
                    {
                        img[i, k] = 255;
                    }
                    else
                    {
                        img[i, k] = (byte)(img[i, k] + 50);
                    }
                }
            }
        }

        //사진이 너무 밝다. 100만큼 어둡게하고 출력하기
        static void LightVolumDown(byte[,] img, int ROW, int COL)
        {
            Console.WriteLine();

            for (int i = 0; i < ROW; i++)
            {
                for (int k = 0; k < COL; k++)
                {
                    if (img[i, k] - 100 < 0)
                    {
                        img[i, k] = 0;
                    }
                    else
                    {
                        img[i, k] = (byte)(img[i, k] - 100); ;
                    }
                }
            }
        }

        //사진반전처리하기
        static void reversePicture(byte[,] img, int ROW, int COL)
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int k = 0; k < COL; k++)
                {
                    img[i, k] = (byte)(255 - img[i, k]);

                }
            }
        }

        //127기준 회색영상을 흑백영상으로 만들기
        static void PictureDarkMode_127(byte[,] img, int ROW, int COL)
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int k = 0; k < COL; k++)
                {
                    if (img[i, k] - 127 < 0)
                    {
                        img[i, k] = 0;
                    }
                    else
                    {
                        img[i, k] = 255;
                    }
                }
            }
        }

        //위의 기준 127대신에 평균값으로 처리
        static void PictureDarkMode_hap(byte[,] img, int ROW, int COL)
        {
            int hap;
            hap = 0;
            for (int i = 0; i < ROW; i++)
            {
                for (int k = 0; k < COL; k++)
                {
                    hap += img[i, k];

                }
            }
            Console.WriteLine("hap : " + hap);
            hap = hap / (ROW * COL);

            Console.WriteLine("hap : " + hap);

            for (int i = 0; i < ROW; i++)
            {
                for (int k = 0; k < COL; k++)
                {
                    if (img[i, k] - hap < 0)
                    {
                        img[i, k] = 0;
                    }
                    else
                    {
                        img[i, k] = 255;
                    }
                }
            }
        }

        static void Exam3() 
        {
            //1단계 파일 열기 
            string fullName = "lena.raw";
            BinaryReader br = new BinaryReader(File.Open(fullName, FileMode.Open));

            //2단계 파일 처리하기...내맘대로
            //파일 --> 메모일(배열)
            int ROW = 512, COL = 512;
            byte[,] image = new byte[ROW, COL];
            for (int i = 0; i < ROW; i++)
            {
                for (int k = 0; k < COL; k++)
                {
                    image[i, k] = br.ReadByte();
                }
            }
              
            printImage(image);

            Console.WriteLine("\n[PictureDarkMode_hap]");
            PictureDarkMode_hap(image, ROW, COL);
            printImage(image);
            Console.WriteLine();

            Console.WriteLine("[LightVolumUp]");
            LightVolumUp(image, ROW, COL);
            printImage(image);
            Console.WriteLine();

            Console.WriteLine("[LightVolumDown]");
            LightVolumDown(image, ROW, COL);
            printImage(image);
            Console.WriteLine();

            Console.WriteLine("[reversePicture]");
            reversePicture(image, ROW, COL);
            printImage(image);
            Console.WriteLine();


            Console.WriteLine("[PictureDarkMode_127]");
            PictureDarkMode_127(image, ROW, COL);
            printImage(image);
            Console.WriteLine();

            br.Close();
        }

        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            //Exam3();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
