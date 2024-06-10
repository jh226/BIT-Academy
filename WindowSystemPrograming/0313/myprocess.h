// myprocess.h
#pragma once

#include <Windows.h>
#include <tchar.h>

//p21, 프로세스 기본 생성 코드
BOOL myp_CreateProcess(const TCHAR* name, HANDLE *ph);

//p22, 프로세스 종료
BOOL myp_ExitProcess(HANDLE h);

//p24, 자신의 PHandle 얻기
HANDLE myp_GetMyProcessHandle();

//p24, 종료여부 확인
BOOL myp_IsExitProcess(HANDLE h);