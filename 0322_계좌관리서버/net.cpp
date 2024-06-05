//net.cpp
#include "std.h"

SOCKET listen_sock;				//��� ����
vector<SOCKET> client_socks;	//��� ���ϵ�

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

//socket()->bind()->listen()
bool net_CreateSocket(int port)
{
	//[�ʱ�ȭ]
	listen_sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (listen_sock == INVALID_SOCKET)
	{
		cout << "���� ���� ����" << endl;
		return false;
	}

	SOCKADDR_IN  addr;
	//memset(&arr, 0, sizeof(SOCKADDR_IN)); //C : C�Լ��� ����������API���
	ZeroMemory(&addr, sizeof(SOCKADDR_IN)); //API

	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);			//PORT�Ҵ�
	addr.sin_addr.S_un.S_addr = htonl(INADDR_ANY);	//IP�Ҵ�	

	int ret = bind(listen_sock, (SOCKADDR*)&addr, sizeof(SOCKADDR_IN));
	if (ret == SOCKET_ERROR)
	{
		cout << "�ּ� ���� ����" << endl;
		return false;
	}

	ret = listen(listen_sock, SOMAXCONN);
	if (ret == SOCKET_ERROR)
	{
		cout << "�� ���� ����" << endl;
		return false;
	}
	
	//Thread����, Thread����� ���� �ʰڴ�.
	CloseHandle(CreateThread(0, 0, Run_Thread, (void*)listen_sock, 0, 0));
	return true;
}

//accept() : Thread Func
unsigned long __stdcall Run_Thread(void* param)
{
	SOCKET sock = (SOCKET)param;
	while (true)
	{
		SOCKADDR_IN c_addr;
		int addrsize = sizeof(c_addr);

		SOCKET client_sock = accept(sock, (SOCKADDR*)&c_addr, &addrsize);
		if (sock == INVALID_SOCKET)
		{
			cout << "Ŭ���̾�Ʈ ���� ó�� ����" << endl;
			continue; //�ݺ��� ������� �̵�!
		}

		char buf[20];
		inet_ntop(AF_INET, &c_addr.sin_addr, buf, sizeof(buf));
		printf("\n[Ŭ���̾�Ʈ ����] %s:%d\n", buf, ntohs(c_addr.sin_port));

		//��� ���� ���� ************************************************
		client_socks.push_back(client_sock);


		//Thread����, Thread����� ���� �ʰڴ�.
		CloseHandle(CreateThread(0, 0, Work_Thread, (void*)client_sock, 0, 0));
	}
}

//recv()->send() : �ݺ�
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

		con_RecvData((HANDLE)sock, recv_buf, ret);

		//1�� 1���
		//SendData(sock, recv_buf, ret);

		//1�� �����
		//SendAllData(sock, recv_buf, ret);
	}
	//������� ***************************************************
	for (int i = 0; i < (int)client_socks.size(); i++)
	{
		if (client_socks[i] == sock)
		{
			client_socks.erase(client_socks.begin() + i);  //���� �ڵ� 
			break;
		}
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
int net_SendData(SOCKET sock, const char* buf, int size)
{
	//���
	int ret = send(sock, (char*)&size, sizeof(int), 0);

	//������
	ret = send(sock, buf, size, 0);
	//printf("���� ����Ʈ : %dbyte)\n", ret);
	return ret;
}

int SendAllData(SOCKET sock, const char* buf, int size)
{
	int ret = 1;
	for (int i = 0; i < (int)client_socks.size(); i++)
	{
		SOCKET s = client_socks[i];
		//if (sock != s)
		//{
			ret = net_SendData(s, buf, size);
		//}
	}
	return ret;
}

void net_Exit()
{
	closesocket(listen_sock);
	WSACleanup();
}