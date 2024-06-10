//01_��Ʈ��
/*
* 1) ��Ʈ�� ���� (CreateWindow) : WM_CREATE
* 2)
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

//���� (WM_COMMAND)
#define	IDC_BUTTON1 1
#define IDC_EDIT1	2
#define IDC_STATIC1	3
#define IDC_LIST1	4
#define IDC_CBBOX1	5

//���� (SendMessage)
HWND hbtn1, hedit1, hstatic1, hlist1, hcbbox1;

void  CreateControl(HWND hwnd)
{
	hedit1 = CreateWindow(TEXT("edit"), TEXT("����"),
		WS_CHILD | WS_BORDER | WS_VISIBLE,
		10, 10, 200, 30, hwnd, (HMENU)IDC_EDIT1, 0, 0);

	hstatic1 = CreateWindow(TEXT("static"), TEXT("�б� ����"),
		WS_CHILD | WS_BORDER | WS_VISIBLE,
		10, 50, 200, 30, hwnd, (HMENU)IDC_STATIC1, 0, 0);

	hbtn1 = CreateWindow(TEXT("button"), TEXT("Ŭ��"),
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
	if (LOWORD(wParam) == IDC_BUTTON1)		//��Ʈ��ID
	{
		//SetWindowText(hwnd, TEXT("��ư Ŭ��!"));
		//SetWindowText(hbtn1, TEXT("��ư Ŭ��!"));
		
		//Edit ��Ʈ�� �Է� ���� ȹ��
		TCHAR buf[50] = { 0 };
		GetWindowText(hedit1, buf, _countof(buf));
		if (_tcslen(buf) == 0)
		{
			MessageBox(0, TEXT("���ڸ� �Է��ϼ���."), TEXT("�˸�"), MB_OK);
			return;
		}
		SendMessage(hlist1, LB_ADDSTRING, 0, (LPARAM)buf);
		SendMessage(hcbbox1, CB_ADDSTRING, 0, (LPARAM)buf);
		
		//EDIT ��Ʈ�� �ʱ�ȭ
		SetWindowText(hedit1, TEXT(""));
		SetFocus(hbtn1);
	}
	else if (LOWORD(wParam) == IDC_EDIT1)
	{
		if (HIWORD(wParam) == EN_CHANGE)	//� ���� �ϴ°���?
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
			//ListBox���� ���õ� ���ڸ� ��� ���
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
			//ListBox���� ���õ� ���ڸ� ��� ���
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