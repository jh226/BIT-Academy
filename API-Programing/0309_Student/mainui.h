//mainui.h
#pragma once

void mainui_GetControlHandle(HWND hDlg);
void mainui_CreateListHeader(HWND hDlg);
void mainui_CountPrint(HWND hDlg, int size);
void mainui_ListPrintAll(HWND hDlg, vector<STUDENT*> students);
void mainui_SelectName(HWND hDlg, STUDENT* name);
