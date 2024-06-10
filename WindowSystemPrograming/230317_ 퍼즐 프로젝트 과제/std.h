#pragma once
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <windows.h>
#include <TCHAR.h>
#include <windowsX.h>
#include <time.h>

#include "resource.h"
#include "handler.h"
#include "puzzle.h"

#define COUNT   5	// Block �� ����
#define EMPTY   (COUNT*COUNT-1)	// ���� ������ ����� ��� �ְ� �Ѵ�.
#define OX		10	// �׸�(���)�� �׷��� ���� ��ǥ
#define OY		10

static HBITMAP	g_hBitmap; 	// �׸�
static SIZE    g_szFull;	// ��Ʈ���� ��ü ũ��
static SIZE    g_szBlock;	// 1�� ����� ũ��( g_szFull / COUNT)
static BOOL  	g_bRunning;	// ������ ���� ���ΰ� ?
static int	g_image[COUNT][COUNT];