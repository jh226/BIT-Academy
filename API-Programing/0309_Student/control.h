//control.h
#pragma once

void con_Init(HWND hDlg);

void con_Insert(HWND hDlg);
void con_Select(HWND hDlg);
void con_SelectAll(HWND hDlg);
void con_Delete(HWND hDlg);


void con_Apply(HWND hDlg);

int NameToIdx(TCHAR* name);				//수정, 삭제
STUDENT* NameToStudent(TCHAR* name);	//수정
