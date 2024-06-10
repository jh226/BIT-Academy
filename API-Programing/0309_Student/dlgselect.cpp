//dlgselect.cpp
#include "std.h"

//대화상자 프로시저(모달리스)
BOOL CALLBACK DlgProcSelect(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static TCHAR* name = NULL;

	switch (msg)
	{
	//최초 호출 시점.
	case WM_INITDIALOG:
	{
		name = (TCHAR*)lParam;  //부모가 전달한 주소를 잃어버리지 않고 보관!!
		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			//전달된 주소를 이용해서 부모의 값을 변경!
			GetDlgItemText(hDlg, IDC_EDIT1, name, sizeof(name));	

			SendMessage(GetParent(hDlg), WM_APPLY, 0, 0);

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
