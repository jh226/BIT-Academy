//packet.h
#pragma once

//클라이언트 -> 서버
#define PACK_MAKEACCOUNT	10		//pack_MAKEACCOUNT
#define PACK_DELETEACCOUNT	11		//pack_DELETEACCOUNT
#define PACK_INPUTMONEY		12		//pack_INPUTMONEY
#define PACK_OUTPUTMONEY	13		//pack_INPUTMONEY
#define PACK_SELECTACCOUNT	14		//pack_SELECTACCOUNT
#define PACK_ALLACCOUNT		15		//pack_ALLACCOUNT

//서버 -> 클라이언트 
#define ACK_MAKEACCOUNT		20		//pack_MAKEACCOUNT[name,id,balance-1or0]
#define ACK_DELETEACCOUNT	21		//pack_DELETEACCOUNT
#define ACK_INPUTMONEY		22		//pack_INPUTMONEY
#define ACK_OUTPUTMONEY		23		//pack_INPUTMONEY
#define ACK_SELECTACCOUNT	24		//pack_SELECTACCOUNT
#define ACK_ALLACCOUNT		25		//pack_ALLACCOUNT

typedef struct pack_MAKEACCOUNT pack_SELECTACCOUNT;
struct pack_MAKEACCOUNT
{
	int flag;
	char name[20];
	int id;
	int balance;
	bool result;
};

struct pack_DELETEACCOUNT
{
	int flag;
	int id;
	bool result;
};

typedef struct pack_INPUTMONEY pack_OUTPUTMONEY;
struct pack_INPUTMONEY
{
	int flag;
	int id;			//client
	int money;		//client : server : 입금 후 잔액
	bool result;	//         server : true, false
};

struct pack_ALLACCOUNT
{
	int flag;
	int size;
	Account accounts[35];
};


pack_MAKEACCOUNT pack_MakeAccount(const char* name, int id, int balance);
pack_DELETEACCOUNT pack_DeleteAccount(int id);
pack_INPUTMONEY pack_InputMoney(int id, int money);
pack_OUTPUTMONEY pack_OutputMoney(int id, int money);
pack_SELECTACCOUNT pack_SelectAccount(int id);
pack_ALLACCOUNT pack_AllAccount();