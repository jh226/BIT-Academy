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
	BOOL b = TerminateProcess(h, 0);	// 2���� : �����ڵ�
	return b;
}

HANDLE myp_GetMyProcessHandle()
{
	//�ڽ��� Handle Table�� �ڽ��� Process Handle�� �����Ѵ�.
	return GetCurrentProcess();
}

BOOL myp_IsExitProcess(HANDLE h)
{
	DWORD code;
	GetExitCodeProcess(h, &code);
	if (STILL_ACTIVE)		//����ִ�
		return false;
	else					//�׾���
		return true;
}