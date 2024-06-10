#include "std.h"

BOOL OnCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	HINSTANCE hInstace = GetModuleHandle(0);
	g_hBitmap = (HBITMAP)LoadImage(0, TEXT("image\\Puzzle.bmp"), IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
	BITMAP bm;
	GetObject(g_hBitmap, sizeof(bm), &bm);
	g_szFull.cx = bm.bmWidth;
	g_szFull.cy = bm.bmHeight;
	g_szBlock.cx = g_szFull.cx / COUNT;
	g_szBlock.cy = g_szFull.cy / COUNT;
	RECT r = { 0, 0, g_szFull.cx + OX * 2, g_szFull.cy + OY * 2 };
	AdjustWindowRect(&r, GetWindowLong(hwnd, GWL_STYLE), TRUE);
	int width = r.right - r.left + 200;
	int height = r.bottom - r.top;
	int x = (GetSystemMetrics(SM_CXSCREEN) - width) / 2;
	int y = (GetSystemMetrics(SM_CYSCREEN) - height) / 2;
	MoveWindow(hwnd, x, y, width, height, TRUE);

	g_bRunning = FALSE;
	int k = 0;
	for (int y = 0; y < COUNT; y++)
		for (int x = 0; x < COUNT; x++)
			g_image[y][x] = k++;
	return TRUE;
}

BOOL OnPaint(HWND hwnd, WPARAM wParam, LPARAM lparam)
{
	PAINTSTRUCT ps;
	HDC hdc = BeginPaint(hwnd, &ps);
	Rectangle(hdc, OX - 5, OY - 5, OX + g_szFull.cx + 5, OY + g_szFull.cy + 5);
	SetViewportOrgEx(hdc, OX, OY, 0);
	HDC dcMem = CreateCompatibleDC(hdc);
	HBITMAP old = (HBITMAP)SelectObject(dcMem, g_hBitmap);

	for (int y = 0; y < COUNT; y++)
	{
		for (int x = 0; x < COUNT; x++)
		{
			if (g_image[y][x] == EMPTY) // EMPTY
			{
				PatBlt(hdc, x * g_szBlock.cx, y * g_szBlock.cy, g_szBlock.cx, g_szBlock.cy, WHITENESS);
			}
			else
			{
				POINTS pt = { (SHORT)(g_image[y][x] % COUNT), (SHORT)(g_image[y][x] / COUNT) };
				BitBlt(hdc, x * g_szBlock.cx, y * g_szBlock.cy, g_szBlock.cx - 1, g_szBlock.cy - 1,
					dcMem, pt.x * g_szBlock.cx, pt.y * g_szBlock.cy, SRCCOPY);
			}
		}
	}

	TCHAR buf[] = _T("퍼즐 게임");
	TextOut(hdc, 575, 5, buf, _tcslen(buf));

	//wsprintf(buf, TEXT("%02d:%02d:%02"),pdata->st.wHour, pdata->st.wMinute, pdata->st.wSecond);

	/*SYSTEMTIME tmSystem;
	::GetSystemTime(&tmSystem);
	wsprintf(buf, TEXT("%02d:%02d:%02"), tmSystem.wHour, tmSystem.wMinute, tmSystem.wSecond);*/

	SelectObject(dcMem, old);
	DeleteDC(dcMem);
	EndPaint(hwnd, &ps);
	return TRUE;
}

BOOL OnLButtonDown(HWND hwnd, BOOL fDoubleClick, int x, int y, UINT keyFlags)

{
	if ((!g_bRunning) || (x < OX) || (x > OX + g_szFull.cx) ||
		(y < OY) || (y > OY + g_szFull.cy))
		return 0;
	int xBlock = (x - OX) / g_szBlock.cx;
	int yBlock = (y - OY) / g_szBlock.cy;
	if (!MoveBlock(hwnd, xBlock, yBlock))
		MessageBeep(0);
	else
	{
		if (IsSuccess())
		{
			MessageBox(hwnd, TEXT("축하합니다 . ^ ^!"), TEXT("성공!"), MB_OK);
			g_bRunning = FALSE;
		}
	}
	return TRUE;
}

BOOL OnCommand(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case ID_40001:
	{
		if (g_bRunning == TRUE)
		{
			UINT ret = MessageBox(hwnd, TEXT("새로운 게임을 하시겠습니까 ?"), TEXT("확인"), MB_YESNO);
			if (ret != IDYES) return 0;
		}
		Shuffle(hwnd);
		g_bRunning = TRUE;
		return 0;
	}
	case ID_40002:
	{
		if (!g_bRunning) return 0;
		HDC hdc = GetDC(hwnd);
		HDC dcMem = CreateCompatibleDC(hdc);

		HBITMAP old = (HBITMAP)SelectObject(dcMem, g_hBitmap);
		BitBlt(hdc, OX, OY, g_szFull.cx, g_szFull.cy,
			dcMem, 0, 0, SRCCOPY);
		SelectObject(dcMem, old);
		DeleteDC(dcMem);
		ReleaseDC(hwnd, hdc);
		Sleep(2000);
		InvalidateRect(hwnd, 0, FALSE);
		UpdateWindow(hwnd);

		return 0;
	}
	case ID_40003:
	{
		if (g_bRunning == TRUE)
		{
			UINT ret = MessageBox(hwnd, TEXT("현재 게임을 포기 하시겠습니까 ?"), TEXT("확인"), MB_YESNO);
			if (ret != IDYES) return 0;
		}
		InvalidateRect(hwnd, 0, TRUE);
		OnCreate(hwnd, wParam, lParam);
		g_bRunning = FALSE;
		return 0;
	}

	case ID_40004:
	{
		SendMessage(hwnd, WM_CLOSE, 0, 0);
		return 0;
	}
	}
	return FALSE;
}

BOOL OnDestroy(HWND hwnd, WPARAM wParam, LPARAM lparam)
{
	DeleteObject(g_hBitmap);
	PostQuitMessage(0);

	return TRUE;
}

BOOL MoveBlock(HWND hwnd, int x, int y)
{
	// Rectangle Current Block
	RECT r = { x * g_szBlock.cx,y * g_szBlock.cy,(x + 1) * g_szBlock.cx, (y + 1) * g_szBlock.cy };
	if (y > 0 && g_image[y - 1][x] == EMPTY)
	{
		Swap(&g_image[y][x], &g_image[y - 1][x]);
		r.top -= g_szBlock.cy;
	}
	else if (y < COUNT - 1 && g_image[y + 1][x] == EMPTY)
	{
		Swap(&g_image[y][x], &g_image[y + 1][x]);
		r.bottom += g_szBlock.cy;
	}
	else if (x > 0 && g_image[y][x - 1] == EMPTY)
	{
		Swap(&g_image[y][x], &g_image[y][x - 1]);
		r.left -= g_szBlock.cx;
	}
	else if (x < COUNT - 1 && g_image[y][x + 1] == EMPTY)
	{
		Swap(&g_image[y][x], &g_image[y][x + 1]);
		r.right += g_szBlock.cx;
	}
	else
		return FALSE;
	OffsetRect(&r, OX, OY);
	InvalidateRect(hwnd, &r, FALSE);
	UpdateWindow(hwnd);
	return TRUE;
}