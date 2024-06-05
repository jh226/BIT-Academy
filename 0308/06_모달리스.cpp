//05_모달대화상자
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>
#include "resource.h"	

//부모와 자식간 연결고리!
struct COUNTDATA
{
	int x;
	int y;
};

//대화상자 프로시저
BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static COUNTDATA* pdata = NULL;

	switch (msg)
	{
		//최초 호출 시점.
	case WM_INITDIALOG:
	{
		pdata = (COUNTDATA*)lParam;  //부모가 전달한 주소를 잃어버리지 않고 보관!!

		//컨트롤 초기화
		SetDlgItemInt(hDlg, IDC_EDIT1, pdata->x, 0);
		SetDlgItemInt(hDlg, IDC_EDIT2, pdata->y, 0);

		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			//전달된 주소를 이용해서 부모의 값을 변경!
			pdata->x = GetDlgItemInt(hDlg, IDC_EDIT1, 0, 0);
			pdata->y = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);
			EndDialog(hDlg, IDOK);
			return TRUE;
		}
		case IDCANCEL:
		{
			EndDialog(hDlg, IDCANCEL); return TRUE;
		}
		}
	}
	}
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}


//윈도우 프로시저
int g_xcount = 5;
int g_ycount = 5;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//모달!!!
	case WM_LBUTTONDOWN:
	{
		COUNTDATA data = { g_xcount, g_ycount };

		INT_PTR ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG2), hwnd,
			(DLGPROC)DlgProc, (LPARAM)&data);
		if (ret == IDOK)
		{
			g_xcount = data.x;
			g_ycount = data.y;
			InvalidateRect(hwnd, 0, TRUE);
		}

		return 0;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);

		RECT rc;
		GetClientRect(hwnd, &rc);
		int width = rc.right - rc.left;
		int height = rc.bottom - rc.top;

		//세로줄		
		for (int i = 1; i <= g_xcount; i++)
		{
			MoveToEx(hdc, i * (width / (g_xcount + 1)), 0, 0);
			LineTo(hdc, i * (width / (g_xcount + 1)), height);
		}

		//가로줄
		for (int i = 1; i <= g_ycount; i++)
		{
			MoveToEx(hdc, 0, i * (height / (g_ycount + 1)), 0);
			LineTo(hdc, width, i * (height / (g_ycount + 1)));
		}

		EndPaint(hwnd, &ps);
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