//04_��ȭ���ڱ��

//01_��ȭ���ڱ��.cpp
/*
* skeleton �ڵ尡 �����.
* 1. ���ҽ��� ��ȭ���ڸ� ����
* 2. 1������ ���� ��ȭ������ �޽����� ó���� ���ν��� ����(�������� ���ν����ʹ� �ٸ���..)
* 3. WinMain������ 1������ ���� ��ȭ���ڸ� �����ϴ� �Լ� ȣ��
*    - �ش��Լ��� ��ȭ���ڰ� ����Ǳ� ������ ������ ����
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#include "resource.h"

//��Ʈ���� ID�� resource.h���Ͽ� �����Ǿ� �ִ�.

//������ �ڵ��� �Լ��� ���� ȹ�� : GetDlgItem(), �ʱ�ȭ�������� ȹ��(WM_ININTDIALOG)
HWND hEdit, hBtn, hCombo;

//��ư Ŭ���� ����Ʈ�� �ִ� ���ڿ��� Ÿ��Ʋ�ٿ� ���!

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_COMMAND:
	{
		switch (LOWORD(wParam) == IDC_BUTTON1)		//��Ʈ��ID
		{
		case IDC_BUTTON1:
		{
			TCHAR buf[50] = { 0 };
			GetWindowText(hEdit, buf, _countof(buf));
			SetWindowText(hDlg, buf);
			return TRUE;
		}
		case IDCANCEL: EndDialog(hDlg, IDCANCEL);	return TRUE;
		}
		return FALSE;
	}
	//���� ȣ�� ����.
	case WM_INITDIALOG:
	{
		hEdit = GetDlgItem(hDlg, IDC_EDIT1);
		hBtn = GetDlgItem(hDlg, IDC_BUTTON1);
		hCombo = GetDlgItem(hDlg, IDC_COMBO1);

		return TRUE;
	
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
			//�������.
			//EndDialog : ���̾�α׸� �����ϴ� �Լ�
			//hDlg : ������ , IDCANCEL : ����� ��ȯ��
		case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
		}
	}
	}
	return FALSE;	//�޽����� ó������ �ʾҴ�.-> �� ������ ��ȭ���� ó���ϴ� default���ν���
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	DialogBox(
		hInst,							// instance
		MAKEINTRESOURCE(IDD_DIALOG1),	// ���̾�α� ����
		0,								// �θ� ������
		(DLGPROC)DlgProc);				// Proc..

	return 0;
}