using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0427_계좌
{
    //WPF의 바인딩 시스템에 변경 사실을 알림 + List기능
    //ObservacleCollection<Account>
    internal class AccountIOList :List<AccountIO>
    {
        public AccountIOList()
        {
            this.Add(new AccountIO(1111, 1000, 0, 1000));
            this.Add(new AccountIO(2222, 1000, 0, 1000));
            this.Add(new AccountIO(3333, 1000, 0, 1000));
        }
    }
}
