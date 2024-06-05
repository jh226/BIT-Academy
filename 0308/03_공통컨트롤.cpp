//03_������Ʈ��
//�߰��� ��Ʈ�� (���α׷����� : �۾��帧)
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