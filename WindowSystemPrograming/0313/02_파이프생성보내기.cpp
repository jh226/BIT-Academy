//03_�����������
//02_����������������
/*
* 1. ������ ����
* 2. ������ write
* 3. �ڽ����μ���(hread)
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
		printf("%dbyte���ۿϷ�\n\n", len);
	}

}

void pipe_read(int argc, char** argv)
{
	if (argc != 2)
	{
		printf("���� ������ �� �����ϴ�. �θ� ������ �ּ���\n");
		return;
	}

	HANDLE hRead = (HANDLE)atoi(argv[1]);

	char buf[4096];
	while (true)
	{
		memset(buf, 0, sizeof(buf));	//buf�� sizwof(buf)��ŭ 0���� �ٲ۴�.

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