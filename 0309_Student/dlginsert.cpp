//dlginsert.cpp
#include "std.h"

//��ȭ���� ���ν���(���)
BOOL CALLBACK DlgProcInsert(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	static STUDENT* pdata = NULL;
	static HWND hcombo;

	switch (msg)
	{
		//���� ȣ�� ����.
	case WM_INITDIALOG:
	{
		pdata = (STUDENT*)lParam;  //�θ� ������ �ּҸ� �Ҿ������ �ʰ� ����!!

		//��Ʈ�� �ʱ�ȭ
		hcombo = GetDlgItem(hDlg, IDC_COMBO1);
		SendMessage(hcombo, CB_ADDSTRING, 0, (LPARAM)TEXT("<<�����ϼ���>>"));
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
			//���޵� �ּҸ� �̿��ؼ� �θ��� ���� ����!
			GetDlgItemText(hDlg, IDC_EDIT1, pdata->name, _countof(pdata->name));
			//--comboboxó��---------------------------------------------
			int row = (int)SendMessage(hcombo, CB_GETCURSEL, 0, 0);
			TCHAR buf[5];
			SendMessage(hcombo, CB_GETLBTEXT, row, (LPARAM)buf);

			//���1) ���ڿ� -> ���� : int atoi(char*)
			//int value = atoi("111");
			pdata->grade = _tstoi(buf);
			
			//���2) �޺��ڽ��� cur��ġ�� ������� ȹ�� row�� ������ �г��̴�.
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
	return FALSE;	//�޽����� ó������ �ʾҴ�.-> �� ������ ��ȭ���� ó���ϴ� default���ν���
}
