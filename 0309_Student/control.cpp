//control.cpp
#include "std.h"

vector<STUDENT*> students;

//검색 모달리스윈도우 핸들
HWND hChildDlg = 0;
TCHAR name[20];	//부모와 자식이 공유하는 데이터 

void con_Init(HWND hDlg)
{
	mainui_GetControlHandle(hDlg);
	mainui_CreateListHeader(hDlg);
}

void con_Insert(HWND hDlg)
{
	STUDENT stu;

	//1. 모달을 통해서 입력
	INT_PTR ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG_INSERT), 
		hDlg,(DLGPROC)DlgProcInsert, (LPARAM)&stu);
	if (ret == IDOK)
	{
		STUDENT* pstu = student_create(stu);

		//2. 저장 
		students.push_back(pstu);

		MessageBox(hDlg, TEXT("저장되었습니다"), TEXT("알림"),MB_OK);
	}
}

void con_Select(HWND hDlg)
{
	// 모달리스 만들기. 
	if (hChildDlg == 0)
	{		
		hChildDlg = CreateDialogParam(GetModuleHandle(0),MAKEINTRESOURCE(IDD_DIALOG_SELECT), 
			hDlg, (DLGPROC)DlgProcSelect, (LPARAM)name);
		
		ShowWindow(hChildDlg, SW_SHOW );
	}
	else 
		SetFocus(hChildDlg);//이미 만들어진 경우 focus이동 
}

void con_SelectAll(HWND hDlg)
{
	mainui_CountPrint(hDlg, (int)students.size());
	mainui_ListPrintAll(hDlg, students);
}

void con_Delete(HWND hDlg) {

}

void con_Apply(HWND hDlg)
{
	STUDENT* pstu = NameToStudent(name);
	if (pstu == NULL)
	{
		MessageBox(hDlg, TEXT("없는 이름입니다"), TEXT("알림"), MB_OK);
		return;
	}

	mainui_SelectName(hDlg, pstu);
}

int NameToIdx(TCHAR* name)
{
	for (int i = 0; i < (int)students.size(); i++)
	{
		STUDENT* stu = students[i];
		if (_tcscmp(stu->name, name) == 0)
		{
			return i;
		}
	}
	return -1;
}

STUDENT* NameToStudent(TCHAR* name)
{
	for (int i = 0; i < (int)students.size(); i++)
	{
		STUDENT* stu = students[i];
		if (_tcscmp(stu->name, name) == 0)
		{
			return stu;
		}
	}
	return NULL;
}