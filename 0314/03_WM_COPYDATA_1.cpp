//03_WM_COPYDATA_1.cpp

/*
* 1. ���ҽ��� ��ȭ���ڸ� ����
* 2. 1������ ���� ��ȭ������ �޽����� ó���� ���ν��� ����(�������� ���ν����ʹ� �ٸ���..)
* 3. WinMain������ 1������ ���� ��ȭ���ڸ� �����ϴ� �Լ� ȣ��
*    - �ش��Լ��� ��ȭ���ڰ� ����Ǳ� ������ ������ ����
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#include "resource.h"

struct DATA
{
	int flag;			//1:�޽���  2:��ǥ
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
	//���� ������ ����
	DATA data = { 0 };
	data.flag = 1;
	GetDlgItemText(hDlg, IDC_EDIT1, data.nickname, sizeof(data.nickname));
	GetDlgItemText(hDlg, IDC_EDIT3, data.message, sizeof(data.message));

	//WM_COPYDATA
	COPYDATASTRUCT cds;
	cds.cbData = sizeof(DATA);
	cds.dwData = (ULONG_PTR)hDlg;			//�����...
	cds.lpData = &data;

	HWND hwnd = FindWindow(0, TEXT("BBB"));
	if (hwnd == NULL)
	{
		MessageBox(0, TEXT("BBB�� �����ϼ���"), TEXT("�˸�"), MB_OK);
		return;
	}

	SendMessage(hwnd, WM_COPYDATA, 0, (LPARAM)&cds);
}

void SendPointData(HWND hDlg, POINTS pt)
{
	//���� ������ ����
	DATA data = { 0 };
	data.flag = 2;
	data.pt = pt;

	//WM_COPYDATA
	COPYDATASTRUCT cds;
	cds.cbData = sizeof(DATA);
	cds.dwData = (ULONG_PTR)hDlg;			//�����...
	cds.lpData = &data;

	HWND hwnd = FindWindow(0, TEXT("BBB"));
	if (hwnd == NULL)
	{
		MessageBox(0, TEXT("BBB�� �����ϼ���"), TEXT("�˸�"), MB_OK);
		return;
	}

	SendMessage(hwnd, WM_COPYDATA, 0, (LPARAM)&cds);
}

void OnRecvData(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	//1. ������ ����
	COPYDATASTRUCT* p = (COPYDATASTRUCT*)lParam;	//�ӽ�
	hwnd = (HWND)p->dwData;
	DATA* pdata = (DATA*)p->lpData;

	//2. UIó��
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
	return FALSE;	//�޽����� ó������ �ʾҴ�.-> �� ������ ��ȭ���� ó���ϴ� default���ν���
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	DialogBox(
		hInst,							// instance
		MAKEINTRESOURCE(IDD_DIALOG1),	// ���̾�α� ����
		0,								// �θ� ������
		(DLGPROC)DlgProc);				// Proc..

	return 0;
}