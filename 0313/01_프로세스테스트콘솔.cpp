//01_���μ����׽�Ʈ�ܼ�

#include <Windows.h>
#include <iostream>
using namespace std;
#include <tchar.h>

#include "myprocess.h"

//���μ��� ��� ���� - ����, �Ҹ�, Ȯ��
void exam1()
{
	HANDLE hprocess;
	//(ī��Ʈ 2)
	if (myp_CreateProcess(_TEXT("0302.exe"), &hprocess) == FALSE)
		//return 0;

	//���̻� �������� �ʰڴ�. (ī��Ʈ 1)
	//CloseHandle(hprocess);
	cout << "����";
	//char dummy = getchar();

	myp_ExitProcess(hprocess);

	cout << "�����ڵ尪 ���";
	char dummy = getchar();

	if (myp_IsExitProcess(hprocess) == true)
		cout << "����" << endl;
	else
		cout << "����ִ�" << endl;
}

//���μ��� �ڵ� ���(p31)
void exam2()
{
	//1. ����� �Ұ����� �ڵ� ����
	HANDLE h = CreateEvent(0, 0, 0, TEXT("key"));

	//2. ���� �� ��� ���� ����
	SetHandleInformation(h, HANDLE_FLAG_INHERIT, TRUE);

	//-----------------------------------------------------

	//1. ������ ��Ӱ����ϵ��� ó��
	SECURITY_ATTRIBUTES sa;
	sa.nLength = sizeof(sa);
	sa.bInheritHandle = TRUE;
	sa.lpSecurityDescriptor = 0;	//����

	HANDLE h1 = CreateEvent(&sa, 0, 0, TEXT("key1"));
}

int main()
{
	exam1();
	
	return 0;
}