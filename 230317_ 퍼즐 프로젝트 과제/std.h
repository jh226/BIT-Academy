#pragma once
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"

#include <windows.h>
#include <TCHAR.h>
#include <windowsX.h>
#include <time.h>

#include "resource.h"
#include "handler.h"
#include "puzzle.h"

#define COUNT   5	// Block 의 갯수
#define EMPTY   (COUNT*COUNT-1)	// 제일 마지막 블록은 비어 있게 한다.
#define OX		10	// 그림(블록)이 그려질 시작 좌표
#define OY		10

static HBITMAP	g_hBitmap; 	// 그림
static SIZE    g_szFull;	// 비트맵의 전체 크기
static SIZE    g_szBlock;	// 1개 블록의 크기( g_szFull / COUNT)
static BOOL  	g_bRunning;	// 게임이 진행 중인가 ?
static int	g_image[COUNT][COUNT];