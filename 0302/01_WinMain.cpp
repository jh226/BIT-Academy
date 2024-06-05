#include <Windows.h>
#include <tchar.h>

int WINAPI _tWinMain(HINSTANCE hinst, HINSTANCE hprev, LPTSTR lpCmdLine, int nShowCmd)		// 윈도우의 핸들, 메시지 출력 내용, 
{																							 // 타이틀바 문자열, 버튼 모양 및 ICON모양
	MessageBox(0, TEXT("Hello, API"), TEXT("알림"), MB_OKCANCEL);							

	return 0;
}
