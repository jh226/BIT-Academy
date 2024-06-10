//02_mmf_전송.cpp
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

struct LINE
{
	POINTS ptFrom;
	POINTS ptTo;
};

HANDLE hEvent, hMap;
LINE* pData = NULL;

//좌표 보관
LINE line;

LRESULT  OnCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	//1. Event 객체 생성
	hEvent = CreateEvent(0, TRUE, 0, TEXT("Draw_Signal"));
	//2. MMF 처리
	hMap = CreateFileMapping((HANDLE)-1, 0, PAGE_READWRITE, 0, sizeof(LINE), TEXT("map"));
	pData = (LINE*)MapViewOfFile(hMap, FILE_MAP_WRITE, 0, 0, 0);
	if (pData == 0)
		MessageBox(0, TEXT("Fail"), TEXT(""), MB_OK);

	return 0;
}

LRESULT  OnDestroy(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	UnmapViewOfFile(pData);
	CloseHandle(hMap);
	CloseHandle(hEvent);

	PostQuitMessage(0);
	return 0;
}

LRESULT OnLButtonDown(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	line.ptFrom = MAKEPOINTS(lParam);

	return 0;
}

LRESULT OnMouseMove(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	if (wParam & MK_LBUTTON)
	{
		line.ptTo = MAKEPOINTS(lParam);

		//1.그림 그리기
		HDC hdc = GetDC(hwnd);
		MoveToEx(hdc, line.ptFrom.x, line.ptFrom.y, 0);
		LineTo(hdc, line.ptTo.x, line.ptTo.y);
		ReleaseDC(hwnd, hdc);
		
		//2. 공유메모리 저장
		pData->ptFrom = line.ptFrom;
		pData->ptTo = line.ptTo;
		
		line.ptFrom = line.ptTo;	//시작점 변경

		//3. 알림
		SetEvent(hEvent);
	}
	return 0;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:	return OnLButtonDown(hwnd, wParam, lParam);
	case WM_MOUSEMOVE:		return OnMouseMove(hwnd, wParam, lParam);
	case WM_CREATE:			return OnCreate(hwnd, wParam, lParam);
	case WM_DESTROY:		return OnDestroy(hwnd, wParam, lParam);
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
		100, 100, 500, 500,
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