//control.h
#pragma once

#define SERVER_PORT 9000
#define RECV_BUFF	1024

void con_init();
void con_exit();

//net에서 수신된 데이터 처리
void con_RecvData(HANDLE sock, char* msg, int size);

//기능
void MakeAccount(HANDLE sock, pack_MAKEACCOUNT* msg);
void DeleteAccount(HANDLE sock, pack_DELETEACCOUNT* msg);
void InputMoney(HANDLE sock, pack_INPUTMONEY* msg);
void OutputMoney(HANDLE sock, pack_INPUTMONEY* msg);
void SelectAccount(HANDLE sock, pack_SELECTACCOUNT* msg);
void AllAccount(HANDLE sock, pack_ALLACCOUNT* msg);

bool Id_Check(int id);

void PrintAllAccount();

