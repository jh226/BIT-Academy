//handler.cpp

#include "std.h"

LRESULT OnCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	shape_init(hwnd);

	return TRUE;
}

LRESULT OnDestroy(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	PostQuitMessage(0);

	return TRUE;
}

LRESULT OnPaint(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	PAINTSTRUCT ps;
	HDC hdc = BeginPaint(hwnd, &ps);

	shape_print(hwnd, hdc);

	shape_infoPrint(hwnd, hdc);

	EndPaint(hwnd, &ps);
	return TRUE;
}

LRESULT OnLButtonUp(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	POINT pt = { LOWORD(lParam), HIWORD(lParam) };

	if (pt.x < 100 && pt.y < 100)
		return TRUE;

	shape_setPoint(hwnd, pt);
	InvalidateRect(hwnd, 0, TRUE);
	
	return TRUE;
}

LRESULT OnKeyDown(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
	int key = (int)wParam;		//가상키 코드 (스캔코드, 아스키코드-,WM_CHAR)
	if (key == '1' || key == '2' || key == 3)
		shape_setWidth(hwnd, key);
	else if (key == 'R' || key == 'G' || key == 'B' || key == 'M')
		shape_setColor(hwnd, key);
	else if (key == VK_UP || key == VK_DOWN)
		shape_setType(hwnd, key);
	else if (key == VK_LEFT || key == VK_RIGHT)
		shape_setSize(hwnd, key);



	InvalidateRect(hwnd, 0, TRUE);

	return 0;
}

