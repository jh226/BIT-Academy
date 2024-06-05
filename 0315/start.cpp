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
	return FALSE;	//메시지를 처리하지 않았다.-> 이 다음에 대화상자 처리하는 default프로시저
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	DialogBox(
		hInst,							// instance
		MAKEINTRESOURCE(IDD_DIALOG1),	// 다이얼로그 선택
		0,								// 부모 윈도우
		(DLGPROC)DlgProc);				// Proc..

	return 0;
}