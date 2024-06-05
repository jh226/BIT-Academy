//std.h
#pragma once

/*
* 1. 리소스로 대화상자를 생성
* 2. 1번에서 만든 대화상자의 메시지를 처리할 프로시저 구현(윈도우기반 프로시저와는 다르다..)
* 3. WinMain에서는 1번에서 만든 대화상자를 실행하는 함수 호출
*    - 해당함수는 대화상자가 종료되기 전까지 리턴을 안함
*/
#pragma comment (linker, "/subsystem:windows")		// "/subsystem:console"
#include <Windows.h>
#include <tchar.h>
#include <vector>
using namespace std;
#include <commctrl.h>
#include "resource.h"

#define WM_APPLY WM_USER + 100	//모달리스가 사용하는 사용자 정의 메시지

#include "handler.h"
#include "student.h"
#include "control.h"

#include "dlginsert.h"
#include "dlgselect.h"
#include "mainui.h"			//메인윈도우의 UI처리 
