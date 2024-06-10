using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _0427_계좌
{
    internal class Account : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region 속성
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

        public int? balance;
        public int? Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                //WPF 바인딩 시스템에 통지!
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Balance"));
            }
        }

        public DateTime? newdate;
        public DateTime? NewDate
        {
            get { return newdate; }
            set
            {
                newdate = value;
                //WPF 바인딩 시스템에 통지!
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NewDate"));
            }
        }
        #endregion

        #region 생성자
        public Account()
        {
            Id = null;
            Name = string.Empty;
            Balance = null;
            NewDate = null;
        }
        public Account(int? id, string name, int? balance)
        {
            Id = id;
            Name = name;
            Balance = balance;
            NewDate = DateTime.Now;
        }
        #endregion

        public void InputMoney(int? money)
        {
            Balance += money;
        }

        public void OutputMoney(int? money)
        {
            Balance -= money;
        }

        public void Clear()
        {
            Id = null;
            Name = string.Empty;
            Balance = null;
            NewDate = null;
        }

        public override string ToString()
        {
            return Id+", " +Name +", " + Balance +", " + NewDate.ToString();
        }
    }
}
