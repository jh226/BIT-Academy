//data.cpp
#include "std.h"

DATA data_packetData(const TCHAR* nickname, const TCHAR* message)
{
	DATA data;

	_tcscpy_s(data.nickname, _countof(data.nickname), nickname);
	_tcscpy_s(data.message, _countof(data.message), message);
	GetLocalTime(&data.st);

	return data;
}