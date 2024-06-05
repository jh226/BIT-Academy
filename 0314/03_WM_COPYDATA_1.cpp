//03_WM_COPYDATA_1.cpp

/*
* 1. 리소스로 대화상자를 생성
* 2. 1번에서 만든 대화상자의 메시지를 처리할 프로시저 구현(윈도우기반 프로시저와는 다르다..)
* 3. WinMain에서는 1번에서 만든 대화상자를 실행하는 함수 호출
*    - 해당함수는 대화상자가 종료되기 전까지 리턴을 안함
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#include "resource.h"

struct DATA
{
	int flag;			//1:메시지  2:좌표
	TCHAR nickname[20];
	TCHAR message[50];
	POINTS pt;
};

HWND hwnd;
HWND edit;

void InitDialog(HWND hDlg)
{
	SetWindowText(hDlg, TEXT("AAA"));
	edit = GetDlgItem(hDlg, IDC_EDIT2);
}

void SendShortData(HWND hDlg)
{
	//보낼 데이터 구성
	DATA data = { 0 };
	data.flag = 1;
	GetDlgItemText(hDlg, IDC_EDIT1, data.nickname, sizeof(data.nickname));
	GetDlgItemText(hDlg, IDC_EDIT3, data.message, sizeof(data.message));

	//WM_COPYDATA
	COPYDATASTRUCT cds;
	cds.cbData = sizeof(DATA);
	cds.dwData = (ULONG_PTR)hDlg;			//맘대로...
	cds.lpData = &data;

	HWND hwnd = FindWindow(0, TEXT("BBB"));
	if (hwnd == NULL)
	{
		MessageBox(0, TEXT("BBB를 실행하세요"), TEXT("알림"), MB_OK);
		return;
	}

	SendMessage(hwnd, WM_COPYDATA, 0, (LPARAM)&cds);
}

void SendPointData(HWND hDlg, POINTS pt)
{
	//보낼 데이터 구성
	DATA data = { 0 };
	data.flag = 2;
	data.pt = pt;

	//WM_COPYDATA
	COPYDATASTRUCT cds;
	cds.cbData = sizeof(DATA);
	cds.dwData = (ULONG_PTR)hDlg;			//맘대로...
	cds.lpData = &data;

	HWND hwnd = FindWindow(0, TEXT("BBB"));
	if (hwnd == NULL)
	{
		MessageBox(0, TEXT("BBB를 실행하세요"), TEXT("알림"), MB_OK);
		return;
	}

	SendMessage(hwnd, WM_COPYDATA, 0, (LPARAM)&cds);
}

void OnRecvData(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	//1. 데이터 수신
	COPYDATASTRUCT* p = (COPYDATASTRUCT*)lParam;	//임시
	hwnd = (HWND)p->dwData;
	DATA* pdata = (DATA*)p->lpData;

	//2. UI처리
	if (pdata->flag == 1)
	{
		SYSTEMTIME st;
		GetLocalTime(&st);

		TCHAR buf[100];
		wsprintf(buf, TEXT("[%s] %s (%02d:%02d:%02d)"), pdata->nickname, pdata->message,
			st.wHour, st.wMinute, st.wSecond);

		SendMessage(edit, EM_REPLACESEL, 0, (LPARAM)buf);
		SendMessage(edit, EM_REPLACESEL, 0, (LPARAM)TEXT("\r\n"));
	}
	else if (pdata->flag == 2)
	{
		HDC hdc = GetDC(hDlg);

		POINTS pt = pdata->pt;
		Rectangle(hdc, pt.x - 25, pt.y - 25, pt.x + 25, pt.y + 25);
		ReleaseDC(hDlg, hdc);
	}
}


BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONUP:
	{
		SendPointData(hDlg, MAKEPOINTS(lParam));
		return TRUE;
	}
	case WM_COPYDATA:	OnRecvData(hDlg, wParam, lParam);
	{
		return TRUE;
	}
	case WM_INITDIALOG:
	{
		InitDialog(hDlg);
		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDC_BUTTON1:	SendShortData(hDlg);	return TRUE;
		case IDCANCEL: EndDialog(hDlg, IDCANCEL);	return TRUE;
		}
	}
	}
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	DialogBox(
		hInst,							// instance
		MAKEINTRESOURCE(IDD_DIALOG1),	// 다이얼로그 선택
		0,								// 부모 윈도우
		(DLGPROC)DlgProc);				// Proc..

	return 0;
}