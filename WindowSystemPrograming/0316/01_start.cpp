// DLL �Ͻ��� ���
//01_start.cpp
// DLL���� ������ .h / .dll / .lib �ҽ� ���� ������ ����
#include <stdio.h>
#include "cal.h"							//����� (import)

//1) ������Ʈ �Ӽ� >> ��Ŀ >> �Է� >> [�̸� ��ϵ� lib ���ϸ���Ʈ�� �߰�]
//    ** ����� / ������ ���� Ȯ��
//2) ��ó����
//#pragma comment(lib, "0316_CallDll.lib")	//dll����

int main()
{
	printf(" %d\n", add(10, 20));
	printf(" %d\n", sub(10, 20));
	return 0;
}