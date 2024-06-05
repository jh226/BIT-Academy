using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0427_계좌
{
    //WPF의 바인딩 시스템에 변경 사실을 알림 + List기능
    //ObservacleCollection<Account>
    internal class AccountList : ObservableCollection<Account> //List<Account>
    {
        public AccountList()
        {
            this.Add(new Account(1111, "홍길동", 1000));
            this.Add(new Account(2222, "김길동", 2000));
            this.Add(new Account(3333, "허길동", 3000));
        }
    }
}
