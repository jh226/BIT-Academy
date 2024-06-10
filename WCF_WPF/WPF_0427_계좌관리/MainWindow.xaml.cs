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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _0427_계좌
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccountList accounts        = null;
        private AccountIOList accountios    = null;

        public MainWindow()
        {
            InitializeComponent();

            accounts    = (AccountList)FindResource("account_list");
            accountios  = (AccountIOList)FindResource("accountio_list");
        }

        //저장
        private void input_Account_Click(object sender, RoutedEventArgs e)
        {
            Account acc = (Account)FindResource("input_account");
            accounts.Add(new Account(acc.Id, acc.Name, acc.Balance));

            acc.Clear();
        }

        //입출금
        private void io_Account_Click(object sender, RoutedEventArgs e)
        {
            AccountIO accio = (AccountIO)FindResource("io_account");

            var acc = from account in accounts
                         where account.Id == accio.Id
                         select account;

            if (acc.Count() <= 0)
            {
                MessageBox.Show("없는 계좌번호입니다");
                return;
            }

            if(accio.Input != 0)
                acc.First<Account>().InputMoney(accio.Input);
            else
                acc.First<Account>().OutputMoney(accio.Output);

        }
            
        //계좌리스트 선택
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(FindResource("account_list"));
            Account account = (Account)view.CurrentItem;

            //필터
            ICollectionView ioview = CollectionViewSource.GetDefaultView(FindResource("accountio_list"));
            ioview.Filter = delegate (object obj)
            {
                return ((Account)obj).Id == account.Id;
            };
        }
    }
}
