//std.h
#pragma once

#define WIN32_LEAN_AND_MEAN  //**** Windows.h , WinSock2.h �浹����

#include <Windows.h>
#include <iostream>
#include <vector>
using namespace std;
//--------------------------- [ net ] -------------------------------------
#include <WinSock2.h>				//socket api �Լ��� ����...
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")	//dll ����(����� �Լ� ����Ʈ ��)..
//-------------------------------------------------------------------------

#include "account.h"
#include "packet.h"
#include "net.h"
#include "control.h"


