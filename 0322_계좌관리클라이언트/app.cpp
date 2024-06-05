//app.c
#include "std.h"

void app_init()
{
	logo();

	//서버 연결처리
	con_init();
//	myarr1_init(&g_myarr);
}

void app_run()
{
	char ch;
	while (1)
	{
		system("cls");
		menuprint();
		ch = _getch();		//문자 하나를 입력받는 함수
		switch (ch)
		{
		case '1':		bank_makeaccount();		break;
		case '2':		bank_deleteaccount();	break;
		case '3':		bank_allaccount();		break;
		case '4':		bank_selectaccount();	break;
		case '5':		bank_inputmoney();		break;
		case '6':		bank_outputmoney();		break;
		case '7':		return;
		}
		Sleep(1000);
		system("pause");
	}	
}

void app_exit()
{
	ending();
	con_exit();
}

void logo()
{
	system("cls");
	printf("************************************\n");
	printf(" 우송비트 고급 37기 과정 \n");
	printf(" WNP 과정 \n");
	printf(" 계좌 관리 프로그램 \n");
	printf(" 2023년 3월 22일 \n");
	printf(" 홍길동 \n");
	printf("************************************\n");
	system("pause");
}


void ending()
{
	system("cls");
	printf("************************************\n");
	printf(" 프로그램을 종료합니다.\n");
	printf("************************************\n");
	system("pause");
}

void menuprint()
{
	printf("************************************\n");
	printf(" [1] 계좌 생성(INSERT)\n");
	printf(" [2] 계좌 삭제(DELETE)\n");
	printf(" [3] 계좌 전체 출력(SELECTALL)\n");
	printf(" [4] 계좌 검색(SELECT)\n");
	printf(" [5] 계좌 입금(UPDATE)\n");
	printf(" [6] 계좌 출금(UPDATE)\n");
	printf("************************************\n");
	printf(" [7] 프로그램 종료\n");
	printf("************************************\n");
}