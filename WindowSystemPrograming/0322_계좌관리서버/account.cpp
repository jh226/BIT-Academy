//account.cpp
#include "std.h"

Account* acc_create(const char* name, int balance, int id)
{
	Account* pacc = (Account*)malloc(sizeof(Account));
	strcpy_s(pacc->name, sizeof(pacc->name), name);
	pacc->balance = balance;
	pacc->id = id;

	return pacc;
}

void acc_print(int idx, Account* pacc)
{
	printf("[%2d] %s\t%d\t%d\n", idx, pacc->name, pacc->balance, pacc->id);
}

void acc_addmoney(Account* pacc, int money)
{
	pacc->balance += money;
}

void acc_minmoney(Account* pacc, int money)
{
	pacc->balance -= money;
}