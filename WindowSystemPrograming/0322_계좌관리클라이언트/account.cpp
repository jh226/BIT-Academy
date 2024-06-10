//account.cpp

#include "std.h"

void acc_print(int idx, Account* pacc)
{
	printf("[%2d] %s\t%d\t%d\n", idx, pacc->name, pacc->balance, pacc->id);
}