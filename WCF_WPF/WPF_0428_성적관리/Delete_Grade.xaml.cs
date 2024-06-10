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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _0428_성적
{
    /// <summary>
    /// Delete_Grade.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Delete_Grade : Page
    {
        private StudentList students = null;
        public Delete_Grade()
        {
            InitializeComponent();
            students = (StudentList)FindResource("studentlist");
        }

        private void delete_Grade_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.SelectedIndex >= 0)
            {
                students.RemoveAt(dataGrid.SelectedIndex);
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = students.FindStudents(textBox.Text);
        }
    }
}
