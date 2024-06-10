//02_�����ܸ��ҽ�.cpp
/*
*Ŀ��, ������
* ��� ��� 1) ���ҽ��� ����ؼ� ���
*				- �������� ���ҽ��� ���! -> ������ID����(resource.h)
*				- �������Ͽ� ������ �̹����� ���Եȴ�.
*				
* ��� ��� 2) ���丮���� ������ �о�� ���
*				- �������Ͽ� ���Ե��� �ʴ´�.
* page 126
* �޴� ������ ���� --> WM_COMMAND	LOWORD(wParam)->MenuItem ID
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"

COLORREF g_color	RGB(0, 0, 0);

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static HCURSOR h1 = LoadCursorFromFile(TEXT("c:\\windows\\cursors\\size1_i.cur"));
	static HCURSOR h2 = LoadCursorFromFile(TEXT("c:\\windows\\cursors\\size1_il.cur"));
	
	switch (msg)
	{
	case WM_CONTEXTMENU:
	{
		POINT pt = { LOWORD(lParam), HIWORD(lParam) };
		HMENU hmenu = GetMenu(hwnd);
		HMENU hSubMenu = GetSubMenu(hmenu, 2);
		TrackPopupMenu(hSubMenu, TPM_LEFTBUTTON, pt.x, pt.y, 0, hwnd, 0);
	}
	case WM_INITMENUPOPUP:				//�޴��� �������� ������ ȣ��!
	{
		HMENU hMenu = GetMenu(hwnd);	//�����찡 ���� �ִ� �޴� �ڵ�!
		/*
		EnableMenuItem(hMenu, ID_40006, g_color == RGB(255,0,0));
		EnableMenuItem(hMenu, ID_40007, g_color == RGB(0, 255, 0));
		EnableMenuItem(hMenu, ID_40008, g_color == RGB(0, 0, 255));
		EnableMenuItem(hMenu, ID_40016, FALSE);
		*/
		CheckMenuItem(hMenu, ID_40006, g_color == RGB(255, 0, 0) ? MF_CHECKED:MF_UNCHECKED);
		CheckMenuItem(hMenu, ID_40007, g_color == RGB(0, 255, 0) ? MF_CHECKED : MF_UNCHECKED);
		CheckMenuItem(hMenu, ID_40008, g_color == RGB(0, 0, 255) ? MF_CHECKED : MF_UNCHECKED);
		CheckMenuItem(hMenu, ID_40016, FALSE ? MF_CHECKED : MF_UNCHECKED);

		return 0;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		//����
		case ID_40005: SendMessage(hwnd, WM_CLOSE, 0, 0);	break;
		//����
		case ID_40006: g_color = RGB(255, 0, 0); InvalidateRect(hwnd, 0, FALSE); break;
		case ID_40007: g_color = RGB(0, 255, 0); InvalidateRect(hwnd, 0, FALSE); break;
		case ID_40008: g_color = RGB(0, 0, 255); InvalidateRect(hwnd, 0, FALSE); break;
		case ID_40016: g_color = RGB(rand()%256, rand() % 256, rand() % 256); InvalidateRect(hwnd, 0, FALSE); break;
		}
		return 0;
	}
	case WM_SETCURSOR:
	{
		int code = LOWORD(lParam);	//HitTestCode

		if (code == HTLEFT || code == HTRIGHT)
		{
			SetCursor(h2);
			return TRUE;
		}
		if (code == HTTOP || code == HTBOTTOM)
		{
			SetCursor(h1);
			return TRUE;
		}

		return DefWindowProc(hwnd, msg, wParam, lParam);
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		HICON hIcon = LoadIcon(GetModuleHandle(0), MAKEINTRESOURCE(IDI_ICON1));
		DrawIcon(hdc, 10, 10, hIcon);

		RECT rc;
		GetClientRect(hwnd, &rc);
		HBRUSH hbr = CreateSolidBrush(g_color);
		HBRUSH oldbr = (HBRUSH)SelectObject(hdc, hbr);

		Rectangle(hdc, 10, 10, rc.right - rc.left - 10, rc.bottom - rc.top - 10);

		DeleteObject(SelectObject(hdc, oldbr));
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
	//wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hIcon = LoadIcon(hInst, MAKEINTRESOURCE(IDI_ICON1));
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //�̸� ���� �����Ǵ� ���ν���(������ ���� ���)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = MAKEINTRESOURCE(IDR_MENU1);		//�޴� ���
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