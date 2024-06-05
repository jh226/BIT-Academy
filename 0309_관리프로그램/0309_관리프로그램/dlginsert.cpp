//dlginsert.cpp
#include "std.h"

//대화상자 프로시저(모달)
BOOL CALLBACK DlgProcInsert(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static STUDENT* pdata = NULL;
	static HWND hcombo;

	switch (msg)
	{
		//최초 호출 시점.
	case WM_INITDIALOG:
	{
		pdata = (STUDENT*)lParam;  //부모가 전달한 주소를 잃어버리지 않고 보관!!

		//컨트롤 초기화
		hcombo = GetDlgItem(hDlg, IDC_COMBO1);
		SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)TEXT("<<선택하세요>>"));
		SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)TEXT("1"));
		SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)TEXT("2"));
		SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)TEXT("3"));
		SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)TEXT("4"));

		SendMessage(hcombo, CB_SETCURSEL, 0, 0);

		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			//전달된 주소를 이용해서 부모의 값을 변경!
			GetDlgItemText(hDlg, IDC_EDIT1, pdata->name, _countof(pdata->name));
			//--combobox처리---------------------------------------------
			int row = (int)SendMessage(hcombo, CB_GETCURSEL, 0, 0);
			TCHAR buf[5];
			SendMessage(hcombo, CB_GETLBTEXT, row, (LPARAM)buf);

			//방법1) 문자열 -> 숫자 : int atoi(char*)
			//int value = atoi("111");
			pdata->grade = _tstoi(buf);
			
			//방법2) 콤보박스의 cur위치를 기반으로 획득 row가 선택한 학년이다.
			pdata->grade = row;
			//-----------------------------------------------------------
			pdata->kor = GetDlgItemInt(hDlg, IDC_EDIT2, 0, 0);
			pdata->eng = GetDlgItemInt(hDlg, IDC_EDIT3, 0, 0);
			pdata->mat = GetDlgItemInt(hDlg, IDC_EDIT4, 0, 0);

			EndDialog(hDlg, IDOK);
			return TRUE;
		}
		case IDCANCEL:
		{
			EndDialog(hDlg, IDCANCEL); return TRUE;
		}
		}
	}
	}
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}
