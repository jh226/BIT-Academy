//control.h
#pragma once

void con_InitDialog(HWND hDlg);
void con_SetTitle(HWND hDlg);
void con_Connect(HWND hDlg);
void con_DisConnect(HWND hDlg);
void con_SendShortData(HWND hDlg);
void con_RecvData(HWND hDlg, WPARAM wParam, LPARAM lParam);