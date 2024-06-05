using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _0428_성적
{
    /// <summary>
    /// Insert_Grade.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Insert_Grade : Page
    {
        private StudentList students = null;
        public Insert_Grade()
        {
            InitializeComponent();
        }

        private void input_Grade_Click(object sender, RoutedEventArgs e)
        {
            Student stu = (Student)FindResource("student");
            students.Add(new Student(stu.Name, stu.Id, stu.Subject, stu.Grade));

            stu.Clear();
        }
    }
}
