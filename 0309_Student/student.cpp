//student.cpp

#include "std.h"

STUDENT* student_create(STUDENT stu)
{
	//�� �޸𸮿� STUDENT�� ������ �� �ִ� ���� ����
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
	//printf("[�� ��] %s\n", pstu->name);
	//printf("[�� ��] %d�г�\n", pstu->grade);
	//printf("[�� ��] %d��\n", pstu->kor);
	//printf("[�� ��] %d��\n", pstu->eng);
	//printf("[�� ��] %d��\n", pstu->mat);
	//printf("[�� ��] %.1f\n", pstu->average);
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