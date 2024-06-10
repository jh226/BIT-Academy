//ipc.cpp
#include "std.h"

//[접속을 요청]해서 상대방 핸들 보관
HWND targetHandle;

//[요청을 당한]상대방이 나에게 전달한 핸들
HWND recHandle;

BOOL ipc_Connect(HWND hDlg, TCHAR* name)
{
	targetHandle = FindWindow(0, name);
	if (targetHandle == NULL)
	{
		return FALSE;
	}


	//상대방에게 내 핸들 전달
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
	//WM_COPYDATA 통신
	COPYDATASTRUCT cds;
	cds.cbData = size;
	cds.dwData;
	cds.lpData = msg;

	SendMessage(targetHandle, WM_COPYDATA, 0, (LPARAM)&cds);

	return 0;
}

DATA* ipc_RecvData(HWND hDlg, LPARAM lParam)
{
	COPYDATASTRUCT* p = (COPYDATASTRUCT*)lParam;	//임시
	DATA* pdata = (DATA*)p->lpData;
	return pdata;
}