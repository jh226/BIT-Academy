//control.h
#pragma once

#define SERVER_IP	"127.0.0.1"  //10.101.15.108
#define SERVER_PORT 9000
#define RECV_BUFF	1024

void con_init();
void con_exit();

//net에서 수신된 데이터 처리
void con_RecvData(char* msg, int size);

