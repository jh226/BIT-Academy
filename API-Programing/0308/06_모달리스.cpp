//05_��޴�ȭ����
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"	

//�θ�� �ڽİ� �����!
struct COUNTDATA
{
	int x;
	int y;
};

//��ȭ���� ���ν���
BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static COUNTDATA* pdata = NULL;

	switch (msg)
	{
		//���� ȣ�� ����.
	case WM_INITDIALOG:
	{
		pdata = (COUNTDATA*)lParam;  //�θ� ������ �ּҸ� �Ҿ������ �ʰ� ����!!

		//��Ʈ�� �ʱ�ȭ
		SetDlgItemInt(hDlg, IDC_EDIT1, pdata->x, 0);
		SetDlgItemInt(hDlg, IDC_EDIT2, pdata->y, 0);

		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			//���޵� �ּҸ� �̿��ؼ� �θ��� ���� ����!
			pdata->x = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
			pdata->y = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);
			EndDialog(hDlg, IDOK);
			return TRUE;
		}
		case IDCANCEL:
		{
			EndDialog(hDlg, IDCANCEL); return TRUE;
		}
		}
	}
	}
	return FALSE;	//�޽����� ó������ �ʾҴ�.-> �� ������ ��ȭ���� ó���ϴ� default���ν���
}


//������ ���ν���
int g_xcount = 5;
int g_ycount = 5;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//���!!!
	case WM_LBUTTONDOWN:
	{
		COUNTDATA data = { g_xcount, g_ycount };

		INT_PTR ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG2), hwnd,
			(DLGPROC)DlgProc, (LPARAM)&data);
		if (ret == IDOK)
		{
			g_xcount = data.x;
			g_ycount = data.y;
			InvalidateRect(hwnd, 0, TRUE);
		}

		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		RECT rc;
		GetClientRect(hwnd, &rc);
		int width = rc.right - rc.left;
		int height = rc.bottom - rc.top;

		//������		
		for (int i = 1; i <= g_xcount; i++)
		{
			MoveToEx(hdc, i * (width / (g_xcount + 1)), 0, 0);
			LineTo(hdc, i * (width / (g_xcount + 1)), height);
		}

		//������
		for (int i = 1; i <= g_ycount; i++)
		{
			MoveToEx(hdc, 0, i * (height / (g_ycount + 1)), 0);
			LineTo(hdc, width, i * (height / (g_ycount + 1)));
		}

		EndPaint(hwnd, &ps);
		return 0;
	}
	case WM_CREATE:
	{
		return 0;
	}
	case WM_DESTROY:
	{
		PostQuitMessage(0);
		return 0;
	}
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//������ Ŭ���� ����
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //��, �귯��, ��Ʈ
	wc.hCursor = LoadCursor(0, IDC_ARROW);//�ý���
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//�޴� ���
	wc.style = 0;				//������ ��Ÿ��

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);

	//�޽��� ����
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}