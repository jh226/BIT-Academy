using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0428_성적
{
    internal class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region 속성
        public string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                //WPF 바인딩 시스템에 통지!
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public int? id;
        public int? Id
        {
            get { return id; }
            set
            {
                id = value;
                //WPF 바인딩 시스템에 통지!
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }

        public string subject;
        public string Subject
        {
            get { return subject; }
            set
            {
                subject = value;
                //WPF 바인딩 시스템에 통지!
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Subject"));
            }
        }

        public string grade;
        public string Grade
        {
            get { return grade; }
            set
            {
                grade = value;
                //WPF 바인딩 시스템에 통지!
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Grade"));
            }
        }
        #endregion

        #region 생성자
        public Student()
        {
            Name = string.Empty;
            Id = null;
            Subject = string.Empty;
            Grade = string.Empty;            
        }
        public Student(string name, int? id, string subject, string grade)
        {
            Name = name;
            Id = id;
            Subject = subject;
            Grade = grade;
        }
        #endregion
        public override string ToString()
        {
            return Name + ", " + Id + ", " + Subject + ", " + Grade;
        }

        public void Clear()
        {
            Name = string.Empty;
            Id = null;
            Subject = string.Empty;
            Grade = string.Empty;
        }
    }
}
