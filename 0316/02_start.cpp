// DLL 명시적 사용(p131)
// 1) 명시적 처리
//   1단계 - LoadLibrary 호출 : DLL이 메모리에 로드
//   2단계 - CLL모듈과 함수명을 알면 해당 주소를 얻을 수 있다.
// 2) 사용
// 3) 사용 후 메모리에서 제거

#include <Windows.h>
#include <stdio.h>

typedef int (*Func)(int, int);

int main()
{
	HMODULE h = LoadLibrary(TEXT("0316_CallDll.dll"));
	if (h == NULL)
	{
		printf("DLL을 못 찾음");
		return 0;
	}
	int (*Add)(int, int) = (int(*)(int,int))GetProcAddress(h, "add");
	Func Sub = (Func)GetProcAddress(h, "sub");

	printf(" %d\n", Add(10, 20));
	printf(" %d\n", Sub(10, 20));
	
	FreeLibrary(h);

	return 0;
}