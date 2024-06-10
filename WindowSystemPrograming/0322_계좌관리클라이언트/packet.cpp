//packet.cpp

#include "std.h"

pack_MAKEACCOUNT pack_MakeAccount(const char* name, int id, int balance)
{
	pack_MAKEACCOUNT pack;
	
	pack.flag = PACK_MAKEACCOUNT;
	strcpy_s(pack.name, sizeof(pack.name), name);
	pack.id = id;
	pack.balance = balance;

	return pack;
}

pack_DELETEACCOUNT pack_DeleteAccount(int id)
{
	pack_DELETEACCOUNT pack;

	pack.flag = PACK_DELETEACCOUNT;
	pack.id = id;

	return pack;
}

pack_INPUTMONEY pack_InputMoney(int id, int money)
{
	pack_INPUTMONEY pack;

	pack.flag	= PACK_INPUTMONEY;
	pack.id		= id;
	pack.money	= money;

	return pack;
}

pack_OUTPUTMONEY pack_OutputMoney(int id, int money)
{
	pack_OUTPUTMONEY pack;

	pack.flag = PACK_OUTPUTMONEY;
	pack.id = id;
	pack.money = money;

	return pack;
}

pack_SELECTACCOUNT pack_SelectAccount(int id)
{
	pack_SELECTACCOUNT pack = { 0 };

	pack.flag = PACK_SELECTACCOUNT;
	pack.id = id;

	return pack;
}

pack_ALLACCOUNT pack_AllAccount()
{
	pack_ALLACCOUNT pack = { 0 };

	pack.flag = PACK_ALLACCOUNT;

	return pack;
}