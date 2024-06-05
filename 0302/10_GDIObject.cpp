//10_GDIObject (page63)

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//���������Ʈ��� - ��
void exam1(HWND hwnd)
{
	HDC hdc = GetDC(hwnd);

	Rectangle(hdc, 10, 10, 10 + 100, 10 + 100);

	HBRUSH br = (HBRUSH)GetStockObject(GRAY_BRUSH);	// 
	HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);	//������ ��� ����


	Rectangle(hdc, 120, 10, 120 + 100, 10 + 100);

	SelectObject(hdc, oldbr);

	ReleaseDC(hwnd, hdc);
}

//������Ʈ ���� �� ��� - ��
void exam2(HWND hwnd)
{
	HDC hdc = GetDC(hwnd);

	HPEN pen = CreatePen(PS_SOLID, 5, RGB(0, 0, 255));
	HPEN oldpen = (HPEN)SelectObject(hdc, pen);

	Rectangle(hdc, 10, 10, 10 + 100, 10 + 100);

	DeleteObject(SelectObject(hdc, oldpen));		//�Ҹ�
	ReleaseDC(hwnd, hdc);
}

//������Ʈ ���� �� ��� - ��,�귯��
void exam3(HWND hwnd)
{
	
	HDC hdc = GetDC(hwnd);

	HPEN pen = CreatePen(PS_SOLID, 5, RGB(0, 0, 255));
	HBRUSH br = CreateSolidBrush(RGB(255, 255, 0));

	HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);
	HPEN oldpen = (HPEN)SelectObject(hdc, pen);

	Rectangle(hdc, 10, 10, 10 + 100, 10 + 100);

	DeleteObject(SelectObject(hdc, oldpen));		//�Ҹ�
	DeleteObject(SelectObject(hdc, oldbr));

	ReleaseDC(hwnd, hdc);
}

//������Ʈ ��� �� ���� ���� ��� - p.66 - 67
void exam4(HWND hwnd)
{
	HDC hdc = GetDC(hwnd);

	
	HBRUSH br = CreateSolidBrush(RGB(255, 0, 0));
	HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);
	Rectangle(hdc, 10, 10, 10 + 100, 10 + 100);
	DeleteObject(SelectObject(hdc, oldbr));

	HBRUSH br1 = CreateSolidBrush(RGB(0, 255, 0));
	HBRUSH oldbr1 = (HBRUSH)SelectObject(hdc, br1);
	Rectangle(hdc, 110, 10, 110 + 100, 10 + 100);
	DeleteObject(SelectObject(hdc, oldbr1));

	HBRUSH br2 = CreateSolidBrush(RGB(0, 0, 255));
	HBRUSH oldbr2 = (HBRUSH)SelectObject(hdc, br2);
	Rectangle(hdc, 210, 10, 210 + 100, 10 + 100);
	DeleteObject(SelectObject(hdc, oldbr2));

	HBRUSH br3 = CreateSolidBrush(RGB(rand()%256, rand() % 256, rand() % 256));
	HBRUSH oldbr3 = (HBRUSH)SelectObject(hdc, br3);
	Rectangle(hdc, 310, 10, 310 + 100, 10 + 100);
	DeleteObject(SelectObject(hdc, oldbr3));

	ReleaseDC(hwnd, hdc);
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	//�簢�� ������ RGB�� Ȯ��
	case WM_LBUTTONDOWN:
	{
		POINTS pt = MAKEPOINTS(lParam);

		HDC hdc = GetDC(hwnd);

		COLORREF color = GetPixel(hdc, pt.x, pt.y);

		int r = GetRValue(color);
		int g = GetGValue(color);
		int b = GetBValue(color);

		TCHAR buf[50];
		wsprintf(buf, TEXT("%d:%d:%d"), r, g, b);
		SetWindowText(hwnd, buf);

		ReleaseDC(hwnd, hdc);

		return 0;
	}

	case WM_KEYDOWN:
	{
		if (wParam == '1')			exam1(hwnd);
		else if (wParam == '2')		exam2(hwnd);
		else if (wParam == '3')		exam3(hwnd);
		else if (wParam == '4')		exam4(hwnd);
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