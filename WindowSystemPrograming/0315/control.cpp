//control.cpp
#include "std.h"

void con_InitDialog(HWND hDlg)
{
	ui_GetControlHandle(hDlg);
	ui_EnableButton(hDlg, TRUE, FALSE);
	ui_TitleName(hDlg, TEXT("연결전..."));
}

void con_SetTitle(HWND hDlg)
{
	ui_SetName(hDlg);
}

void con_Connect(HWND hDlg)
{
	TCHAR name[20];
	//1. 연결하는 상대의 타이틀명 획득
	ui_GetTargetName(hDlg, name);

	//2. 해당  윈도우 핸들을 획득 (연결)
	//   상대방에게 내 핸들 전달
	if (ipc_Connect(hDlg, name) == TRUE)
	{
		ui_EnableButton(hDlg, FALSE, TRUE);
	}
	else
	{
		MessageBox(NULL, TEXT("연결 실패"), TEXT("알림"), MB_OK);
	}
}

void con_DisConnect(HWND hDlg)
{
	ipc_Disconnect(hDlg);
	ui_TitleName(hDlg, TEXT("연결전..."));
	ui_EnableButton(hDlg, TRUE, FALSE);
}

void con_SendShortData(HWND hDlg)
{
	//보낼 데이터 구성
	TCHAR nickname[20], message[100];
	ui_GetSendData(hDlg, nickname, message);
	DATA data = data_packetData(nickname, message);

	//전송
	ipc_SendData(hDlg, (char*)&data, sizeof(data));
}

void con_RecvData(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	//1. 데이터 수신
	DATA *pdata = ipc_RecvData(hDlg, lParam);

	//2. UI처리
	ui_RecvDataPrint(hDlg, pdata);
}