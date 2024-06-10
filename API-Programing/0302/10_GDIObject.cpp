//10_GDIObject (page63)

#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//스톡오브젝트사용 - 펜
void exam1(HWND hwnd)
{
	HDC hdc = GetDC(hwnd);

	Rectangle(hdc, 10, 10, 10 + 100, 10 + 100);

	HBRUSH br = (HBRUSH)GetStockObject(GRAY_BRUSH);	// 
	HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);	//손으로 드는 행위


	Rectangle(hdc, 120, 10, 120 + 100, 10 + 100);

	SelectObject(hdc, oldbr);

	ReleaseDC(hwnd, hdc);
}

//오브젝트 생성 및 사용 - 펜
void exam2(HWND hwnd)
{
	HDC hdc = GetDC(hwnd);

	HPEN pen = CreatePen(PS_SOLID, 5, RGB(0, 0, 255));
	HPEN oldpen = (HPEN)SelectObject(hdc, pen);

	Rectangle(hdc, 10, 10, 10 + 100, 10 + 100);

	DeleteObject(SelectObject(hdc, oldpen));		//소멸
	ReleaseDC(hwnd, hdc);
}

//오브젝트 생성 및 사용 - 펜,브러쉬
void exam3(HWND hwnd)
{
	
	HDC hdc = GetDC(hwnd);

	HPEN pen = CreatePen(PS_SOLID, 5, RGB(0, 0, 255));
	HBRUSH br = CreateSolidBrush(RGB(255, 255, 0));

	HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);
	HPEN oldpen = (HPEN)SelectObject(hdc, pen);

	Rectangle(hdc, 10, 10, 10 + 100, 10 + 100);

	DeleteObject(SelectObject(hdc, oldpen));		//소멸
	DeleteObject(SelectObject(hdc, oldbr));

	ReleaseDC(hwnd, hdc);
}

//오브젝트 사용 및 색생 정보 얻기 - p.66 - 67
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
	//사각형 색상의 RGB값 확인
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