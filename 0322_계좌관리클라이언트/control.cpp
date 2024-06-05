//control.cpp

#include "std.h"

//�ʱ�ȭ
void con_init()
{
	if (net_init() == false)
	{
		exit(0);	
	}

	if (net_ConnectSocket(SERVER_IP, SERVER_PORT) == false)
	{
		exit(0);
	}
	cout << "���� ���� ����................" << endl;	
}

//����ó��
void con_exit()
{
	net_Exit();
}

//�м�
void con_RecvData(char* msg, int size)
{
	int* p = (int*)msg;
	if (*p == ACK_MAKEACCOUNT)
	{
		bank_makeaccount_ack((pack_MAKEACCOUNT*)msg);
	}
	else if (*p == ACK_DELETEACCOUNT)
	{
		bank_deleteaccount_ack((pack_DELETEACCOUNT*)msg);
	}	
	else if (*p == ACK_INPUTMONEY)
	{
		bank_inputmoney_ack((pack_INPUTMONEY*)msg);
	}
	else if (*p == ACK_OUTPUTMONEY)
	{
		bank_outputmoney_ack((pack_OUTPUTMONEY*)msg);
	}
	else if (*p == ACK_SELECTACCOUNT)
	{
		bank_selectaccount_ack((pack_SELECTACCOUNT*)msg);
	}
	else if (*p == ACK_ALLACCOUNT)
	{
		bank_allaccount_ack((pack_ALLACCOUNT*)msg);
	}
}
