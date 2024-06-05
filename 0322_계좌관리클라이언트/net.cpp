//net.cpp
#include "std.h"

SOCKET sock;

bool net_init()
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
	{
		cout << "���� �ʱ�ȭ ����" << endl;
		return false;
	}
	return true;
}

//socket()->connect()
bool net_ConnectSocket(const char* ip, int port)
{
	//[�ʱ�ȭ]
	sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sock == INVALID_SOCKET)
	{
		cout << "���� ���� ����" << endl;
		return false;
	}

	//[�����û]
	SOCKADDR_IN  addr;
	ZeroMemory(&addr, sizeof(SOCKADDR_IN)); //API

	int n_addr;
	inet_pton(AF_INET, ip, (char*)&n_addr);

	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);				//PORT�Ҵ�
	addr.sin_addr.S_un.S_addr = n_addr;	//IP�Ҵ�	

	int ret = connect(sock, (SOCKADDR*)&addr, sizeof(SOCKADDR_IN));
	if (ret == SOCKET_ERROR)
	{
		cout << "���� ���� ����" << endl;
		return false;
	}

	CloseHandle(CreateThread(0, 0, Work_Thread, (void*)sock, 0, 0));
	return true;
}

//recv() : �ݺ�
unsigned long __stdcall Work_Thread(void* param)
{
	SOCKET sock = (SOCKET)param;

	//����ó��
	while (true)
	{
		char recv_buf[RECV_BUFF] = { 0 };
		int ret = RecvData(sock, recv_buf, sizeof(recv_buf));
		if (ret == -1)
			break;

		con_RecvData(recv_buf, ret);
	}
	
	closesocket(sock);
	return 0;
}

//����
int RecvData(SOCKET sock, char* buf, int size)
{
	//1. ��Ŷ ��� ȹ��(����ũ��:4byte)
	int packetsize;
	int ret = recv(sock, (char*)&packetsize, sizeof(int), MSG_WAITALL);
	if (ret == 0 || ret == -1)
	{
		return -1;
	}

	//2. ��Ŷ ������ ȹ��(����ũ��:packetsize ��ŭ�� ũ���)
	ret = recv(sock, buf, packetsize, MSG_WAITALL);
	if (ret == -1)
	{
		cout << "���� ����" << endl;
		return -1;
	}
	else if (ret == 0)
	{
		cout << "������ ������ ����" << endl;
		return -1;
	}
	else
	{
		//printf("[����] %dbyte\n", ret);
		return ret;
	}
}

//send()
int net_SendData(const char* buf, int size)
{
	//���
	int ret = send(sock, (char*)&size, sizeof(int), 0);

	//������
	ret = send(sock, buf, size, 0);
	//printf("���� ����Ʈ : %dbyte)\n", ret);
	return ret;
}

void net_Exit()
{
	closesocket(sock);
	WSACleanup();
}