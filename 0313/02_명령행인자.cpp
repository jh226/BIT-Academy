//02_핸들상속보내기.cpp
//명령행 인자
#include <iostream>
using namespace std;

//argc(count) : 명령행인자의 개수
//argv(value) :	문자열들
//개발과정 테스트 : 프로젝트 >> 속성 >> 디버깅 >> 명령 인수(111 한글 10.123 aaa)
//확인 끝나면 다시 제거

int main(int argc, char** argv)
{
	/*for (int i = 0; i < argc; i++)
	{
		printf("[%d] %s\n", i, argv[i]);
	}*/

	if (argc != 4)
	{
		cout << "명령행 인자 오류" << endl;
		cout << "호출예) xxx.exe 10 + 3" << endl;
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