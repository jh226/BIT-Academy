//net.h
#pragma once

//초기화 과정 호출 ------------------------------------------------

//con_init()에서 호출
bool net_init();
bool net_CreateSocket(int port);

//net_CreateSocket()에서 호출
unsigned long __stdcall Run_Thread(void* param);

//Run_Thread()에서 호출
unsigned long __stdcall Work_Thread(void* param);

//Run_Thread()에서 호출
int RecvData(SOCKET sock, char* buf, int size);
//-----------------------------------------------------------------

int net_SendData(SOCKET sock, const char* buf, int size);
int SendAllData(SOCKET sock, const char* buf, int size);

void net_Exit();
