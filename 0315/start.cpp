//start.cpp
#include "std.h"

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CONNECTHANDLE:	return OnConnextMessage(hDlg, wParam, lParam);
	case WM_COPYDATA:		return OnCopyData(hDlg, wParam, lParam);
	case WM_INITDIALOG:		return OnitDialog(hDlg, wParam, lParam);
	case WM_COMMAND:		return OnCommand(hDlg, wParam, lParam);
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