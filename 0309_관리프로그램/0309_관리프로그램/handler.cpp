//handler.cpp

#include "std.h"

BOOL OnInitDialog(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	con_Init(hDlg);

	return TRUE;
}

BOOL OnCommand(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	switch (LOWORD(wParam))
	{
	case IDCANCEL: return OnCancel(hDlg); 
	//�ҷ����� ��ư
	case IDC_BUTTON1:	con_SelectAll(hDlg);	return TRUE;
	//�л����� �޴�
	case ID_STU_INSERT: con_Insert(hDlg);		return TRUE;
	case ID_STU_SELECT: con_Select(hDlg);		return TRUE;
	case ID_STU_DELETE: con_Delete(hDlg);		return TRUE;
	}
	return FALSE;
}

BOOL OnApply(HWND hDlg, WPARAM wParam, LPARAM lParam)
{
	con_Apply(hDlg);

	return TRUE;
}

BOOL OnCancel(HWND hDlg)
{
	EndDialog(hDlg, IDCANCEL);

	return TRUE;
}