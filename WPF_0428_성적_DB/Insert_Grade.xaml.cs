using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
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
using System.Xml.Linq;

namespace _0428_성적
{
    /// <summary>
    /// Insert_Grade.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Insert_Grade : Page
    {
        private StudentList students = null;
        WbDB db = null;
        public Insert_Grade()
        {
            InitializeComponent();
            db = new WbDB();
            db.FillTable();
            DataTable dataTable = new DataTable();
            dataTable = db.dt;
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void input_Grade_Click(object sender, RoutedEventArgs e)
        {
            Student stu = (Student)FindResource("student");
            if (db.Insert_Student(stu.Name, (int)stu.Id, stu.Subject, stu.Grade) == false)
                MessageBox.Show("저장 실패");
            db.SQLUpdate();
            stu.Clear();
        }
    }
}
