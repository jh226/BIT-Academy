//net.h
#pragma once

//�ʱ�ȭ ���� ȣ�� ------------------------------------------------

//con_init()���� ȣ��
bool net_init();
bool net_ConnectSocket(const char* ip, int port);

//net_ConnectSocket()���� ȣ��
unsigned long __stdcall Work_Thread(void* param);

//Work_Thread()���� ȣ��
int RecvData(SOCKET sock, char* buf, int size);
//-----------------------------------------------------------------

int net_SendData(const char* buf, int size);

void net_Exit();
