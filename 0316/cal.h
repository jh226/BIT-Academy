//calc.h
#pragma once

#ifdef DLL_WB_SOURCE
	#define DLLFUNC __declspec(dllexport)
#else
	#define DLLFUNC __declspec(dllimport)
#endif

extern "C" DLLFUNC int add(int n1, int n2);
extern "C" DLLFUNC int sub(int n1, int n2);
void message(const char* msg);
