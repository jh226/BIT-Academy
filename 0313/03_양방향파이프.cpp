#include <Windows.h>
#include <iostream>
using namespace std;

#define BUF_SIZE	4096

HANDLE hread, hwrite;

BOOL getHandle(int argc, char** argv)
{
	if (argc != 3)
	{
		printf("직접 실행할 수 없습니다. 부모를 실행해 주세요\n");
		return FALSE;
	}

	hread = (HANDLE)atoi(argv[1]);
	hwrite = (HANDLE)atoi(argv[2]);

	return TRUE;
}

int main(int argc, char** argv)
{
	if (getHandle(argc, argv) == FALSE)
		return 0;

	char buf[BUF_SIZE];
	while (true)
	{
		//1. 수신
		memset(buf, 0, sizeof(buf));	//buf의 sizwof(buf)만큼 0으로 바꾼다.

		DWORD len;
		BOOL b = ReadFile(hread, buf, sizeof(buf), &len, 0);
		if (b == FALSE)
			break;

		printf("[%d]%s\n", len, buf);
		
		//2. 송신
		printf(">> ");
		gets_s(buf);

		DWORD len1;
		WriteFile(hwrite, buf, (DWORD)(strlen(buf) + 1), &len1, 0);
		printf("%dbyte전송완료\n\n", len1);
	}

	CloseHandle(hread);
	CloseHandle(hwrite);


	return 0;
}