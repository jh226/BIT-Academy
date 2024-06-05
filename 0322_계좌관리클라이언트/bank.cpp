//bank.cpp
#include "std.h"

void bank_makeaccount()
{
	printf("계좌 생성\n");

	//1. 정보 입력
	char name[20];
	int  id, balance;	

	printf("이름 : ");		gets_s(name, sizeof(name));
	printf("입금액 : ");	scanf_s("%d", &balance);
	printf("계좌번호 : ");	scanf_s("%d", &id);
	char dummy = getchar();

	//2. 패킷 생성
	pack_MAKEACCOUNT pack = pack_MakeAccount(name, id, balance);

	//3. 서버로 전송
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_makeaccount_ack(pack_MAKEACCOUNT *msg)
{
	if (msg->balance == 1)
	{
		cout << "계좌 개설 성공" << endl;
	}
	else
	{
		cout << "계좌 개설 실패" << endl;
	}
}

void bank_deleteaccount()
{
	printf("계좌 삭제\n");

	int  accid;
	printf("계좌번호 : ");		scanf_s("%d", &accid);

	//2. 패킷 생성
	pack_DELETEACCOUNT pack = pack_DeleteAccount(accid);

	//3. 서버로 전송
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_deleteaccount_ack(pack_DELETEACCOUNT* msg)
{
	if (msg->result == true)
	{
		cout << "계좌 삭제 성공" << endl;
	}
	else
	{
		cout << "계좌 삭제 실패" << endl;
	}
}

void bank_inputmoney()
{
	printf("계좌 입금\n");

	int  accid, balance;
	
	printf("계좌번호 : ");		scanf_s("%d", &accid);
	printf("입금액 : ");		scanf_s("%d", &balance);
	
	//2. 패킷 생성
	pack_INPUTMONEY pack = pack_InputMoney(accid, balance);

	//3. 서버로 전송
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_inputmoney_ack(pack_INPUTMONEY* msg)
{
	if (msg->result == true)
	{
		cout << "계좌 입금 성공" << endl;
		cout << "입금 후 잔액 : " << msg->money << endl;
	}
	else
	{
		cout << "계좌 입금 실패" << endl;
	}
}

void bank_outputmoney()
{
	printf("계좌 출금\n");

	int  accid, balance;

	printf("계좌번호 : ");		scanf_s("%d", &accid);
	printf("출금액 : ");		scanf_s("%d", &balance);

	//2. 패킷 생성
	pack_OUTPUTMONEY pack = pack_OutputMoney(accid, balance);

	//3. 서버로 전송
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_outputmoney_ack(pack_OUTPUTMONEY* msg)
{
	if (msg->result == true)
	{
		cout << "계좌 출금 성공" << endl;
		cout << "출금 후 잔액 : " << msg->money << endl;
	}
	else
	{
		cout << "계좌 출금 실패" << endl;
	}
}

void bank_selectaccount()
{
	printf("계좌 검색\n");

	int  accid;

	printf("계좌번호 : ");		scanf_s("%d", &accid);

	//2. 패킷 생성
	pack_SELECTACCOUNT pack = pack_SelectAccount(accid);

	//3. 서버로 전송
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_selectaccount_ack(pack_SELECTACCOUNT* msg)
{
	if (msg->result == true)
	{		
		cout << "계좌번호 : " << msg->id << endl;
		cout << "이름 : " << msg->name << endl;
		cout << "잔액 : " << msg->balance << endl;
	}
	else
	{
		cout << "계좌 출금 실패" << endl;
	}
}

void bank_allaccount()
{
	printf("계좌 전체 출력\n");

	//2. 패킷 생성
	pack_ALLACCOUNT pack = pack_AllAccount();

	//3. 서버로 전송
	net_SendData((const char*)&pack, sizeof(pack));
}

void bank_allaccount_ack(pack_ALLACCOUNT* msg)
{
	cout << "저장 개수 : " << msg->size << "\n" << endl;

	for (int i = 0; i < msg->size; i++)
	{
		Account acc = msg->accounts[i];
		acc_print(i, &acc);
	}
}