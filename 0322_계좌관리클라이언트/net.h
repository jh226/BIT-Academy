//net.h
#pragma once

//초기화 과정 호출 ------------------------------------------------

//con_init()에서 호출
bool net_init();
bool net_ConnectSocket(const char* ip, int port);

//net_ConnectSocket()에서 호출
unsigned long __stdcall Work_Thread(void* param);

//Work_Thread()에서 호출
int RecvData(SOCKET sock, char* buf, int size);
//-----------------------------------------------------------------

int net_SendData(const char* buf, int size);

void net_Exit();
