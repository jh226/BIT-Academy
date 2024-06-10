//net.cpp
#include "std.h"

SOCKET listen_sock;				//대기 소켓
vector<SOCKET> client_socks;	//통신 소켓들

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

//socket()->bind()->listen()
bool net_CreateSocket(int port)
{
	//[초기화]
	listen_sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (listen_sock == INVALID_SOCKET)
	{
		cout << "소켓 생성 에러" << endl;
		return false;
	}

	SOCKADDR_IN  addr;
	//memset(&arr, 0, sizeof(SOCKADDR_IN)); //C : C함수는 내부적으로API사용
	ZeroMemory(&addr, sizeof(SOCKADDR_IN)); //API

	addr.sin_family = AF_INET;
	addr.sin_port = htons(port);			//PORT할당
	addr.sin_addr.S_un.S_addr = htonl(INADDR_ANY);	//IP할당	

	int ret = bind(listen_sock, (SOCKADDR*)&addr, sizeof(SOCKADDR_IN));
	if (ret == SOCKET_ERROR)
	{
		cout << "주소 연결 실패" << endl;
		return false;
	}

	ret = listen(listen_sock, SOMAXCONN);
	if (ret == SOCKET_ERROR)
	{
		cout << "망 연결 실패" << endl;
		return false;
	}
	
	//Thread실행, Thread제어는 하지 않겠다.
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
			cout << "클라이언트 접속 처리 실패" << endl;
			continue; //반복문 상단으로 이동!
		}

		char buf[20];
		inet_ntop(AF_INET, &c_addr.sin_addr, buf, sizeof(buf));
		printf("\n[클라이언트 접속] %s:%d\n", buf, ntohs(c_addr.sin_port));

		//통신 소켓 저장 ************************************************
		client_socks.push_back(client_sock);


		//Thread실행, Thread제어는 하지 않겠다.
		CloseHandle(CreateThread(0, 0, Work_Thread, (void*)client_sock, 0, 0));
	}
}

//recv()->send() : 반복
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

		con_RecvData((HANDLE)sock, recv_buf, ret);

		//1대 1통신
		//SendData(sock, recv_buf, ret);

		//1대 다통신
		//SendAllData(sock, recv_buf, ret);
	}
	//통신종료 ***************************************************
	for (int i = 0; i < (int)client_socks.size(); i++)
	{
		if (client_socks[i] == sock)
		{
			client_socks.erase(client_socks.begin() + i);  //삭제 코드 
			break;
		}
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
int net_SendData(SOCKET sock, const char* buf, int size)
{
	//헤더
	int ret = send(sock, (char*)&size, sizeof(int), 0);

	//데이터
	ret = send(sock, buf, size, 0);
	//printf("보낸 바이트 : %dbyte)\n", ret);
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