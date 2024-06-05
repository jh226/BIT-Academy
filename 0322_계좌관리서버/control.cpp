//control.cpp

#include "std.h"

vector<Account*> accounts;

//�ʱ�ȭ
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
	cout << "���� ���� ��................" << endl;	
}

//����ó��
void con_exit()
{
	net_Exit();
}


//�м�
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

//------------------- ��� ----------------------------------------------
void MakeAccount(HANDLE sock, pack_MAKEACCOUNT* msg)
{
	//1. ����Ÿ�� ��ü ����	
	Account* pmem = acc_create(msg->name, msg->balance, msg->id);

	//����ó��(�ߺ�ID ���� �Ұ�)
	int value;
	if (Id_Check(pmem->id) == true)  //�̻��� ����.
	{
		value = 1;
		//2. ���������� ����
		accounts.push_back(pmem);
	}
	else
	{
		value = 0;
	}	

	PrintAllAccount();

	//--------------- ���� ó�� --------------------------
	pack_MAKEACCOUNT pack = pack_MakeAccount(msg->name, msg->id, value);
	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}

void DeleteAccount(HANDLE sock, pack_DELETEACCOUNT* msg)
{
	bool ret = false; 

	//1. �˻� �� ����	
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

	//2. ȭ�� ���
	PrintAllAccount();

	//--------------- ���� ó�� --------------------------
	pack_DELETEACCOUNT pack = pack_DeleteAccount(msg->id, ret);
	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}

void InputMoney(HANDLE sock, pack_INPUTMONEY* msg)
{
	bool ret = false;
	int balance = -1;
	//1. �˻� �� ����	
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

	//2. ȭ�� ���
	PrintAllAccount();

	//--------------- ���� ó�� --------------------------
	pack_INPUTMONEY pack = pack_InputMoney(msg->id, balance, ret);
	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}

void OutputMoney(HANDLE sock, pack_INPUTMONEY* msg)
{
	bool ret = false;
	int balance = -1;
	//1. �˻� �� ����	
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

	//2. ȭ�� ���
	PrintAllAccount();

	//--------------- ���� ó�� --------------------------
	pack_INPUTMONEY pack = pack_OutputMoney(msg->id, balance, ret);
	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}

void SelectAccount(HANDLE sock, pack_SELECTACCOUNT* msg)
{
	bool ret = false;
	Account* acc = NULL;
	//1. �˻�
	for (int i = 0; i < (int)accounts.size(); i++)
	{
		acc = accounts[i];
		if (acc->id == msg->id)
		{		
			ret = true;
			break;
		}
	}

	//2. ȭ�� ���
	PrintAllAccount();

	//--------------- ���� ó�� --------------------------
	pack_SELECTACCOUNT pack;
	if(ret == true)
		pack = pack_SelectAccount(acc->id, acc->name, acc->balance, ret);
	else
		pack = pack_SelectAccount(msg->id, "", -1, ret);

	net_SendData((SOCKET)sock, (char*)&pack, sizeof(pack));
}

void AllAccount(HANDLE sock, pack_ALLACCOUNT* msg)
{	
	//--------------- ���� ó�� --------------------------
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
	printf("���� ����: %d��\n", (int)accounts.size());

	printf("�ε���\t�̸�\t�ܾ�\t���¹�ȣ\n");
	printf("===========================================\n");

	for (int i = 0; i < (int)accounts.size(); i++)
	{
		Account* value = accounts[i];
		acc_print(i, value);					// acc�� ��� ��û
	}
	printf("===========================================\n");
}

