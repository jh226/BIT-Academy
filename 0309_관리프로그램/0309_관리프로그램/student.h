//student.h
#pragma once

struct STUDENT
{
	TCHAR name[20];	//�̸�
	int  grade;		//�г�(1~4)
	int  kor;		//��������
	int  eng;		//��������
	int  mat;		//��������
	float average;	//���
};

STUDENT* student_create(STUDENT stu);
/*
void student_print(const student* pstu);
void student_println(const student* pstu);
void student_setaverage(student* pstu);
void student_jumsuupdate(student* pstu, int kor, int eng, int mat);
void student_init(student* pstu);
*/