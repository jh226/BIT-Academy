//app.c
#include "std.h"

void app_init()
{
	logo();

	//���� ����ó��
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
		ch = _getch();		//���� �ϳ��� �Է¹޴� �Լ�
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
	printf(" ��ۺ�Ʈ ��� 37�� ���� \n");
	printf(" WNP ���� \n");
	printf(" ���� ���� ���α׷� \n");
	printf(" 2023�� 3�� 22�� \n");
	printf(" ȫ�浿 \n");
	printf("************************************\n");
	system("pause");
}


void ending()
{
	system("cls");
	printf("************************************\n");
	printf(" ���α׷��� �����մϴ�.\n");
	printf("************************************\n");
	system("pause");
}

void menuprint()
{
	printf("************************************\n");
	printf(" [1] ���� ����(INSERT)\n");
	printf(" [2] ���� ����(DELETE)\n");
	printf(" [3] ���� ��ü ���(SELECTALL)\n");
	printf(" [4] ���� �˻�(SELECT)\n");
	printf(" [5] ���� �Ա�(UPDATE)\n");
	printf(" [6] ���� ���(UPDATE)\n");
	printf("************************************\n");
	printf(" [7] ���α׷� ����\n");
	printf("************************************\n");
}