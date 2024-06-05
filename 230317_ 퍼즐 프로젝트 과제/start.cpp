//start.cpp
#include "std.h"

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CREATE:		return OnCreate(hwnd, wParam, lParam);
	case WM_PAINT:		return OnPaint(hwnd, wParam, lParam);
	HANDLE_MSG(hwnd, WM_LBUTTONDOWN, OnLButtonDown);
	case WM_COMMAND:	return OnCommand(hwnd, wParam, lParam);
	case WM_DESTROY:	return OnDestroy(hwnd, wParam, lParam);
		/*HANDLE_MSG(hwnd, WM_CREATE, OnCreate);
		HANDLE_MSG(hwnd, WM_PAINT, OnPaint);
		HANDLE_MSG(hwnd, WM_LBUTTONDOWN, OnLButtonDown);
		HANDLE_MSG(hwnd, WM_COMMAND, OnCommand);
		HANDLE_MSG(hwnd, WM_DESTROY, OnDestroy);*/
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//윈도우 클래스 정의
	TCHAR szAppName[] = _T("Puzzle Game");
	WNDCLASS wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)GetStockObject(LTGRAY_BRUSH);
	wc.hCursor = LoadCursor(0, IDC_ARROW);
	wc.hIcon = LoadIcon(0, IDI_WINLOGO);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;
	wc.lpszClassName = szAppName;
	wc.lpszMenuName = MAKEINTRESOURCE(IDR_MENU1);
	wc.style = 0;

	ATOM atom = RegisterClass(&wc);
	HWND hwnd = CreateWindowEx(0, szAppName, szAppName,
		WS_OVERLAPPEDWINDOW & ~WS_THICKFRAME & ~WS_MAXIMIZEBOX,
		CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, 0, 0, hInst, 0);
	ShowWindow(hwnd, nShowCmd);
	UpdateWindow(hwnd);
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return (int)msg.wParam;
}