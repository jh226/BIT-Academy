//student.cpp

#include "std.h"

STUDENT* student_create(STUDENT stu)
{
	//힙 메모리에 STUDENT를 저장할 수 있는 공간 생성
	STUDENT* pstu = (STUDENT*)malloc(sizeof(STUDENT));

	_tcscpy_s(pstu->name, _countof(pstu->name), stu.name);
	pstu->grade = stu.grade;
	pstu->kor = stu.kor;
	pstu->eng = stu.eng;
	pstu->mat = stu.mat;
	pstu->average = (stu.kor + stu.eng + stu.mat) / 3.0f;

	return pstu;
}
/*
void student_print(const student* pstu)
{
	//printf("%s\t %d\t %d\t %d\t %d\t %.1f\n",
	//	pstu->name, pstu->grade, pstu->kor, pstu->eng, pstu->mat, pstu->average);
}

void student_println(const student* pstu)
{
	//printf("[이 름] %s\n", pstu->name);
	//printf("[학 년] %d학년\n", pstu->grade);
	//printf("[국 어] %d점\n", pstu->kor);
	//printf("[영 어] %d점\n", pstu->eng);
	//printf("[수 어] %d점\n", pstu->mat);
	//printf("[평 균] %.1f\n", pstu->average);
}

void student_setaverage(student* pstu)
{
	int sum = pstu->kor + pstu->eng + pstu->mat;
	pstu->average = sum / 3.0f;
}

void student_jumsuupdate(student* pstu, int kor, int eng, int mat)
{
	pstu->kor = kor;
	pstu->eng = eng;
	pstu->mat = pstu->mat;

	student_setaverage(pstu);
}

void student_init(student* pstu)
{
	pstu->flag = 0;	 //<--------------------
	strcpy_s(pstu->name, sizeof(pstu->name), "");
	pstu->grade = 0;
	pstu->kor = 0;
	pstu->eng = 0;
	pstu->mat = 0;
	pstu->average = 0.0f;
}
*/