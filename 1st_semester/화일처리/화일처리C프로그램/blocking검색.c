//std.db ���� ���α׷�

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#define REC_SIZE	sizeof(struct student)	//���ڵ� ũ��
#define BF			2	//���۴� ���ڵ� ��
#define NAME_SIZE 16 

struct student
{
		char name[NAME_SIZE];
		int age;
};


main()
{
	struct student std;
	struct student *sp = NULL;
	FILE *fp = NULL;
	char name[16] = {0,};
	char block[REC_SIZE*BF]= {0,};
	int i, flag;

	fp = fopen("std.txt", "r");

	while(1)
	{
		printf("�̸��� �Է��ϼ��� (�����Ϸ��� ^Z)  : ");
		if((fgets(name, sizeof(name), stdin)) == NULL)
			break;
		name[strlen(name)-1] = NULL;

		for(flag=0; flag !=1; )
		{
			i=0;
			if(fread(block, REC_SIZE, BF, fp) == 0) // ȭ���� ������ ���۷� ������
				break;

			sp= (struct student *)block;
			for(i=0; i<BF; i++)  //���ۿ� ����ִ� ���ڵ� ����ŭ �ݺ�
			{
				if(strcmp(sp->name, name) == 0)
				{
					flag = 1;
					break;
				}
				sp++;	//�� ��� ����
			}
		}

		if(flag)
			printf("Found  Name = %s,  Age = %d\n", sp->name, sp->age);
		else
			printf("%s is not found \n", name);
		fseek(fp, 0,0);
	}
	fclose(fp);
}
