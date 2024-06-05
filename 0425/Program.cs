using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.TextFormatting;

/*
 * 1. 어셈블리 참조
 *  - WindowsBase.dll
 *  - Presentationcore.dll
 *  - PresentationFramework.dll
 *  - System.Xaml.dll
 */

namespace _0425
{
    //컨텐츠
    internal class MyWindow : Window
    {
        public MyWindow()
        {
            Button btn = new Button();
            btn.Click += btn_Click;            
            //btn.Width = 100;
            //btn.Height = 50;
            btn.Content = "버튼클릭!";     //버튼의 자식 컨텐츠로 문자열 !

            this.Content = btn;
            //this.AddChild(btn);

            this.Title = "부모 윈도우 -> 버튼 -> 텍스트";
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }

    //실행 흐름
    internal class MyApp : Application
    {
        public MyApp()
        {
            this.Startup += MyApp_Startup;
        }

        public void MyApp_Startup(object sender, StartupEventArgs e)
        {
            MyWindow window = new MyWindow();
            window.Show();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            MessageBox.Show("Hello, WPF");

            MyApp app = new MyApp();
            app.Run();
        }
    }
}
