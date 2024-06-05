//dlgselect.cpp
#include "std.h"

//��ȭ���� ���ν���(��޸���)
BOOL CALLBACK DlgProcSelect(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static TCHAR* name = NULL;

	switch (msg)
	{
	//���� ȣ�� ����.
	case WM_INITDIALOG:
	{
		name = (TCHAR*)lParam;  //�θ� ������ �ּҸ� �Ҿ������ �ʰ� ����!!
		return TRUE;
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			//���޵� �ּҸ� �̿��ؼ� �θ��� ���� ����!
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
	return FALSE;	//�޽����� ó������ �ʾҴ�.-> �� ������ ��ȭ���� ó���ϴ� default���ν���
}
