//03_공통컨트롤
//추가된 컨트롤 (프로그래스바 : 작업흐름)
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <CommCtrl.h>

#define IDC_PROGRESS1	1
HWND hprogress;

void CreateChildWindow(HWND hwnd)
{
	hprogress = CreateWindow(TEXT("msctls_progress32"), NULL,
		WS_CHILD | WS_VISIBLE | WS_BORDER | PBS_SMOOTH,
		10, 10, 400, 20, hwnd, (HMENU)IDC_PROGRESS1, 0, NULL);

	SendMessage(hprogress, PBM_SETRANGE32, 0, 100);
	SendMessage(hprogress, PBM_SETPOS, 10, 0);
}
void ScrollTest(HWND hwnd)
{
	for (int i = 0; i < 100; i++)
	{
		SendMessage(hprogress, PBM_SETPOS, i, 0);
		Sleep(1000);
	}
}


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:	ScrollTest(hwnd);
	{
		return 0;
	}
	case WM_CREATE: CreateChildWindow(hwnd);
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