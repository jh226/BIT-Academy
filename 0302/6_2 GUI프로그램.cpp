//6_2 GUI ���α׷�
//skeleton �ڵ�
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
		//DC�� �׷����� ��ȭ��+�׸��� ����(��ȸ��) - GDI���
		//1. ���� �׸��� 
		HDC hdc = GetDC(hwnd);

		Rectangle(hdc, wParam, lParam, wParam+100, lParam +100);
		
		ReleaseDC(hwnd, hdc);	//������

		//2. �޽��� ����
		HWND hwnd_ellipse = FindWindow(NULL, TEXT("Ellipse"));
		if (hwnd_ellipse == NULL)		//���� ��� ���� ó��
		{
			cout << "Ellipse ���α׷��� ���� ����" << endl;
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
		TEXT("FIRST"), TEXT("Sample"), WS_OVERLAPPEDWINDOW,
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