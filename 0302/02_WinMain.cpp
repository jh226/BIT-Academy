#pragma comment(linker, "/subsystem:windows")  // 하위 시스템을 console이 아닌 window로 설정

#include <Windows.h>
#include <tchar.h>

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//1. 윈도우 클래스 생성
	WNDCLASS wc;
	wc.cbClsExtra = 0;											//클래스 정보를 저장할 수 있는 예약 영역
	wc.cbWndExtra = 0;										//윈도우 정보를 저장할 수 있는 예약 영역

	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);	//배경 색상을 표현하기 위한 브러시 지정
	wc.hCursor = LoadCursor(0, IDC_ARROW);								//사용할 커서 지정
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);								//윈도우에서 사용할 큰 아이콘의 핸들
	wc.hInstance = hInst;																//윈도우 프로시저를 담은 핸들
	wc.lpfnWndProc = DefWindowProc;											//윈도우 메시지 처리 함수 지정
	wc.lpszClassName = TEXT("WSBIT");										//클래스의 이름을 문자열로 저장
	wc.lpszMenuName = 0;															//윈도우에 부착할 메뉴 지정
	wc.style = 0;																			//윈도우 스타일 정의

	//2. 레지스트리에 등록
	RegisterClass(&wc);

	//3. 윈도우 생성(User 객체)
	HWND hwnd = CreateWindowEx(0,
		TEXT("wsbit"),						//클래스 이름
		TEXT("첫번째 윈도우"),				//윈도우 이름
		WS_OVERLAPPEDWINDOW,		//윈도우 형태 지정
		10, 10, 300, 300,					//윈도우 위치, 크기
		0, 0, hInst, 0);						//부모 윈도우 핸들 지정, 사용할 메뉴 지정, 인스턴스 핸들 지정, 생성 인자 전달

	//4. 윈도우 출력
	ShowWindow(hwnd, nShowCmd);

	MessageBox(NULL, TEXT("Hello, API"), TEXT("알림"), MB_OKCANCEL | MB_ICONQUESTION);

	return 0;
}