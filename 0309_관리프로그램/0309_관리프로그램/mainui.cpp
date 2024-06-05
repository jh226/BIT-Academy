//mainui.cpp

#include "std.h"

HWND hListView; 

void mainui_GetControlHandle(HWND hDlg)
{
	hListView = GetDlgItem(hDlg, IDC_LIST1);
}

void mainui_CreateListHeader(HWND hDlg)
{
	LVCOLUMN COL;

	COL.mask = LVCF_FMT | LVCF_WIDTH | LVCF_TEXT | LVCF_SUBITEM;
	COL.fmt = LVCFMT_LEFT;
	COL.cx = 100;
	COL.pszText = (LPTSTR)TEXT("이름");				
	COL.iSubItem = 0;
	SendMessage(hListView, LVM_INSERTCOLUMN, 0, (LPARAM)&COL);

	COL.pszText = (LPTSTR)TEXT("학년");			
	COL.iSubItem = 1;
	SendMessage(hListView, LVM_INSERTCOLUMN, 1, (LPARAM)&COL);

	COL.pszText = (LPTSTR)TEXT("국어");					
	COL.iSubItem = 2;
	SendMessage(hListView, LVM_INSERTCOLUMN, 2, (LPARAM)&COL);

	COL.pszText = (LPTSTR)TEXT("영어");		
	COL.iSubItem = 3;
	SendMessage(hListView, LVM_INSERTCOLUMN, 3, (LPARAM)&COL);

	COL.pszText = (LPTSTR)TEXT("수학");
	COL.iSubItem = 4;
	SendMessage(hListView, LVM_INSERTCOLUMN, 4, (LPARAM)&COL);

	COL.pszText = (LPTSTR)TEXT("평균");					
	COL.iSubItem = 5;
	SendMessage(hListView, LVM_INSERTCOLUMN, 5, (LPARAM)&COL);
}

void mainui_CountPrint(HWND hDlg, int size)
{
	TCHAR buf[50];
	wsprintf(buf, TEXT("학생수 : %d"), size);
	SetDlgItemText(hDlg, IDC_STATIC1, buf);
}

void mainui_ListPrintAll(HWND hDlg, vector<STUDENT*> students)
{
	//리스트뷰에 등록된 모든 아이템 삭제
	ListView_DeleteAllItems(hListView);

	LVITEM LI;

	LI.mask = LVIF_TEXT;

	for (int i = 0; i < (int)students.size(); i++)
	{
		STUDENT* pst = students[i];

		LI.iItem = i;  //0번째 데이터
		LI.iSubItem = 0;
		LI.pszText = pst->name;			
		SendMessage(hListView, LVM_INSERTITEM, 0, (LPARAM)&LI);

		TCHAR buf[10];
		wsprintf(buf, TEXT("%d"), pst->grade);
		LI.iSubItem = 1;
		LI.pszText = buf;
		SendMessage(hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		wsprintf(buf, TEXT("%d"), pst->kor);
		LI.iSubItem = 2;
		LI.pszText = buf;
		SendMessage(hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		wsprintf(buf, TEXT("%d"), pst->eng);
		LI.iSubItem = 3;
		LI.pszText = buf;
		SendMessage(hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		wsprintf(buf, TEXT("%d"), pst->mat);
		LI.iSubItem = 4;
		LI.pszText = buf;
		SendMessage(hListView, LVM_SETITEM, 0, (LPARAM)&LI);

		wsprintf(buf, TEXT("%.1f"), pst->average);
		LI.iSubItem = 5;
		LI.pszText = buf;
		SendMessage(hListView, LVM_SETITEM, 0, (LPARAM)&LI);
	}
}

void mainui_SelectName(HWND hDlg, STUDENT* pstu)
{
	SetDlgItemText(hDlg, IDC_EDIT1, pstu->name);
	SetDlgItemInt(hDlg, IDC_EDIT5, pstu->grade, 0);
	SetDlgItemInt(hDlg, IDC_EDIT6, pstu->kor, 0);
	SetDlgItemInt(hDlg, IDC_EDIT7, pstu->eng, 0);
	SetDlgItemInt(hDlg, IDC_EDIT8, pstu->mat, 0);
	TCHAR temp[20];
	wsprintf(temp, TEXT("%.1f"), pstu->average);
	SetDlgItemText(hDlg, IDC_EDIT9, temp);
}