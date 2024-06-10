using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace _0426_데이터바인드
{
    internal class Person1 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name 
        {
            get { return name; }
            set
            {
                name = value;

                //WPF바인딩 시스템에 통지
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;

                //WPF바인딩 시스템에 통지
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }

        private int? age;
        public int? Age
        {
            get { return age; }
            set
            {
                age = value;

                //WPF바인딩 시스템에 통지
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Age"));
            }
        }

        private bool? male;
        public bool? Male
        {
            get { return male; }
            set
            {
                male = value;
                //WPF바인딩 시스템에 통지
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Male"));
            }
        }


        public override string ToString()
        {
            return Name + ", " + Phone + ", " + Age + ", " + Male;
        }
    }

    internal class People1 : List<Person1>
    {
        public People1()
        {
            Add(new Person1() { Name = "홍길동", Phone = "010-1111-1111", Age = 10, Male = true });
            Add(new Person1() { Name = "이길동", Phone = "010-2222-2222", Age = 20, Male = false });
            Add(new Person1() { Name = "김길동", Phone = "010-3333-3333", Age = 30, Male = true });
        }
    }



    [ValueConversion(/* 원본 형식 */ typeof(bool), /* 대상 형식 */ typeof(bool))]
    public class MaleToFemaleConverter : IValueConverter
    {
        // 데이터 속성을 UI 속성으로 변경할 때
        public object Convert(object value, Type targetType, object parameter,
       System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool?))
                return null;
            bool? male = (bool?)value;
            if (male == null)
                return null;
            else
                return !(bool?)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(/* 원본 형식 */ typeof(bool), /* 대상 형식 */ typeof(string))]
    public class MaleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? male = (bool?)value;

            if (male == null)
                return "";
            else if (male == true)
                return "남자";
            else if (male == false)
                return "여자";
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string male = (string)value;

            if (male == "")
                return null;
            else if (male.Equals("남자"))
                return true;
            else if (male.Equals("여자"))
                return false;
            return "";
        }
    }


    public class AgeValidationRules : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number;
            if (int.TryParse((string)value, out number) == false)
                return new ValidationResult(false, "정수를 입력하세요");
            if (Min <= number && Max >= number)
                return new ValidationResult(true, null);
            else
            {
                string msg = string.Format("{0} ~ {1} 사이의 값만 가능 ", Min, Max);
                return new ValidationResult(false, msg);
            }
        }
    }

}
