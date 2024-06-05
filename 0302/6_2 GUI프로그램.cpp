//6_2 GUI 프로그램
//skeleton 코드
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <iostream>
using namespace std;

#define WM_MYMESSAGE1	WM_USER+100
#define WM_MYMESSAGE2	WM_USER+200

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_MYMESSAGE1:
	{
		//DC는 그려지는 도화지+그리는 도구(일회용) - GDI모듈
		//1. 도형 그리기 
		HDC hdc = GetDC(hwnd);

		Rectangle(hdc, wParam, lParam, wParam+100, lParam +100);
		
		ReleaseDC(hwnd, hdc);	//버리기

		//2. 메시지 전달
		HWND hwnd_ellipse = FindWindow(NULL, TEXT("Ellipse"));
		if (hwnd_ellipse == NULL)		//없는 경우 예외 처리
		{
			cout << "Ellipse 프로그램을 먼저 실행" << endl;
			return 0;
		}
		SendMessage(hwnd_ellipse, WM_MYMESSAGE2, wParam + 100, lParam + 100);

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
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;	 //미리 만들어서 제공되는 프로시저(윈도우 공통 기능)
	wc.lpszClassName = TEXT("First");
	wc.lpszMenuName = 0;		//메뉴 등록
	wc.style = 0;				//윈도우 스타일

	RegisterClass(&wc);

	HWND hwnd = CreateWindowEx(0,
		TEXT("FIRST"), TEXT("Sample"), WS_OVERLAPPEDWINDOW,
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