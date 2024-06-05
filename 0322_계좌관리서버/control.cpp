//control.cpp

#include "std.h"

vector<Account*> accounts;

//초기화
void con_init()
{
	if (net_init() == false)
	{
		exit(0);	
	}

	if (net_CreateSocket(SERVER_PORT) == false)
	{
		exit(0);
	}
	cout << "서버 동작 중................" << endl;	
}

//종료처리
void con_exit()
{
	net_Exit();
}


//분석
void con_RecvData(HANDLE sock, char* msg, int size)
{
	int* p = (int*)msg;
	if (*p == PACK_MAKEACCOUNT)
	{
		MakeAccount(sock, (pack_MAKEACCOUNT*)msg);
	}
	else if (*p == PACK_DELETEACCOUNT)
	{
		DeleteAccount(sock, (pack_DELETEACCOUNT*)msg);
	}	
	else if (*p == PACK_INPUTMONEY)
	{
		InputMoney(sock, (pack_INPUTMONEY*)msg);
	}
	else if (*p == PACK_OUTPUTMONEY)
	{
		OutputMoney(sock, (pack_INPUTMONEY*)msg);
	}
	else if (*p == PACK_SELECTACCOUNT)
	{
		SelectAccount(sock, (pack_SELECTACCOUNT*)msg);
	}
	else if (*p == PACK_ALLACCOUNT)
	{
		AllAccount(sock, (pack_ALLACCOUNT*)msg);
	}
}

//------------------- 기능 ----------------------------------------------
void MakeAccount(HANDLE sock, pack_MAKEACCOUNT* msg)
{
	//1. 저장타입 객체 생성	
	Account* pmem = acc_create(msg->name, msg->balance, msg->id);

	//예외처리(중복ID 저장 불가)
	int value;
	if (Id_Check(pmem->id) == true)  //이상이 없다.
	{
		value = 1;
		//2. 전역변수에 저장
		accounts.push_back(pmem);
	}
	else
	{
		value = 0;
	}	

	PrintAllAccount();

	//--------------- 응답 처리 --------------------------
	pack_MAKEACCOUNT pack = pack_MakeAccount(msg->name, msg->id, value);
	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}

void DeleteAccount(HANDLE sock, pack_DELETEACCOUNT* msg)
{
	bool ret = false; 

	//1. 검색 후 삭제	
	for (int i = 0; i < (int)accounts.size(); i++)
	{
		Account* acc = accounts[i];
		if (acc->id == msg->id)
		{
			accounts.erase(accounts.begin() + i);
			ret = true;
			break;
		}
	}

	//2. 화면 출력
	PrintAllAccount();

	//--------------- 응답 처리 --------------------------
	pack_DELETEACCOUNT pack = pack_DeleteAccount(msg->id, ret);
	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}

void InputMoney(HANDLE sock, pack_INPUTMONEY* msg)
{
	bool ret = false;
	int balance = -1;
	//1. 검색 후 삭제	
	for (int i = 0; i < (int)accounts.size(); i++)
	{
		Account* acc = accounts[i];
		if (acc->id == msg->id)
		{
			acc_addmoney(acc, msg->money);
			balance = acc->balance;
			ret = true;
			break;
		}
	}

	//2. 화면 출력
	PrintAllAccount();

	//--------------- 응답 처리 --------------------------
	pack_INPUTMONEY pack = pack_InputMoney(msg->id, balance, ret);
	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}

void OutputMoney(HANDLE sock, pack_INPUTMONEY* msg)
{
	bool ret = false;
	int balance = -1;
	//1. 검색 후 삭제	
	for (int i = 0; i < (int)accounts.size(); i++)
	{
		Account* acc = accounts[i];
		if (acc->id == msg->id)
		{
			acc_minmoney(acc, msg->money);
			balance = acc->balance;
			ret = true;
			break;
		}
	}

	//2. 화면 출력
	PrintAllAccount();

	//--------------- 응답 처리 --------------------------
	pack_INPUTMONEY pack = pack_OutputMoney(msg->id, balance, ret);
	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}

void SelectAccount(HANDLE sock, pack_SELECTACCOUNT* msg)
{
	bool ret = false;
	Account* acc = NULL;
	//1. 검색
	for (int i = 0; i < (int)accounts.size(); i++)
	{
		acc = accounts[i];
		if (acc->id == msg->id)
		{		
			ret = true;
			break;
		}
	}

	//2. 화면 출력
	PrintAllAccount();

	//--------------- 응답 처리 --------------------------
	pack_SELECTACCOUNT pack;
	if(ret == true)
		pack = pack_SelectAccount(acc->id, acc->name, acc->balance, ret);
	else
		pack = pack_SelectAccount(msg->id, "", -1, ret);

	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}

void AllAccount(HANDLE sock, pack_ALLACCOUNT* msg)
{	
	//--------------- 응답 처리 --------------------------
	pack_ALLACCOUNT pack = pack_AllAccount(accounts);
	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}


bool Id_Check(int id)
{
	for (int i = 0; i < (int)accounts.size(); i++)
	{
		Account* acc = accounts[i];
		if (acc->id == id)
			return false;
	}
	return true;
}

void PrintAllAccount()
{
	system("cls");
	printf("저장 개수: %d개\n", (int)accounts.size());

	printf("인덱스\t이름\t잔액\t계좌번호\n");
	printf("===========================================\n");

	for (int i = 0; i < (int)accounts.size(); i++)
	{
		Account* value = accounts[i];
		acc_print(i, value);					// acc에 출력 요청
	}
	printf("===========================================\n");
}

