//02_아이콘리소스.cpp
/*
*커서, 아이콘
* 사용 방법 1) 리소스에 등록해서 사용
*				- 아이콘을 리소스에 등록! -> 아이콘ID생성(resource.h)
*				- 실행파일에 아이콘 이미지가 포함된다.
*				
* 사용 방법 2) 디렉토리에서 파일을 읽어와 사용
*				- 실행파일에 포함되지 않는다.
* page 126
* 메뉴 아이템 선택 --> WM_COMMAND	LOWORD(wParam)->MenuItem ID
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
	case WM_INITMENUPOPUP:				//메뉴가 펼쳐지기 직전에 호출!
	{
		HMENU hMenu = GetMenu(hwnd);	//윈도우가 갖고 있는 메뉴 핸들!
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
		//종료
		case ID_40005: SendMessage(hwnd, WM_CLOSE, 0, 0);	break;
		//색상
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
	//윈도우 클래스 정의
	WNDCLASS	wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); //펜, 브러쉬, 폰트
	wc.hCursor = LoadCursor(0, IDC_ARROW);//시스템
	//wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hIcon = LoadIcon(hInst, MAKEINTRESOURCE(IDI_ICON1));
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = MAKEINTRESOURCE(IDR_MENU1);		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("0830"), WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0,
		0, 0, hInst, 0);

	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);

	//메시지 루프
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}