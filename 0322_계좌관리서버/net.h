//net.h
#pragma once

//�ʱ�ȭ ���� ȣ�� ------------------------------------------------

//con_init()���� ȣ��
bool net_init();
bool net_CreateSocket(int port);

//net_CreateSocket()���� ȣ��
unsigned long __stdcall Run_Thread(void* param);

//Run_Thread()���� ȣ��
unsigned long __stdcall Work_Thread(void* param);

//Run_Thread()���� ȣ��
int RecvData(SOCKET sock, char* buf, int size);
//-----------------------------------------------------------------

int net_SendData(SOCKET sock, const char* buf, int size);
int SendAllData(SOCKET sock, const char* buf, int size);

void net_Exit();
