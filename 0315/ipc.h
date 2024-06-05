//ipc.h
#pragma once

//����
BOOL ipc_Connect(HWND hDlg, TCHAR* name);

//���� ����
BOOL ipc_Disconnect(HWND hDlg);

//������ ������ �ڵ� ����
void ipc_ConnectMessage(HWND hDlg, HWND lParam);

//���� - char : ���� �ּ� / int : �� �ּҺ��� size 
int ipc_SendData(HWND hDlg, char* msg, int size);

//����
DATA* ipc_RecvData(HWND hDlg, LPARAM lParam);