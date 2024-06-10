//01_프로세스테스트콘솔

#include <Windows.h>
#include <iostream>
using namespace std;
#include <tchar.h>

#include "myprocess.h"

//프로세스 사용 예제 - 생성, 소멸, 확인
void exam1()
{
	HANDLE hprocess;
	//(카운트 2)
	if (myp_CreateProcess(_TEXT("0302.exe"), &hprocess) == FALSE)
		//return 0;

	//더이상 제어하지 않겠다. (카운트 1)
	//CloseHandle(hprocess);
	cout << "종료";
	//char dummy = getchar();

	myp_ExitProcess(hprocess);

	cout << "종료코드값 얻기";
	char dummy = getchar();

	if (myp_IsExitProcess(hprocess) == true)
		cout << "종료" << endl;
	else
		cout << "살아있다" << endl;
}

//프로세스 핸들 상속(p31)
void exam2()
{
	//1. 상속이 불가능한 핸들 생성
	HANDLE h = CreateEvent(0, 0, 0, TEXT("key"));

	//2. 생성 후 상속 여부 변경
	SetHandleInformation(h, HANDLE_FLAG_INHERIT, TRUE);

	//-----------------------------------------------------

	//1. 생성시 상속가능하도록 처리
	SECURITY_ATTRIBUTES sa;
	sa.nLength = sizeof(sa);
	sa.bInheritHandle = TRUE;
	sa.lpSecurityDescriptor = 0;	//보안

	HANDLE h1 = CreateEvent(&sa, 0, 0, TEXT("key1"));
}

int main()
{
	exam1();
	
	return 0;
}