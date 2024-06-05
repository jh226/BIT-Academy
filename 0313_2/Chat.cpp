//2개의 파이프를 생성
//하나는 보내는 용도
//하나는 받는 용도

#include <iostream>
#include <Windows.h>
using namespace std;

#define	BUF_SIZE	4096

HANDLE hread, hwrite;		//hwrite
HANDLE hread1, hwrite1;		//hread1

void createPipe()
{
	CreatePipe(&hread, &hwrite, 0, BUF_SIZE);
	SetHandleInformation(hread, HANDLE_FLAG_INHERIT, TRUE);

	CreatePipe(&hread1, &hwrite1, 0, BUF_SIZE);
	SetHandleInformation(hwrite1, HANDLE_FLAG_INHERIT, TRUE);
}

void childPrpcessCreate()
{
	TCHAR cmd[256];
	wsprintf(cmd, TEXT("child.exe %d %d"), hread, hwrite1);

	PROCESS_INFORMATION pi;
	STARTUPINFO si = { sizeof(si) };

	BOOL b = CreateProcess(0, cmd, 0, 0, TRUE, CREATE_NEW_CONSOLE, 0, 0, &si, &pi);
	if (b)
	{
		CloseHandle(pi.hProcess);
		CloseHandle(pi.hThread);
		CloseHandle(hread);
		CloseHandle(hwrite1);
	}
}

int main()
{
	createPipe();
	childPrpcessCreate();

	char buf[BUF_SIZE];
	char buf1[BUF_SIZE];
	while (true)
	{
		//전송
		printf(">> ");
		gets_s(buf);

		DWORD len;
		WriteFile(hwrite, buf, (DWORD)(strlen(buf) + 1), &len, 0);
		printf("%dbyte전송완료\n\n", len);
		
		//수신
		memset(buf1, 0, sizeof(buf1));
		BOOL b = ReadFile(hread1, buf1, sizeof(buf1), &len, 0);
		if (b == FALSE)
			break;
		printf("\t[%d] %s\n", len, buf1);
	}

	return 0;
}