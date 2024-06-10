//9. 8���ڵ尳��
/*
*[ L��ư Ŭ���� ] -> [ �ش� ��ġ�� 50*50 �簢�� ��� ]
* 
* 0) �������� ����
* 1) L��ư�� Ŭ���Ǹ� ��ǥ ������ ���������� �����Ѵ�.
* 2) WM_PAINT �޽���
*	 ���������� ����� ��� ��ǥ�� �̿��Ͽ� �簢���� ����Ѵ�.
* 
* [ D Ű���� Ŭ���� ] -> [ ��ȿȭ ���� ���� �߻� ]
* 
*[ MouseMove ] -> [ �»�ܿ� ���콺 ��ǥ ��� ] 
* 
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include <vector>
using namespace std;

vector<POINTS> g_points;
POINTS g_pt;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_MOUSEMOVE:
	{
		//POINTS pt = MAKEPOINTS(lParam);
		
		g_pt = MAKEPOINTS(lParam);

		RECT rc = { 0,0,200,20 };
		InvalidateRect(hwnd, &rc, TRUE);


		/*TCHAR buf[50];
		wsprintf(buf, TEXT("���콺 ��ǥ : %04d : %04d"), pt.x, pt.y);

		HDC hdc = GetDC(hwnd);

		TextOut(hdc, 5, 5, buf, _tcslen(buf));

		ReleaseDC(hwnd, hdc);*/

		return 0;
	}
	case WM_LBUTTONDOWN:
	{
		POINTS pt = MAKEPOINTS(lParam);

		g_points.push_back(pt);

		//InvalidateRect(hwnd, 0, TRUE);
		//InvalidateRect(hwnd, 0, FALSE);
		RECT rt = { pt.x, pt.y, pt.x + 50, pt.y + 50 };
		InvalidateRect(hwnd, &rt, FALSE);

		TCHAR buf[50];
		wsprintf(buf, TEXT("���尳�� : % d"), g_points.size());
		SetWindowText(hwnd, buf);

		return 0;
	}

	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);
		
		//1. ���� ���
		for (int i = 0; i < (int)g_points.size(); i++)
		{
			POINTS pt = g_points[i];
			Rectangle(hdc, pt.x, pt.y, pt.x + 50, pt.y + 50);
		}

		//2. ��ǥ ���
		TCHAR buf[50];
		wsprintf(buf, TEXT("���콺 ��ǥ : %04d : %04d"), g_pt.x, g_pt.y);
		TextOut(hdc, 5, 5, buf, _tcslen(buf));

		EndPaint(hwnd, &ps);
		return 0;
	}

	case WM_KEYDOWN:
	{
		char key = wParam;
		if (key == 'D')
		{
			//InvalidateRect(hwnd, 0, TRUE);

			RECT rc = { 0,0,200,200 };
			InvalidateRect(hwnd, &rc, TRUE);
		}
		else if (key == 'P')	//��
		{
			HDC hdc = GetDC(hwnd);
			for (int i = 0; i < 100; i++)
			{
				SetPixel(hdc, rand() % 100, rand() % 100, RGB(255, 0, 0));
			}

			ReleaseDC(hwnd, hdc);
		}
		else if (key == 'L')	//����
		{
			HDC hdc = GetDC(hwnd);

			MoveToEx(hdc, 50, 50, NULL);
			LineTo(hdc, 250, 50);
			LineTo(hdc, 250, 300);

			ReleaseDC(hwnd, hdc);
		}


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