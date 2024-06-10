using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Window2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window2 : Window
    {
        private Person1 per = null;
        public Window2()
        {
            InitializeComponent();

            //per = (Person1)FindResource("person");
            Validation.AddErrorHandler(age, age_ValidationError);
        }

        private void age_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            //MessageBox.Show((string)e.Error.ErrorContent, "에러");
            age.ToolTip = (string)e.Error.ErrorContent;
        }

        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("people"));
            Person1 person = (Person1)view.CurrentItem;

            person.Name = "";
            person.Phone = "";
            person.Age = null;
            person.Male = null;
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("people"));
            view.MoveCurrentToPrevious();
            if (view.IsCurrentBeforeFirst)
            {
                view.MoveCurrentToFirst();
            }

        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("people"));
            view.MoveCurrentToNext();
            if (view.IsCurrentAfterLast)
            {
                view.MoveCurrentToLast();
            }
        }
    }
}
