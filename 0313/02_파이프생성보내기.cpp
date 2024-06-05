//03_양방향파이프
//02_파이프생성보내기
/*
* 1. 파이프 생성
* 2. 본인은 write
* 3. 자식프로세스(hread)
*/
#include <Windows.h>
#include <iostream>
using namespace std;

//p32
void pipe_write()
{
	HANDLE hRead, hWrite;
	CreatePipe(&hRead, &hWrite, 0, 4096);
	SetHandleInformation(hRead, HANDLE_FLAG_INHERIT, TRUE);

	TCHAR cmd[256];
	wsprintf(cmd, TEXT("child.exe %d"), hRead);

	PROCESS_INFORMATION pi;
	STARTUPINFO si = { sizeof(si) };

	BOOL b = CreateProcess(0, cmd, 0, 0, TRUE, CREATE_NEW_CONSOLE, 0, 0, &si, &pi);
	if (b)
	{
		CloseHandle(pi.hProcess);
		CloseHandle(pi.hThread);
		CloseHandle(hRead);
	}

	char buf[4096];
	while (true)
	{
		printf(">> ");
		gets_s(buf);

		DWORD len;
		WriteFile(hWrite, buf, (DWORD)(strlen(buf) + 1), &len, 0);
		printf("%dbyte전송완료\n\n", len);
	}

}

void pipe_read(int argc, char** argv)
{
	if (argc != 2)
	{
		printf("직접 실행할 수 없습니다. 부모를 실행해 주세요\n");
		return;
	}

	HANDLE hRead = (HANDLE)atoi(argv[1]);

	char buf[4096];
	while (true)
	{
		memset(buf, 0, sizeof(buf));	//buf의 sizwof(buf)만큼 0으로 바꾼다.

		DWORD len;
		BOOL b = ReadFile(hRead, buf, sizeof(buf), &len, 0);
		if (b == FALSE)
			break;

		printf("[%d]%s\n", len, buf);
	}

	CloseHandle(hRead);
}

int main(int argc, char** argv)
{
	pipe_write();

	return 0;
}