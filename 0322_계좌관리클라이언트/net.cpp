//net.cpp
#include "std.h"

SOCKET sock;

bool net_init()
{
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
	{
		cout << "소켓 초기화 실패" << endl;
		return false;
	}
	return true;
}

//socket()->connect()
bool net_ConnectSocket(const char* ip, int port)
{
	//[초기화]
	sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sock == INVALID_SOCKET)
	{
		cout << "소켓 생성 에러" << endl;
		return false;
	}

	//[연결요청]
	SOCKADDR_IN  addr;
	ZeroMemory(&addr, sizeof(SOCKADDR_IN)); //API

	int n_addr;
	inet_pton(AF_INET, ip, (char*)&n_addr);

	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);				//PORT할당
	addr.sin_addr.S_un.S_addr = n_addr;	//IP할당	

	int ret = connect(sock, (SOCKADDR*)&addr, sizeof(SOCKADDR_IN));
	if (ret == SOCKET_ERROR)
	{
		cout << "서버 연결 실패" << endl;
		return false;
	}

	CloseHandle(CreateThread(0, 0, Work_Thread, (void*)sock, 0, 0));
	return true;
}

//recv() : 반복
unsigned long __stdcall Work_Thread(void* param)
{
	SOCKET sock = (SOCKET)param;

	//수신처리
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

//수신
int RecvData(SOCKET sock, char* buf, int size)
{
	//1. 패킷 헤더 획득(고정크기:4byte)
	int packetsize;
	int ret = recv(sock, (char*)&packetsize, sizeof(int), MSG_WAITALL);
	if (ret == 0 || ret == -1)
	{
		return -1;
	}

	//2. 패킷 데이터 획득(가변크기:packetsize 만큼의 크기로)
	ret = recv(sock, buf, packetsize, MSG_WAITALL);
	if (ret == -1)
	{
		cout << "수신 오류" << endl;
		return -1;
	}
	else if (ret == 0)
	{
		cout << "상대방이 소켓을 종료" << endl;
		return -1;
	}
	else
	{
		//printf("[수신] %dbyte\n", ret);
		return ret;
	}
}

//send()
int net_SendData(const char* buf, int size)
{
	//헤더
	int ret = send(sock, (char*)&size, sizeof(int), 0);

	//데이터
	ret = send(sock, buf, size, 0);
	//printf("보낸 바이트 : %dbyte)\n", ret);
	return ret;
}

void net_Exit()
{
	closesocket(sock);
	WSACleanup();
}