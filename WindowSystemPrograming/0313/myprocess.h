// myprocess.h
#pragma once

#include <Windows.h>
#include <tchar.h>

//p21, ���μ��� �⺻ ���� �ڵ�
BOOL myp_CreateProcess(const TCHAR* name, HANDLE *ph);

//p22, ���μ��� ����
BOOL myp_ExitProcess(HANDLE h);

//p24, �ڽ��� PHandle ���
HANDLE myp_GetMyProcessHandle();

//p24, ���Ῡ�� Ȯ��
BOOL myp_IsExitProcess(HANDLE h);