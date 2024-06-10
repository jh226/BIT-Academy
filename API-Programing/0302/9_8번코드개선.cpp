//9. 8번코드개선
/*
*[ L버튼 클릭시 ] -> [ 해당 위치에 50*50 사각형 출력 ]
* 
* 0) 전역변수 선언
* 1) L버튼이 클릭되면 좌표 정보를 전역변수에 저장한다.
* 2) WM_PAINT 메시지
*	 전역변수에 저장된 모든 좌표를 이용하여 사각형을 출력한다.
* 
* [ D 키보드 클릭시 ] -> [ 무효화 영역 강제 발생 ]
* 
*[ MouseMove ] -> [ 좌상단에 마우스 좌표 출력 ] 
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
		wsprintf(buf, TEXT("마우스 좌표 : %04d : %04d"), pt.x, pt.y);

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
		wsprintf(buf, TEXT("저장개수 : % d"), g_points.size());
		SetWindowText(hwnd, buf);

		return 0;
	}

	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);
		
		//1. 도형 출력
		for (int i = 0; i < (int)g_points.size(); i++)
		{
			POINTS pt = g_points[i];
			Rectangle(hdc, pt.x, pt.y, pt.x + 50, pt.y + 50);
		}

		//2. 좌표 출력
		TCHAR buf[50];
		wsprintf(buf, TEXT("마우스 좌표 : %04d : %04d"), g_pt.x, g_pt.y);
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
		else if (key == 'P')	//점
		{
			HDC hdc = GetDC(hwnd);
			for (int i = 0; i < 100; i++)
			{
				SetPixel(hdc, rand() % 100, rand() % 100, RGB(255, 0, 0));
			}

			ReleaseDC(hwnd, hdc);
		}
		else if (key == 'L')	//라인
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