//01.Timer
//Ÿ�̸Ӹ� ����ϴ� ������ �ֱ������� [�ݺ�]�� �ʿ��� ���!
// - �簢���� ������ 1�� �������� �����̾�� �Ѵٸ�?

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

void fun(HDC hdc)
{
	static bool b = true;
	if (b)
	{
		Rectangle(hdc, 10, 10, 100, 100);
	}
	else
	{
		HBRUSH br = (HBRUSH)CreateSolidBrush(RGB(255, 0, 0));
		HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);
		Rectangle(hdc, 10, 10, 100, 100);
		DeleteObject(SelectObject(hdc, oldbr));
		b = true;
	}
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{

		return 0;
	}
	case WM_TIMER:
	{
		if (wParam == 10)
		{
			HDC hdc = GetDC(hwnd);

			fun(hdc);
			
			Sleep(1000);
		}
		else if (wParam == 11)
		{
			SYSTEMTIME st;
			GetLocalTime(&st);

			TCHAR buf[100];
			wprintf(buf, TEXT("%04d-%02d-%02d \t %02d:%02d:%02d"),
				st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond);

				SetWindowText(hwnd, buf);
		}
		return 0;
	}
	case WM_CREATE:
	{
		SetTimer(hwnd, 10, 1000, NULL);
		SetTimer(hwnd, 11, 1000, NULL);
		SendMessage(hwnd, WM_TIMER, 11, 0);

		return 0;
	}
	case WM_DESTROY:
	{
		KillTimer(hwnd, 10);
		KillTimer(hwnd, 11);

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