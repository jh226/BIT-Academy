//calc.cpp
#define DLL_WB_SOURCE

#include<stdio.h> 

#include"cal.h"



int add(int n1, int n2)
{
	message("µ¡¼À");
	return n1 + n2;
}
int sub(int n1, int n2)
{
	message("»¬¼À");
	return n1 - n2;
}
void message(const char* msg)
{
	printf(">> %s\n", msg);
}