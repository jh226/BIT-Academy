//02_�ڵ��Ӻ�����.cpp
//����� ����
#include <iostream>
using namespace std;

//argc(count) : ����������� ����
//argv(value) :	���ڿ���
//���߰��� �׽�Ʈ : ������Ʈ >> �Ӽ� >> ����� >> ��� �μ�(111 �ѱ� 10.123 aaa)
//Ȯ�� ������ �ٽ� ����

int main(int argc, char** argv)
{
	/*for (int i = 0; i < argc; i++)
	{
		printf("[%d] %s\n", i, argv[i]);
	}*/

	if (argc != 4)
	{
		cout << "����� ���� ����" << endl;
		cout << "ȣ�⿹) xxx.exe 10 + 3" << endl;
		return -1;
	}

	int num1 = atoi(argv[1]);
	char ch = argv[2][0];
	int num2 = atoi(argv[3]);

	if (ch == '+')
	{
		printf("%d %c %d = %d\n", num1, ch, num2, num1 + num2);
	}
	else if (ch == '-')
	{
		printf("%d %c %d = %d\n", num1, ch, num2, num1 - num2);
	}

	return 0;
}