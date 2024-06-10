using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace _0502_WCFService1
{
    //1. 서비스 계약
    [ServiceContract]
    internal interface IHelloWorld
    {
        [OperationContract]
        string SayHello();

        [OperationContract]
        bool Insert_Product(int id, string name);

        [OperationContract]
        int Product_Count();

        [OperationContract]
        List<Product> GetList_Product();
    }
}
