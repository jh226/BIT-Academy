//shape.h
#pragma once

/*
* ��ǥ : ���� ���콺 ��
* ���� �β� : Ű���� 1,2,3
* �귯�� ���� : Ű���� R,G,B
* ������ Ÿ�� : Ű����(���� ����Ű)
* ������ ũ�� : Ű����(�¿� ����Ű)
*/
struct SHAPE
{
	POINT pt;
	int width;		//1~3
	COLORREF color;	//R, G, B, M(Random)
	int type;		//�簢��, Ÿ��, �ﰢ��
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