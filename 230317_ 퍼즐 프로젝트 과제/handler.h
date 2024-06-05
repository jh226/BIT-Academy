#pragma once

BOOL OnPaint(HWND hwnd, WPARAM wParam, LPARAM lParam);
BOOL OnDestroy(HWND hwnd, WPARAM wParam, LPARAM lParam);
BOOL OnCreate(HWND hwnd, WPARAM wParam, LPARAM lParam);
BOOL OnCommand(HWND hwnd, WPARAM wParam, LPARAM lParam);
BOOL OnLButtonDown(HWND hwnd, BOOL fDoubleClick, int x, int y, UINT keyFlags);

BOOL MoveBlock(HWND hwnd, int x, int y);