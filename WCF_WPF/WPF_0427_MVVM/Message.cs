using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace _0427_MVVM
{
    public class Message : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region 속성
        public string sender { get; set; }
        public string Sender
        {
            get { return sender; }
            set
            {
                sender = value;
                //WPF 바인딩 시스템에 통지!
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Sender"));
            }
        }
        public string content { get; set; }
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                //WPF 바인딩 시스템에 통지!
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Content"));
            }
        }
        #endregion

        #region 생성자
        public Message()
        {
            Sender = string.Empty;
            Content = string.Empty;
        }

        public Message(string content)
        {
            Sender = "client";
            Content = content;
        }

        public Message(string sender, string content)
        {
            Sender = sender;
            Content = content;
        }
        #endregion

        public void Clear()
        {
            Sender = string.Empty;
            Content = string.Empty;
        }
    }
}
