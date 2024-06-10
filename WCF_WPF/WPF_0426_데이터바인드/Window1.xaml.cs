using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _0426_데이터바인드
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        private Person1 per = new Person1()
        {
            Name = "홍길동",
            Phone = "010-1111-1111",
            Age = 10
        };

        public Window1()
        {
            InitializeComponent();

            panel.DataContext = per;
        }
                
        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {
            per.Name = "";
            per.Phone = "";
            per.Age = null;
        }
    }
}