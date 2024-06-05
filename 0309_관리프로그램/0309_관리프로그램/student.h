//student.h
#pragma once

struct STUDENT
{
	TCHAR name[20];	//이름
	int  grade;		//학년(1~4)
	int  kor;		//국어점수
	int  eng;		//영어점수
	int  mat;		//수학점수
	float average;	//평균
};

STUDENT* student_create(STUDENT stu);
/*
void student_print(const student* pstu);
void student_println(const student* pstu);
void student_setaverage(student* pstu);
void student_jumsuupdate(student* pstu, int kor, int eng, int mat);
void student_init(student* pstu);
*/