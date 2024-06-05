//handler.cpp
#include "std.h"

BOOL OnitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	con_InitDialog(hDlg);
	return TRUE;
}

BOOL OnCopyData(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	con_RecvData(hDlg, wParam, lParam);

	return TRUE;
}

BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL: EndDialog(hDlg, IDCANCEL);	return TRUE;
	
	case IDC_BUTTON1:	con_Connect(hDlg);		return TRUE;
	case IDC_BUTTON2:	con_DisConnect(hDlg);	return TRUE;
	case IDC_BUTTON3:	con_SetTitle(hDlg);		return TRUE;
	//데이터 전송
	case IDC_BUTTON4:	con_SendShortData(hDlg);	return TRUE;
	}
	return FALSE;
}

BOOL OnConnextMessage(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	ipc_ConnectMessage(hDlg, (HWND)lParam);
	return TRUE;
}