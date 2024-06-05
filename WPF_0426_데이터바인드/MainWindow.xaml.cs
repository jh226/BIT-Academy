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

namespace _0426_데이터바인드
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private People peoples = new People();  //본 데이터
        private Person person = null;           //current data - 특정 데이터

        public MainWindow()
        {
            InitializeComponent();

            person = peoples[0];

            UpdateNameToUI();
            UpdatePhoneToUI();
            UpdateAgeUI();
            UpdateListBox();
        }

        #region Data -> Control
        private void UpdateNameToUI()
        {
            if (person == null)
            {
                name.Text = "";
                nameLabel.Content = "";
            }
            else
            {
                name.Text = person.Name;
                nameLabel.Content = person.Name;
            }
        }

        private void UpdatePhoneToUI()
        {
            if (person == null)
            {
                phone.Text = "";
                phoneLabel.Content = "";
            }
            else
            {
                phone.Text = person.Phone;
                phoneLabel.Content = person.Phone;
            }
        }

        private void UpdateAgeUI()
        {
            if (person == null)
            {
                age.Text = "";
                ageLabel.Content = "";
            }
            else
            {
                age.Text = person.Age.ToString();
                ageLabel.Content = person.Age.ToString();
            }
        }

        private void UpdateListBox()
        {
            listbox.Items.Clear();
            foreach(Person person in peoples)
            {
                listbox.Items.Add(person);
            }
        }
        #endregion

        #region Control -> Data
        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            //person.Name = name.Text;
            nameLabel.Content = name.Text;
            //UpdateListBox();
        }

        private void phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            //person.Phone = phone.Text;
            phoneLabel.Content = phone.Text;
            //UpdateListBox();
        }
        private void age_TextChanged(object sender, TextChangedEventArgs e)
        {
            int temp;
            if(int.TryParse(age.Text, out temp) == true)
            {
            //    person.Age = temp;
                  ageLabel.Content = temp;                
            }
            else
            {
            //    person.Age = 0;
                ageLabel.Content = "";
            }
            //UpdateListBox();
        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listbox.SelectedIndex >= 0)
            {
                person = peoples[listbox.SelectedIndex];

                UpdateNameToUI();
                UpdatePhoneToUI();
                UpdateAgeUI();
                UpdateListBox();
            }
        }
        #endregion

        #region 버튼 핸들러
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(name.Text == "" || phone.Text == "" || age.Text == "")
                return;

            peoples.Add(new Person() 
            { 
                Name = name.Text, 
                Phone = phone.Text, 
                Age = int.Parse(age.Text) 
            });

            UpdateListBox();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if(listbox.SelectedIndex >= 0)
            {
                peoples.RemoveAt(listbox.SelectedIndex);
                
                if(peoples.Count == 0)
                {
                    person = null;
                }
                else
                {
                    person = peoples[0];
                }

                UpdateNameToUI();
                UpdatePhoneToUI();
                UpdateAgeUI();
                UpdateListBox();
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || age.Text == "")
                return;

            person.Name = name.Text;
            person.Phone = phone.Text;
            person.Age = int.Parse(age.Text);

            UpdateNameToUI();
            UpdatePhoneToUI();
            UpdateAgeUI();
            UpdateListBox();
        }
        #endregion
    }
}
