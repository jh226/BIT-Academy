//02_mmf_����.cpp
//02_mmf_����.cpp
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

DWORD WINAPI ThreadFunc(void* p)
{
	HWND hwnd = (HWND)p;
	while (true)
	{
		//1. �̺�Ʈ ���
		WaitForSingleObject(hEvent, INFINITE);

		//2. ��ǥ ȹ��
		LINE line = *pData;

		//3. �׸���
		HDC hdc = GetDC(hwnd);
		MoveToEx(hdc, line.ptFrom.x, line.ptFrom.y, 0);
		LineTo(hdc, line.ptTo.x, line.ptTo.y);

		ReleaseDC(hwnd, hdc);
	}
}

LRESULT  OnLButtonDown(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	HANDLE h = CreateThread(0, 0, ThreadFunc, hwnd, 0, 0);
	CloseHandle(h);
	return 0;
}

LRESULT  OnCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	//1. Event ��ü ����
	hEvent = OpenEvent(EVENT_ALL_ACCESS, 0, TEXT("Draw_Signal"));

	//2. MMF ó��
	hMap = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, TEXT("map"));
	pData = (LINE*)MapViewOfFile(hMap, FILE_MAP_READ, 0, 0, 0);
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


LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:	return OnLButtonDown(hwnd, wParam, lParam);
	case WM_CREATE:			return OnCreate(hwnd, wParam, lParam);
	case WM_DESTROY:		return OnDestroy(hwnd, wParam, lParam);
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
		100, 100, 500, 500,
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