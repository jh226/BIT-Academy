//account.h
#pragma once

struct Account
{
	char name[20];
	int  balance;
	int  id;
};

void acc_print(int idx, Account* pacc);

