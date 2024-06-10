//ipc.cpp
#include "std.h"

//[������ ��û]�ؼ� ���� �ڵ� ����
HWND targetHandle;

//[��û�� ����]������ ������ ������ �ڵ�
HWND recHandle;

BOOL ipc_Connect(HWND hDlg, TCHAR* name)
{
	targetHandle = FindWindow(0, name);
	if (targetHandle == NULL)
	{
		return FALSE;
	}


	//���濡�� �� �ڵ� ����
	SendMessage(targetHandle, WM_CONNECTHANDLE, 0, (LPARAM)hDlg);
	return TRUE;
}

BOOL ipc_Disconnect(HWND hDlg)
{
	targetHandle = 0;
	return TRUE;
}

void ipc_ConnectMessage(HWND hDlg, HWND lParam)
{
	recHandle = lParam;
}

int ipc_SendData(HWND hDlg, char* msg, int size)
{
	//WM_COPYDATA ���
	COPYDATASTRUCT cds;
	cds.cbData = size;
	cds.dwData;
	cds.lpData = msg;

	SendMessage(targetHandle, WM_COPYDATA, 0, (LPARAM)&cds);

	return 0;
}

DATA* ipc_RecvData(HWND hDlg, LPARAM lParam)
{
	COPYDATASTRUCT* p = (COPYDATASTRUCT*)lParam;	//�ӽ�
	DATA* pdata = (DATA*)p->lpData;
	return pdata;
}