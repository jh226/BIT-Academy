//04_대화상자기반

//01_대화상자기반.cpp
/*
* skeleton 코드가 변경됨.
* 1. 리소스로 대화상자를 생성
* 2. 1번에서 만든 대화상자의 메시지를 처리할 프로시저 구현(윈도우기반 프로시저와는 다르다..)
* 3. WinMain에서는 1번에서 만든 대화상자를 실행하는 함수 호출
*    - 해당함수는 대화상자가 종료되기 전까지 리턴을 안함
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#include "resource.h"

//컨트롤의 ID는 resource.h파일에 생성되어 있다.

//윈도우 핸들은 함수를 통해 획득 : GetDlgItem(), 초기화과정에서 획득(WM_ININTDIALOG)
HWND hEdit, hBtn, hCombo;

//버튼 클릭시 에디트에 있던 문자열을 타이틀바에 출력!

BOOL CALLBACK DlgProc(HWND hDlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_COMMAND:
	{
		switch (LOWORD(wParam) == IDC_BUTTON1)		//컨트롤ID
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
	//최초 호출 시점.
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
			//종료시점.
			//EndDialog : 다이얼로그를 종료하는 함수
			//hDlg : 종료대상 , IDCANCEL : 종료시 반환값
		case IDCANCEL: EndDialog(hDlg, IDCANCEL); return TRUE;
		}
	}
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