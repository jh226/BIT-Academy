//6-1 콘솔프로그램

#pragma comment (linker, "/subsystem:console")

#include <Windows.h>
#include <iostream>
using namespace std;

#define WM_MYMESSAGE1	WM_USER+100

int main()
{
	cout << "윈도우 핸들 얻기(아무키나 누르세요)" << endl;
	system("pause");

	HWND hwnd = FindWindow(NULL, TEXT("Sample"));
	if (hwnd == NULL)		//없는 경우 예외 처리
	{
		cout << "Sample 프로그램을 먼저 실행" << endl;
		return 0;
	}

	while (true)
	{
		SendMessage(hwnd, WM_MYMESSAGE1, rand()%300, rand()%300);

		cout << "X좌표 : "<<rand()%300 << ", Y좌표 : " << rand() % 300 << endl;

		Sleep(1000);	// 1초간 멈춤. sleep(1) = 0.001
	}


	return 0;
}