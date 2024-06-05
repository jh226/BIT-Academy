//control.cpp
#include "std.h"

vector<STUDENT*> students;

//�˻� ��޸��������� �ڵ�
HWND hChildDlg = 0;
TCHAR name[20];	//�θ�� �ڽ��� �����ϴ� ������ 

void con_Init(HWND hDlg)
{
	mainui_GetControlHandle(hDlg);
	mainui_CreateListHeader(hDlg);
}

void con_Insert(HWND hDlg)
{
	STUDENT stu;

	//1. ����� ���ؼ� �Է�
	INT_PTR ret = DialogBoxParam(GetModuleHandle(0), MAKEINTRESOURCE(IDD_DIALOG_INSERT), 
		hDlg,(DLGPROC)DlgProcInsert, (LPARAM)&stu);
	if (ret == IDOK)
	{
		STUDENT* pstu = student_create(stu);

		//2. ���� 
		students.push_back(pstu);

		MessageBox(hDlg, TEXT("����Ǿ����ϴ�"), TEXT("�˸�"),MB_OK);
	}
}

void con_Select(HWND hDlg)
{
	// ��޸��� �����. 
	if (hChildDlg == 0)
	{		
		hChildDlg = CreateDialogParam(GetModuleHandle(0),MAKEINTRESOURCE(IDD_DIALOG_SELECT), 
			hDlg, (DLGPROC)DlgProcSelect, (LPARAM)name);
		
		ShowWindow(hChildDlg, SW_SHOW );
	}
	else 
		SetFocus(hChildDlg);//�̹� ������� ��� focus�̵� 
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
		MessageBox(hDlg, TEXT("���� �̸��Դϴ�"), TEXT("�˸�"), MB_OK);
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