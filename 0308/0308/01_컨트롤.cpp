//01_컨트롤
/*
* 1) 컨트롤 생성 (CreateWindow) : WM_CREATE
* 2)
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//통지 (WM_COMMAND)
#define	IDC_BUTTON1 1
#define IDC_EDIT1	2
#define IDC_STATIC1	3
#define IDC_LIST1	4
#define IDC_CBBOX1	5

//전송 (SendMessage)
HWND hbtn1, hedit1, hstatic1, hlist1, hcbbox1;

void  CreateControl(HWND hwnd)
{
	hedit1 = CreateWindow(TEXT("edit"), TEXT("편집"),
		WS_CHILD | WS_BORDER | WS_VISIBLE,
		10, 10, 200, 30, hwnd, (HMENU)IDC_EDIT1, 0, 0);

	hstatic1 = CreateWindow(TEXT("static"), TEXT("읽기 전용"),
		WS_CHILD | WS_BORDER | WS_VISIBLE,
		10, 50, 200, 30, hwnd, (HMENU)IDC_STATIC1, 0, 0);

	hbtn1 = CreateWindow(TEXT("button"), TEXT("클릭"),
		WS_CHILD | WS_BORDER | WS_VISIBLE,
		220, 10, 110, 30, hwnd, (HMENU)IDC_BUTTON1, 0, 0);

	hlist1 = CreateWindow(TEXT("listbox"), TEXT(""),
		WS_CHILD | WS_BORDER | WS_VISIBLE | LBS_NOTIFY,
		340, 10, 200, 200, hwnd, (HMENU)IDC_LIST1, 0, 0);

	hcbbox1 = CreateWindow(TEXT("combobox"), TEXT(""),
		WS_CHILD | WS_BORDER | WS_VISIBLE | CBS_DROPDOWNLIST,
		550, 10, 200, 200, hwnd, (HMENU)IDC_CBBOX1, 0, 0);
	//
	SendMessage(hlist1, LB_ADDSTRING, 0, (LPARAM)TEXT("AAA"));
	SendMessage(hlist1, LB_ADDSTRING, 0, (LPARAM)TEXT("BBB"));

	SendMessage(hcbbox1, CB_ADDSTRING, 0, (LPARAM)TEXT("CCC"));
	SendMessage(hcbbox1, CB_ADDSTRING, 0, (LPARAM)TEXT("DDD"));
}

void NotifyControl(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	if (LOWORD(wParam) == IDC_BUTTON1)		//컨트롤ID
	{
		//SetWindowText(hwnd, TEXT("버튼 클릭!"));
		//SetWindowText(hbtn1, TEXT("버튼 클릭!"));
		
		//Edit 컨트롤 입력 정보 획득
		TCHAR buf[50] = { 0 };
		GetWindowText(hedit1, buf, _countof(buf));
		if (_tcslen(buf) == 0)
		{
			MessageBox(0, TEXT("글자를 입력하세요."), TEXT("알림"), MB_OK);
			return;
		}
		SendMessage(hlist1, LB_ADDSTRING, 0, (LPARAM)buf);
		SendMessage(hcbbox1, CB_ADDSTRING, 0, (LPARAM)buf);
		
		//EDIT 컨트롤 초기화
		SetWindowText(hedit1, TEXT(""));
		SetFocus(hbtn1);
	}
	else if (LOWORD(wParam) == IDC_EDIT1)
	{
		if (HIWORD(wParam) == EN_CHANGE)	//어떤 말을 하는건지?
		{
			TCHAR buf[50];
			GetWindowText(hedit1, buf, _countof(buf));
			SetWindowText(hstatic1, buf);
		}
	}
	else if (LOWORD(wParam) == IDC_LIST1)
	{
		if (HIWORD(wParam) == LBN_SELCHANGE)
		{
			//ListBox에서 선택된 글자를 얻는 방식
			int row = (int)SendMessage(hlist1, LB_GETCURSEL, 0, 0);
			TCHAR buf[50];
			SendMessage(hlist1, LB_GETTEXT, row, (LPARAM)buf);
			SetWindowText(hwnd, buf);
		}
	}
	else if (LOWORD(wParam) == IDC_CBBOX1)
	{
		if (HIWORD(wParam) == CBN_SELCHANGE)
		{
			//ListBox에서 선택된 글자를 얻는 방식
			int row = (int)SendMessage(hcbbox1, CB_GETCURSEL, 0, 0);
			TCHAR buf[50];
			SendMessage(hcbbox1, CB_GETLBTEXT, row, (LPARAM)buf);
			SetWindowText(hwnd, buf);
		}
	}
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_COMMAND:	NotifyControl(hwnd, wParam, lParam);
	{
		return 0;
	}
	case WM_CREATE:	CreateControl(hwnd);
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