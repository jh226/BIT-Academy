#include "std.h"

void Swap(int* a, int* b)
{
	int t = *a;
	*a = *b;
	*b = t;
}

void Shuffle(HWND hwnd)
{
	enum { LEFT = 0, RIGHT = 1, UP = 2, DOWN = 3 };
	srand((UINT)time(0));
	int empty_x = 4;
	int empty_y = 4;

	int count = 0;

	while (1)
	{
		switch ((rand() % 4))
		{
		case LEFT: empty_x = max(0, empty_x - 1); break;
		case RIGHT:empty_x = min(4, empty_x + 1); break;
		case UP:   empty_y = max(0, empty_y - 1); break;
		case DOWN: empty_y = min(4, empty_y + 1); break;
		}
		if (MoveBlock(hwnd, empty_x, empty_y))
		{
			++count;
			if (count == COUNT * 50) break;
			//Sleep(50);
		}
	}
}

BOOL IsSuccess()
{
	int k = 0;
	for (int y = 0; y < COUNT; y++)
		for (int x = 0; x < COUNT; x++)
			if (g_image[y][x] != k++) return FALSE;
	return TRUE;
}