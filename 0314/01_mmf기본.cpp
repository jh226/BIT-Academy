//mmf�⺻.cpp

#include <windows.h>
#include <iostream>
using namespace std;

int main()
{
	//1.���� ����
	HANDLE hFile = CreateFile(TEXT("a.txt"), GENERIC_READ | GENERIC_WRITE,
		FILE_SHARE_READ | FILE_SHARE_WRITE, 0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

	//2. MMF��ü ���� - ������ �޸�ȭ
	HANDLE hMap = CreateFileMapping(hFile, 0, PAGE_READWRITE, 0, 100, TEXT("map"));

	//3. ���� ��� ��û
	char* p = (char*)MapViewOfFile(hMap, FILE_MAP_WRITE, 0, 0, 0);


	if (p == 0)
		cout << "�޸� �Ҵ� ����";
	else
	{
		strcpy_s(p, 100, "hello");
		p[90] = 'B';
		p[91] = 'C';
	}





	UnmapViewOfFile(p);	// free
	CloseHandle(hMap);
	CloseHandle(hFile);




	return 0;
}