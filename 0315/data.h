//data.h
#pragma once

struct DATA
{
	TCHAR nickname[20];
	TCHAR message[50];
	SYSTEMTIME st;
};

DATA data_packetData(const TCHAR* nickname, const TCHAR* message);