using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0426_데이터바인드
{
    internal class Person
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return Name + ", " + Phone + ", " + Age;
        }
    }

    internal class People : List<Person>
    {
        public People()
        {
            Add(new Person() { Name = "홍길동", Phone = "010-1111-1111", Age = 10 });
            Add(new Person() { Name = "이길동", Phone = "010-2222-2222", Age = 20 });
            Add(new Person() { Name = "김길동", Phone = "010-3333-3333", Age = 30 });
        }
    }

}
