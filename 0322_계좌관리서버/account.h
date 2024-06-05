//account.h
#pragma once

struct Account
{
	char name[20];
	int  balance;
	int  id;
};

Account* acc_create(const char* name, int balance, int id);

void acc_print(int idx, Account* pacc);
void acc_addmoney(Account* pacc, int money);
void acc_minmoney(Account* pacc, int money);

