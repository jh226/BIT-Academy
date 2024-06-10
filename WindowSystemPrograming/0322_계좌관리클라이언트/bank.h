//bank.h
#pragma once

void bank_makeaccount();
void bank_makeaccount_ack(pack_MAKEACCOUNT* msg);

void bank_deleteaccount();
void bank_deleteaccount_ack(pack_DELETEACCOUNT* msg);

void bank_inputmoney();
void bank_inputmoney_ack(pack_INPUTMONEY* msg);

void bank_outputmoney();
void bank_outputmoney_ack(pack_OUTPUTMONEY* msg);

void bank_selectaccount();
void bank_selectaccount_ack(pack_SELECTACCOUNT* msg);

void bank_allaccount();
void bank_allaccount_ack(pack_ALLACCOUNT* msg);