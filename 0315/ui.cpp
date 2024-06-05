//ui.cpp
#include "std.h"

HWND butn1;
HWND butn2;
HWND edit1;

void ui_GetControlHandle(HWND hDlg)
{
	butn1 = GetDlgItem(hDlg, IDC_BUTTON1);
	butn2 = GetDlgItem(hDlg, IDC_BUTTON2);
	edit1 = GetDlgItem(hDlg, IDC_EDIT3);
}

void ui_SetName(HWND hDlg)
{
	TCHAR name[20];
	GetDlgItemText(hDlg, IDC_EDIT1, name, sizeof(TCHAR) * 20);
	SetWindowText(hDlg, name);
}

void ui_TitleName(HWND hDlg, const TCHAR* titlename)
{
	SetWindowText(hDlg, titlename);
}

void ui_GetTargetName(HWND hDlg, TCHAR* name)
{
	GetDlgItemText(hDlg, IDC_EDIT2, name, sizeof(TCHAR) * 20);
}

void ui_GetSendData(HWND hDlg, TCHAR* nickname, TCHAR* msg)
{
	GetDlgItemText(hDlg, IDC_EDIT1, nickname, sizeof(TCHAR) * 20);
	GetDlgItemText(hDlg, IDC_EDIT4, msg, sizeof(TCHAR) * 100);
}

void ui_EnableButton(HWND hDlg, BOOL btn1, BOOL btn2)
{
	EnableWindow(butn1, btn1);
	EnableWindow(butn2, btn2);
}

void ui_RecvDataPrint(HWND hDlg, DATA* pdata)
{
	TCHAR buf[100];
	wsprintf(buf, TEXT("[%s] %s (%02d:%02d:%02d)"), pdata->nickname, pdata->message,
		pdata->st.wHour, pdata->st.wMinute, pdata->st.wSecond);

	SendMessage(edit1, EM_REPLACESEL, 0, (LPARAM)buf);
	SendMessage(edit1, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
}