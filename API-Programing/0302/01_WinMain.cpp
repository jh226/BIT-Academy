#include <Windows.h>
#include <tchar.h>

int WINAPI _tWinMain(HINSTANCE hinst, HINSTANCE hprev, LPTSTR lpCmdLine, int nShowCmd)		// �������� �ڵ�, �޽��� ��� ����, 
{																							 // Ÿ��Ʋ�� ���ڿ�, ��ư ��� �� ICON���
	MessageBox(0, TEXT("Hello, API"), TEXT("�˸�"), MB_OKCANCEL);							

	return 0;
}
