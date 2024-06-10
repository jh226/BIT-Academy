//packet.cpp

#include "std.h"

pack_MAKEACCOUNT pack_MakeAccount(const char* name, int id, int result)
{
	pack_MAKEACCOUNT pack;

	pack.flag = ACK_MAKEACCOUNT;
	strcpy_s(pack.name, sizeof(pack.name), name);
	pack.id = id;
	pack.balance = result;		//성공/실패

	return pack;
}

pack_DELETEACCOUNT pack_DeleteAccount(int id, bool result)
{
	pack_DELETEACCOUNT pack;

	pack.flag = ACK_DELETEACCOUNT;
	pack.id = id;
	pack.result = result;		//성공/실패

	return pack;
}

pack_INPUTMONEY pack_InputMoney(int id, int balance, bool result)
{
	pack_INPUTMONEY pack;

	pack.flag = ACK_INPUTMONEY;
	pack.id = id;
	pack.money = balance;
	pack.result = result;		//성공/실패

	return pack;
}

pack_INPUTMONEY pack_OutputMoney(int id, int balance, bool result)
{
	pack_INPUTMONEY pack;

	pack.flag = ACK_OUTPUTMONEY;
	pack.id = id;
	pack.money = balance;
	pack.result = result;		//성공/실패

	return pack;
}

pack_SELECTACCOUNT pack_SelectAccount(int id, const char* name, int balance, bool result)
{
	pack_SELECTACCOUNT pack;

	pack.flag = ACK_SELECTACCOUNT;
	strcpy_s(pack.name, sizeof(pack.name), name);
	pack.id = id;
	pack.balance = balance;		
	pack.result = result;	//성공/실패

	return pack;
}

pack_ALLACCOUNT pack_AllAccount(vector<Account*> accounts)
{
	pack_ALLACCOUNT pack;

	pack.flag = ACK_ALLACCOUNT;
	pack.size = (int)accounts.size();
	
	for (int i = 0; i < (int)accounts.size(); i++)
	{
		Account* p = accounts[i];

		pack.accounts[i] = *p;  //구조체 값 끼리 대입가능
	}

	return pack;
}