using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace _0502_WCFService1
{
    //4. DataContract
    [DataContract]
    public class Product
    {
        [DataMember( Order= 1)]
        public int ProductId;

        [DataMember (Order = 2)]
        public string ProductName;

        public Product() { }
        public Product(int id, string name)
        {
            ProductId = id;
            ProductName = name;
        }
    }
}
