//ui.h
#pragma once

void ui_GetControlHandle(HWND hDlg);
void ui_TitleName(HWND hDlg, const TCHAR* titlename);
void ui_SetName(HWND hDlg);
void ui_GetTargetName(HWND hDlg, TCHAR* name);
void ui_GetSendData(HWND hDlg, TCHAR* nickname, TCHAR* msg);
void ui_EnableButton(HWND hDlg, BOOL btn1, BOOL btn2);
void ui_RecvDataPrint(HWND hDlg, DATA* pdata);