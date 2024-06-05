//ipc.h
#pragma once

//연결
BOOL ipc_Connect(HWND hDlg, TCHAR* name);

//연결 해제
BOOL ipc_Disconnect(HWND hDlg);

//상대방이 나에게 핸들 전달
void ipc_ConnectMessage(HWND hDlg, HWND lParam);

//전송 - char : 시작 주소 / int : 그 주소부터 size 
int ipc_SendData(HWND hDlg, char* msg, int size);

//수신
DATA* ipc_RecvData(HWND hDlg, LPARAM lParam);