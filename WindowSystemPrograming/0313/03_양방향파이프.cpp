#include <Windows.h>
#include <iostream>
using namespace std;

#define BUF_SIZE	4096

HANDLE hread, hwrite;

BOOL getHandle(int argc, char** argv)
{
	if (argc != 3)
	{
		printf("���� ������ �� �����ϴ�. �θ� ������ �ּ���\n");
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
		//1. ����
		memset(buf, 0, sizeof(buf));	//buf�� sizwof(buf)��ŭ 0���� �ٲ۴�.

		DWORD len;
		BOOL b = ReadFile(hread, buf, sizeof(buf), &len, 0);
		if (b == FALSE)
			break;

		printf("[%d]%s\n", len, buf);
		
		//2. �۽�
		printf(">> ");
		gets_s(buf);

		DWORD len1;
		WriteFile(hwrite, buf, (DWORD)(strlen(buf) + 1), &len1, 0);
		printf("%dbyte���ۿϷ�\n\n", len1);
	}

	CloseHandle(hread);
	CloseHandle(hwrite);


	return 0;
}