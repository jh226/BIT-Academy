// DLL ����� ���(p131)
// 1) ����� ó��
//   1�ܰ� - LoadLibrary ȣ�� : DLL�� �޸𸮿� �ε�
//   2�ܰ� - CLL���� �Լ����� �˸� �ش� �ּҸ� ���� �� �ִ�.
// 2) ���
// 3) ��� �� �޸𸮿��� ����

#include <Windows.h>
#include <stdio.h>

typedef int (*Func)(int, int);

int main()
{
	HMODULE h = LoadLibrary(TEXT("0316_CallDll.dll"));
	if (h == NULL)
	{
		printf("DLL�� �� ã��");
		return 0;
	}
	int (*Add)(int, int) = (int(*)(int,int))GetProcAddress(h, "add");
	Func Sub = (Func)GetProcAddress(h, "sub");

	printf(" %d\n", Add(10, 20));
	printf(" %d\n", Sub(10, 20));
	
	FreeLibrary(h);

	return 0;
}