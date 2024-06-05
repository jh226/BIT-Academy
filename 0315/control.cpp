//control.cpp
#include "std.h"

void con_InitDialog(HWND hDlg)
{
	ui_GetControlHandle(hDlg);
	ui_EnableButton(hDlg, TRUE, FALSE);
	ui_TitleName(hDlg, TEXT("������..."));
}

void con_SetTitle(HWND hDlg)
{
	ui_SetName(hDlg);
}

void con_Connect(HWND hDlg)
{
	TCHAR name[20];
	//1. �����ϴ� ����� Ÿ��Ʋ�� ȹ��
	ui_GetTargetName(hDlg, name);

	//2. �ش�  ������ �ڵ��� ȹ�� (����)
	//   ���濡�� �� �ڵ� ����
	if (ipc_Connect(hDlg, name) == TRUE)
	{
		ui_EnableButton(hDlg, FALSE, TRUE);
	}
	else
	{
		MessageBox(NULL, TEXT("���� ����"), TEXT("�˸�"), MB_OK);
	}
}

void con_DisConnect(HWND hDlg)
{
	ipc_Disconnect(hDlg);
	ui_TitleName(hDlg, TEXT("������..."));
	ui_EnableButton(hDlg, TRUE, FALSE);
}

void con_SendShortData(HWND hDlg)
{
	//���� ������ ����
	TCHAR nickname[20], message[100];
	ui_GetSendData(hDlg, nickname, message);
	DATA data = data_packetData(nickname, message);

	//����
	ipc_SendData(hDlg, (char*)&data, sizeof(data));
}

void con_RecvData(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	//1. ������ ����
	DATA *pdata = ipc_RecvData(hDlg, lParam);

	//2. UIó��
	ui_RecvDataPrint(hDlg, pdata);
}