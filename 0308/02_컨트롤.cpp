//02_��Ʈ��
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <Windows.h>
#include <tchar.h>

#define ID_SCRRED		100
#define ID_SCRGREEN		101
#define ID_SCRBLUE		102
#define ID_EDITRED		103
#define ID_EDITGREEN	104
#define ID_EDITBLUE		105

HWND hRed, hGreen, hBlue, hERed, hEBlue, hEGreen;

int Red = 0, Blue = 225, Green = 100;

void CreateChildWindow(HWND hwnd)
{
	hRed = CreateWindow(TEXT("scrollbar"), NULL, WS_CHILD | WS_VISIBLE | SBS_HORZ,
		10, 10, 400, 20, hwnd, (HMENU)ID_SCRRED, 0, NULL);
	hGreen = CreateWindow(TEXT("scrollbar"), NULL, WS_CHILD | WS_VISIBLE | SBS_HORZ,
		10, 40, 400, 20, hwnd, (HMENU)ID_SCRGREEN, 0, NULL);
	hBlue = CreateWindow(TEXT("scrollbar"), NULL, WS_CHILD | WS_VISIBLE | SBS_HORZ,
		10, 70, 400, 20, hwnd, (HMENU)ID_SCRBLUE, 0, NULL);

	hERed = CreateWindow(TEXT("edit"), TEXT(""),
		WS_CHILD | WS_BORDER | WS_VISIBLE,
		420, 10, 50, 20, hwnd, (HMENU)ID_EDITRED, 0, 0);
	
	hEGreen = CreateWindow(TEXT("edit"), TEXT(""),
		WS_CHILD | WS_BORDER | WS_VISIBLE,
		420, 40, 50, 20, hwnd, (HMENU)ID_EDITGREEN, 0, 0);
	
	hEBlue = CreateWindow(TEXT("edit"), TEXT(""),
		WS_CHILD | WS_BORDER | WS_VISIBLE,
		420, 70, 50, 20, hwnd, (HMENU)ID_EDITBLUE, 0, 0);
	
	
	//1
	TCHAR buf[10];
	wprintf(buf, TEXT("%d"), Red);
	SetWindowText(hERed, buf);
	//2
	SetDlgItemInt(hwnd, ID_EDITGREEN, Green, 0);
	//3
	wprintf(buf, TEXT("%d"), Blue);
	SetDlgItemText(hwnd, ID_EDITBLUE, buf);
	//--------------------------------------------
	SetScrollRange(hRed, SB_CTL, 0, 255, TRUE);
	SetScrollPos(hRed, SB_CTL, Red, TRUE);
	SetScrollRange(hGreen, SB_CTL, 0, 255, TRUE);
	SetScrollPos(hGreen, SB_CTL, Green, TRUE);
	SetScrollRange(hBlue, SB_CTL, 0, 255, TRUE);
	SetScrollPos(hBlue, SB_CTL, Blue, TRUE);
}

void HScroll(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	int TempPos = 0;

	//���� �����ߴ°�? -> �� ��ũ���� ���� TempPos�� ����!
	if ((HWND)lParam == hRed)	TempPos = Red;
	if ((HWND)lParam == hGreen) TempPos = Green;
	if ((HWND)lParam == hBlue)	TempPos = Blue;

	//��� Ŭ���ߴ°�? -> ���� ��ġ ���� ȹ��(TempPos)
	switch (LOWORD(wParam)) {
	case SB_LINELEFT:
		TempPos = max(0, TempPos - 1);		// max - �� ���� ���ڸ� �ָ� ū �� ��ȯ
		break;
	case SB_LINERIGHT:
		TempPos = min(255, TempPos + 1);
		break;
	case SB_PAGELEFT:
		TempPos = max(0, TempPos - 10);
		break;
	case SB_PAGERIGHT:
		TempPos = min(255, TempPos + 10);
		break;
	case SB_THUMBTRACK:
		TempPos = HIWORD(wParam);
		break;
	}

	//TempPos������ ��ũ���� ��ġ�� ����
	if ((HWND)lParam == hRed)
	{
		Red = TempPos;
		SetDlgItemInt(hwnd, ID_EDITRED, Red, 0);
	}
	else if ((HWND)lParam == hGreen)	
	{
		Green = TempPos;
		SetDlgItemInt(hwnd, ID_EDITGREEN, Green, 0);
	}
	else if ((HWND)lParam == hBlue)	
	{
		Blue = TempPos;
		SetDlgItemInt(hwnd, ID_EDITBLUE, Blue, 0);
	}
	
	SetScrollPos((HWND)lParam, SB_CTL, TempPos, TRUE);

	//ȭ�� ����
	InvalidateRect(hwnd, NULL, FALSE);
}

void DrawRectangle(HWND hwnd)
{
	PAINTSTRUCT ps;
	HDC hdc = BeginPaint(hwnd, &ps);
	
	HBRUSH MyBrush = CreateSolidBrush(RGB(Red, Green, Blue));
	HBRUSH OldBrush = (HBRUSH)SelectObject(hdc, MyBrush);
	
	Rectangle(hdc, 10, 100, 410, 300);

	DeleteObject(SelectObject(hdc, OldBrush));
	
	EndPaint(hwnd, &ps);
}

void NotifyControl(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	if (LOWORD(wParam) == ID_EDITRED)
	{
		if (HIWORD(wParam) == EN_KILLFOCUS)	//� ���� �ϴ°���?
		{
			int temp = GetDlgItemInt(hwnd, ID_EDITRED, 0, 0);
			if (temp < 0 || temp>255)
			{
				MessageBox(0, TEXT("���� ������ ������ϴ�"), TEXT("�˸�"), MB_OK);
				SetDlgItemInt(hwnd, ID_EDITRED, Red, 0);
				return;
			}
			Red = temp;
			SetScrollRange(hRed, SB_CTL, 0, Red, TRUE);
			InvalidateRect(hwnd, 0, FALSE);
		}
	}
	else if (LOWORD(wParam) == ID_EDITGREEN)
	{
		if (HIWORD(wParam) == EN_KILLFOCUS)	//� ���� �ϴ°���?
		{
			Green = GetDlgItemInt(hwnd, ID_EDITGREEN, 0, 0);
			SetScrollRange(hGreen, SB_CTL, 0, Green, TRUE);
			InvalidateRect(hwnd, 0, FALSE);
		}
	}
	else if (LOWORD(wParam) == ID_EDITBLUE)
	{
		if (HIWORD(wParam) == EN_KILLFOCUS)	//� ���� �ϴ°���?
		{
			Blue = GetDlgItemInt(hwnd, ID_EDITBLUE, 0, 0);
			SetScrollRange(hBlue, SB_CTL, 0, Blue, TRUE);
			InvalidateRect(hwnd, 0, FALSE);
		}
	}
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_COMMAND:	NotifyControl(hwnd, wParam, lParam);
		//H��ũ�� ����
	case WM_HSCROLL:	HScroll(hwnd, wParam, lParam);
	{
		return 0;
	}
	case WM_CREATE:	CreateChildWindow(hwnd);
	{
		return 0;
	}
	case WM_PAINT:	DrawRectangle(hwnd);
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