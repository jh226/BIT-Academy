#pragma comment(linker, "/subsystem:windows")  // ���� �ý����� console�� �ƴ� window�� ����

#include <Windows.h>
#include <tchar.h>

int WINAPI _tWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPTSTR lpCmdLine, int nShowCmd)
{
	//1. ������ Ŭ���� ����
	WNDCLASS wc;
	wc.cbClsExtra = 0;											//Ŭ���� ������ ������ �� �ִ� ���� ����
	wc.cbWndExtra = 0;										//������ ������ ������ �� �ִ� ���� ����

	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);	//��� ������ ǥ���ϱ� ���� �귯�� ����
	wc.hCursor = LoadCursor(0, IDC_ARROW);								//����� Ŀ�� ����
	wc.hIcon = LoadIcon(0, IDI_APPLICATION);								//�����쿡�� ����� ū �������� �ڵ�
	wc.hInstance = hInst;																//������ ���ν����� ���� �ڵ�
	wc.lpfnWndProc = DefWindowProc;											//������ �޽��� ó�� �Լ� ����
	wc.lpszClassName = TEXT("WSBIT");										//Ŭ������ �̸��� ���ڿ��� ����
	wc.lpszMenuName = 0;															//�����쿡 ������ �޴� ����
	wc.style = 0;																			//������ ��Ÿ�� ����

	//2. ������Ʈ���� ���
	RegisterClass(&wc);

	//3. ������ ����(User ��ü)
	HWND hwnd = CreateWindowEx(0,
		TEXT("wsbit"),						//Ŭ���� �̸�
		TEXT("ù��° ������"),				//������ �̸�
		WS_OVERLAPPEDWINDOW,		//������ ���� ����
		10, 10, 300, 300,					//������ ��ġ, ũ��
		0, 0, hInst, 0);						//�θ� ������ �ڵ� ����, ����� �޴� ����, �ν��Ͻ� �ڵ� ����, ���� ���� ����

	//4. ������ ���
	ShowWindow(hwnd, nShowCmd);

	MessageBox(NULL, TEXT("Hello, API"), TEXT("�˸�"), MB_OKCANCEL | MB_ICONQUESTION);

	return 0;
}