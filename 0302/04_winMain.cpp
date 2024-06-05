//04_WinMain ������ �޽��� ó�� �׽�Ʈ
//[���� ���콺 Ŭ����] ->[Ÿ��Ʋ�ٿ� ��ǥ ����] ���

#pragma comment(linker, "/subsystem:windows")  // console

#include <Windows.h>
#include <tchar.h>

//�޽��� ���ν���
//1) WinMain�� WNDCLASS ������ ���
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_LBUTTONDOWN:
	{
		POINTS pt = MAKEPOINTS(lParam);

		//���ڿ�
		TCHAR buf[50];
		wsprintf(buf, TEXT("%d:%d"), pt.x, pt.y);

		//Ÿ��Ʋ�ٿ� ���
		SetWindowText(hwnd, buf);

		return 0;
	}
	//�����찡 ������ �� �ѹ� ȣ��!(�ʱ�ȭ �ܰ�)
	//WinMain���� CreateWindow�Լ��� ȣ���
	// CreateWindow(������ ����(�ڵ����) -> WM_CREATE ȣ�� -> �Լ� ����:�ڵ��ȯ)
	case WM_CREATE:
		return 0;

		//�����찡 �ı��� �� �ѹ� ȣ��(���� �ܰ�)
		//�������� X��ư Ŭ��(WM_CLOSE�޽��� �߻�)
		//  - DefWindowProc(WM_CLOSE) : DestroyWindow() -> WM_DESTROY ȣ��
	case WM_DESTROY:
		//APP Q�� WM_QUIT�� �־��ش�.-> �޽��� ������ ����ȴ�.(WinMain����)
		PostQuitMessage(0);
		return 0;
	}
	return DefWindowProc(hwnd, msg, wParam, lParam);
}


int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//1. ������ Ŭ���� ����
	WNDCLASS wc;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	//System���� ȹ��
	wc.hbrBackground = (HBRUSH)GetStockObject(LTGRAY_BRUSH);
	wc.hCursor = LoadCursor(0, IDC_ARROW);
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;  // �̸� �����Ǵ� �޽��� ���ν���(�޽��� ó�� �Լ�)
	wc.lpszClassName = TEXT("WSBIT"); //�ĺ���, ��ҹ��� ���� ����
	wc.lpszMenuName = 0;  //�޴� ����
	wc.style = 0;

	//2. ������Ʈ���� ���
	RegisterClass(&wc);

	//3. ������ ����(User ��ü)
	HWND hwnd = CreateWindowEx(0,
		TEXT("wsbit"), TEXT("ù��° ������"),
		WS_OVERLAPPEDWINDOW,
		10, 10, 500, 500,
		0, 0, hInst, 0);

	//4. ������ ���
	ShowWindow(hwnd, nShowCmd);

	//5. �޽��� ����
	//GetMessage�� ���� false�� ��ȯ�ұ�?
	//- App Q���� WM_QUIT�� �����ö��� false��ȯ
	MSG msg;
	while (GetMessage(&msg, 0, 0, 0))	//App Q���� �޽��� ��������(Q�� M�� ���ٸ�?��ٸ�)
	{
		DispatchMessage(&msg);			//��ϵ� �޽������ν����� ȣ��!
	}

	return 0;
}