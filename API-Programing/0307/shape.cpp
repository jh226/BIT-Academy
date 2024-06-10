//shape.cpp
#include "std.h"

//전역변수 참조 선언(다른 파일에 선언된 변수를 사용할 수 있도록)
extern SHAPE g_shape;

void shape_init(HWND hwnd)
{
	g_shape.color = RGB(255, 0, 0);
	g_shape.pt.x = 100;
	g_shape.pt.y = 50;
	g_shape.size = 75;
	g_shape.type = 3;		// 사각형
	g_shape.width = 3;
}

void shape_print(HWND hwnd, HDC hdc)
{
	HPEN pen = CreatePen(PS_SOLID, g_shape.width, RGB(0, 0, 0));
	HPEN oldpen = (HPEN)SelectObject(hdc, pen);

	HBRUSH br = CreateSolidBrush(g_shape.color);
	HBRUSH oldbr = (HBRUSH)SelectObject(hdc, br);

	POINT pt = g_shape.pt;
	if (g_shape.type == 1)
	{
		Rectangle(hdc, pt.x, pt.y, pt.x + g_shape.size, pt.y + g_shape.size);
	}
	else if (g_shape.type == 2)
	{
		Ellipse(hdc, pt.x, pt.y, pt.x + g_shape.size, pt.y + g_shape.size);
	}
	else if (g_shape.type == 3)
	{
		POINT pgon[3] = { pt.x + g_shape.size / 2, pt.y,
						  pt.x + g_shape.size,	   pt.y + g_shape.size,
						  pt.x ,				   pt.y + g_shape.size };
		Polygon(hdc, pgon, 3);
	}
	DeleteObject(SelectObject(hdc, oldpen));
	DeleteObject(SelectObject(hdc, oldbr));
}

void shape_infoPrint(HWND hwnd, HDC hdc)
{
	TCHAR buf[100];
	wsprintf(buf, TEXT("색상 : RGB(%d, %d, %d) - 0x%x"), GetRValue(g_shape.color),
		GetGValue(g_shape.color), GetBValue(g_shape.color), g_shape.color);
	TextOut(hdc, 5, 5, buf, (int)_tcslen(buf));

	wsprintf(buf, TEXT("좌표 : %d, %d"), g_shape.pt.x, g_shape.pt.y);
	TextOut(hdc, 5, 25, buf, (int)_tcslen(buf));

	wsprintf(buf, TEXT("크기 : %d"), g_shape.size);
	TextOut(hdc, 5, 45, buf, (int)_tcslen(buf));

	TCHAR temp[20];
	if (g_shape.type == 1)	_tcscpy_s(temp, _countof(temp), TEXT("사각형"));
	else if (g_shape.type == 2)	_tcscpy_s(temp, _countof(temp), TEXT("타원"));
	else if (g_shape.type == 3)	_tcscpy_s(temp, _countof(temp), TEXT("삼각형"));
	wsprintf(buf, TEXT("타입 : %s"), temp);
	TextOut(hdc, 5, 65, buf, (int)_tcslen(buf));

	wsprintf(buf, TEXT("두께 : %d"), g_shape.width);
	TextOut(hdc, 5, 85, buf, (int)_tcslen(buf));
}

void shape_setPoint(HWND hwnd, POINT pt)
{
	g_shape.pt = pt;
}

void shape_setWidth(HWND hwnd, int key)
{
	g_shape.width = key - '0';
}

void shape_setColor(HWND hwnd, int key)
{
	if (key == 'R') g_shape.color = RGB(255, 0, 0);
	else if (key == 'G') g_shape.color = RGB(0, 255, 0);
	else if (key == 'B') g_shape.color = RGB(0, 0, 255);
	else if (key == 'M') g_shape.color = RGB(rand() % 256, rand() % 256, rand() % 256);
}

void shape_setType(HWND hwnd, int key)
{
	if (key == VK_UP)
	{
		g_shape.type++;
		if (g_shape.type == 4)
			g_shape.type = 1;
	}
	else if (key == VK_DOWN)
	{
		g_shape.type--;
		if (g_shape.type = 0)
			g_shape.type = 3;
	}
}

void shape_setSize(HWND hwnd, int key)
{
	if (key == VK_LEFT)
	{
		g_shape.size = g_shape.size - 25;
		if (g_shape.size == 0)
			g_shape.size = 100;
	}
	else if (key == VK_RIGHT)
	{
		g_shape.size = g_shape.size + 25;
		if (g_shape.size == 125)
			g_shape.size = 25;
	}
}