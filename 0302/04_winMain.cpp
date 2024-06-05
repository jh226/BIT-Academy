//04_WinMain 간단한 메시지 처리 테스트
//[왼쪽 마우스 클릭시] ->[타이틀바에 좌표 정보] 출력

#pragma comment(linker, "/subsystem:windows")  // console

#include <Windows.h>
#include <tchar.h>

//메시지 프로시저
//1) WinMain의 WNDCLASS 정보에 등록
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		POINTS pt = MAKEPOINTS(lParam);

		//문자열
		TCHAR buf[50];
		wsprintf(buf, TEXT("%d:%d"), pt.x, pt.y);

		//타이틀바에 출력
		SetWindowText(hwnd, buf);

		return 0;
	}
	//윈도우가 생성될 때 한번 호출!(초기화 단계)
	//WinMain에서 CreateWindow함수가 호출됨
	// CreateWindow(윈도우 생성(핸들생성) -> WM_CREATE 호출 -> 함수 종료:핸들반환)
	case WM_CREATE:
		return 0;

		//윈도우가 파괴될 때 한번 호출(종료 단계)
		//윈도우의 X버튼 클릭(WM_CLOSE메시지 발생)
		//  - DefWindowProc(WM_CLOSE) : DestroyWindow() -> WM_DESTROY 호출
	case WM_DESTROY:
		//APP Q에 WM_QUIT를 넣어준다.-> 메시지 루프가 종료된다.(WinMain종료)
		PostQuitMessage(0);
		return 0;
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//1. 윈도우 클래스 생성
	WNDCLASS wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	//System에서 획득
	wc.hbrBackground = (HBRUSH)GetStockObject(LTGRAY_BRUSH);
	wc.hCursor = LoadCursor(0, IDC_ARROW);
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;  // 미리 제공되는 메시지 프로시저(메시지 처리 함수)
	wc.lpszClassName = TEXT("WSBIT"); //식별자, 대소문자 구분 안함
	wc.lpszMenuName = 0;  //메뉴 없음
	wc.style = 0;

	//2. 레지스트리에 등록
	RegisterClass(&wc);

	//3. 윈도우 생성(User 객체)
	HWND hwnd = CreateWindowEx(0,
		TEXT("wsbit"), TEXT("첫번째 윈도우"),
		WS_OVERLAPPEDWINDOW,
		10, 10, 500, 500,
		0, 0, hInst, 0);

	//4. 윈도우 출력
	ShowWindow(hwnd, nShowCmd);

	//5. 메시지 루프
	//GetMessage는 언제 false를 반환할까?
	//- App Q에서 WM_QUIT를 가져올때만 false반환
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))	//App Q에서 메시지 가져오기(Q에 M이 없다면?기다림)
	{
		DispatchMessage(&msg);			//등록된 메시지프로시저를 호출!
	}

	return 0;
}