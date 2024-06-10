//bank.cpp
#include "std.h"

void bank_makeaccount()
{
	printf("���� ����\n");

	//1. ���� �Է�
	char name[20];
	int  id, balance;	

	printf("�̸� : ");		gets_s(name, sizeof(name));
	printf("�Աݾ� : ");	scanf_s("%d", &balance);
	printf("���¹�ȣ : ");	scanf_s("%d", &id);
	char dummy = getchar();

	//2. ��Ŷ ����
	pack_MAKEACCOUNT pack = pack_MakeAccount(name, id, balance);

	//3. ������ ����
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_makeaccount_ack(pack_MAKEACCOUNT *msg)
{
	if (msg->balance == 1)
	{
		cout << "���� ���� ����" << endl;
	}
	else
	{
		cout << "���� ���� ����" << endl;
	}
}

void bank_deleteaccount()
{
	printf("���� ����\n");

	int  accid;
	printf("���¹�ȣ : ");		scanf_s("%d", &accid);

	//2. ��Ŷ ����
	pack_DELETEACCOUNT pack = pack_DeleteAccount(accid);

	//3. ������ ����
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_deleteaccount_ack(pack_DELETEACCOUNT* msg)
{
	if (msg->result == true)
	{
		cout << "���� ���� ����" << endl;
	}
	else
	{
		cout << "���� ���� ����" << endl;
	}
}

void bank_inputmoney()
{
	printf("���� �Ա�\n");

	int  accid, balance;
	
	printf("���¹�ȣ : ");		scanf_s("%d", &accid);
	printf("�Աݾ� : ");		scanf_s("%d", &balance);
	
	//2. ��Ŷ ����
	pack_INPUTMONEY pack = pack_InputMoney(accid, balance);

	//3. ������ ����
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_inputmoney_ack(pack_INPUTMONEY* msg)
{
	if (msg->result == true)
	{
		cout << "���� �Ա� ����" << endl;
		cout << "�Ա� �� �ܾ� : " << msg->money << endl;
	}
	else
	{
		cout << "���� �Ա� ����" << endl;
	}
}

void bank_outputmoney()
{
	printf("���� ���\n");

	int  accid, balance;

	printf("���¹�ȣ : ");		scanf_s("%d", &accid);
	printf("��ݾ� : ");		scanf_s("%d", &balance);

	//2. ��Ŷ ����
	pack_OUTPUTMONEY pack = pack_OutputMoney(accid, balance);

	//3. ������ ����
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_outputmoney_ack(pack_OUTPUTMONEY* msg)
{
	if (msg->result == true)
	{
		cout << "���� ��� ����" << endl;
		cout << "��� �� �ܾ� : " << msg->money << endl;
	}
	else
	{
		cout << "���� ��� ����" << endl;
	}
}

void bank_selectaccount()
{
	printf("���� �˻�\n");

	int  accid;

	printf("���¹�ȣ : ");		scanf_s("%d", &accid);

	//2. ��Ŷ ����
	pack_SELECTACCOUNT pack = pack_SelectAccount(accid);

	//3. ������ ����
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_selectaccount_ack(pack_SELECTACCOUNT* msg)
{
	if (msg->result == true)
	{		
		cout << "���¹�ȣ : " << msg->id << endl;
		cout << "�̸� : " << msg->name << endl;
		cout << "�ܾ� : " << msg->balance << endl;
	}
	else
	{
		cout << "���� ��� ����" << endl;
	}
}

void bank_allaccount()
{
	printf("���� ��ü ���\n");

	//2. ��Ŷ ����
	pack_ALLACCOUNT pack = pack_AllAccount();

	//3. ������ ����
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_allaccount_ack(pack_ALLACCOUNT* msg)
{
	cout << "���� ���� : " << msg->size << "\n" << endl;

	for (int i = 0; i < msg->size; i++)
	{
		Account acc = msg->accounts[i];
		acc_print(i, &acc);
	}
}