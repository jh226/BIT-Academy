//std.h
#pragma once

#define WIN32_LEAN_AND_MEAN  //**** Windows.h , WinSock2.h 충돌방지

#include <Windows.h>
#include <iostream>
#include <vector>
using namespace std;
//--------------------------- [ net ] -------------------------------------
#include <WinSock2.h>				//socket api 함수및 정의...
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")	//dll 정보(노출된 함수 리스트 등)..
//-------------------------------------------------------------------------

#include "account.h"
#include "packet.h"
#include "net.h"
#include "control.h"


