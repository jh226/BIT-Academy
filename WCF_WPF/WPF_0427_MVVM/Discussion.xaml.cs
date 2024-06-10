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

namespace _0427_MVVM
{
    /// <summary>
    /// Discussion.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Discussion : Page
    {
        private MessageIO messages = null;
        public Discussion()
        {
            InitializeComponent();
            messages = (MessageIO)FindResource("messages");
        }

        private void send_Message_Click(object sender, RoutedEventArgs e)
        {
            Message message = (Message)FindResource("message");
            messages.Add(new Message(message.Sender,message.Content));

            message.Clear();
        }
    }
}
