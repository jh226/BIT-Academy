//std.h
#pragma once

/*
* 1. ���ҽ��� ��ȭ���ڸ� ����
* 2. 1������ ���� ��ȭ������ �޽����� ó���� ���ν��� ����(�������� ���ν����ʹ� �ٸ���..)
* 3. WinMain������ 1������ ���� ��ȭ���ڸ� �����ϴ� �Լ� ȣ��
*    - �ش��Լ��� ��ȭ���ڰ� ����Ǳ� ������ ������ ����
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#include <vector>
using namespace std;
#include <commctrl.h>
#include "resource.h"

#define WM_APPLY WM_USER + 100	//��޸����� ����ϴ� ����� ���� �޽���

#include "handler.h"
#include "student.h"
#include "control.h"

#include "dlginsert.h"
#include "dlgselect.h"
#include "mainui.h"			//������������ UIó�� 
