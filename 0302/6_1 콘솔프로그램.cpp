//6-1 �ܼ����α׷�

#pragma comment (linker, "/subsystem:console")

#include <Windows.h>
#include <iostream>
using namespace std;

#define WM_MYMESSAGE1	WM_USER+100

int main()
{
	cout << "������ �ڵ� ���(�ƹ�Ű�� ��������)" << endl;
	system("pause");

	HWND hwnd = FindWindow(NULL, TEXT("Sample"));
	if (hwnd == NULL)		//���� ��� ���� ó��
	{
		cout << "Sample ���α׷��� ���� ����" << endl;
		return 0;
	}

	while (true)
	{
		SendMessage(hwnd, WM_MYMESSAGE1, rand()%300, rand()%300);

		cout << "X��ǥ : "<<rand()%300 << ", Y��ǥ : " << rand() % 300 << endl;

		Sleep(1000);	// 1�ʰ� ����. sleep(1) = 0.001
	}


	return 0;
}