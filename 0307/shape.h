//shape.h
#pragma once

/*
* 좌표 : 왼쪽 마우스 업
* 펜의 두께 : 키보드 1,2,3
* 브러쉬 색상 : 키보드 R,G,B
* 도형의 타입 : 키보드(상하 방향키)
* 도형의 크기 : 키보드(좌우 방향키)
*/
struct SHAPE
{
	POINT pt;
	int width;		//1~3
	COLORREF color;	//R, G, B, M(Random)
	int type;		//사각형, 타원, 삼각형
	int size;		//25, 50, 75, 100
};

void shape_init(HWND hwnd);

void shape_print(HWND hwnd, HDC hdc);
void shape_infoPrint(HWND hwnd, HDC hdc);

void shape_setPoint(HWND hwnd, POINT pt);
void shape_setWidth(HWND hwnd, int key);
void shape_setColor(HWND hwnd, int key);
void shape_setType(HWND hwnd, int key);
void shape_setSize(HWND hwnd, int key);