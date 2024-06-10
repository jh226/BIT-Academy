//myprocess.cpp
#include "myprocess.h"

BOOL myp_CreateProcess(const TCHAR* name, HANDLE* ph)
{
	TCHAR temp[100];
	_tcscpy_s(temp, _countof(temp), name);

	STARTUPINFO si = { sizeof(si) };
	PROCESS_INFORMATION pi;
	BOOL b = CreateProcess(0, temp, 0, 0, 0, 0, 0, 0, &si, &pi);
	if (b)
	{
		WaitForInputIdle(pi.hProcess, INFINITE);
		*ph = pi.hProcess;
		return TRUE;
	}
	return FALSE;
}

BOOL myp_ExitProcess(HANDLE h)
{
	BOOL b = TerminateProcess(h, 0);	// 2인자 : 종료코드
	return b;
}

HANDLE myp_GetMyProcessHandle()
{
	//자신의 Handle Table에 자신의 Process Handle도 존재한다.
	return GetCurrentProcess();
}

BOOL myp_IsExitProcess(HANDLE h)
{
	DWORD code;
	GetExitCodeProcess(h, &code);
	if (STILL_ACTIVE)		//살아있다
		return false;
	else					//죽었다
		return true;
}