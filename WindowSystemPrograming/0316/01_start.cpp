// DLL 암시적 사용
//01_start.cpp
// DLL에서 생성된 .h / .dll / .lib 소스 파일 폴더에 복사
#include <stdio.h>
#include "cal.h"							//선언부 (import)

//1) 프로젝트 속성 >> 링커 >> 입력 >> [미리 등록된 lib 파일리스트에 추가]
//    ** 디버그 / 릴리즈 버전 확인
//2) 전처리문
//#pragma comment(lib, "0316_CallDll.lib")	//dll정보

int main()
{
	printf(" %d\n", add(10, 20));
	printf(" %d\n", sub(10, 20));
	return 0;
}