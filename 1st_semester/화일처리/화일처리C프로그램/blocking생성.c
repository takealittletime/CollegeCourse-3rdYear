//std.db ���� ���α׷�

#include <stdio.h>
#include <string.h>
#include<stdlib.h>

#define REC_SIZE	sizeof(struct student)	//���ڵ� ũ��= 20
#define BF		2	//���۴� ���ڵ� ��(Blockin Factor)
#define NAME_SIZE 16 

struct student
{
		char name[NAME_SIZE];
		int age;
};


int main()
{
	struct student std;
	struct student *sp = NULL;
	FILE *fp = NULL;
	char buf[4] = {0,};
	char block[REC_SIZE*BF]= {0,};
	int i, flag=0;

	fp = fopen("std.txt", "w");
	printf("Input Name and Age. Press ^Z for Quit\n");

	while(1)
	{
		i=0;
		sp= (struct student *)block;
		while ( (i++) < BF )     //���ۿ� ���� ���ڵ� ����ŭ ä������ ��ũ�� ����Ѵ�
		{
				printf("Name : ");
				if (fgets(std.name, sizeof(std.name), stdin) == NULL)  //�̸��׸� Ű���� �Է�.. �����Ϸ��� ^z
				{
					flag = 1;
					break;
				}
				std.name[strlen(std.name)-1] = 0;
				printf("Age : ");
				scanf("%d", &std.age);
//				fflush(stdin); 
                     /*����Ű ���� Ű���� ���۸� ����� ���� fflush�� ���µ� �̴� �߸�
                     fflush�� �Է¹��۰� ��¹��۸� ����µ� ����. ���� ������ ����*/
				while (getchar() != '\n')        // ���� ���� �ٹٲ� ���ڰ� �ƴϸ�
					continue;
				*sp = std;
				sp++;
		}

		if(flag == 1)    // ^Z�� ���� �ߴ��Ͽ��� ���
		{
				if(*block == NULL)
					break;
				fwrite(block, REC_SIZE, (i-1), fp);
				printf("�ܿ����ڵ带 ó���Ͽ����ϴ�\n");
				break;
		}
		fwrite(block, REC_SIZE, BF, fp);  //���۰� �������� ���������� �ܺ�ȭ�Ͽ� ���
		printf("���۸� ��ũ�� ����Ͽ����ϴ�\n");
	}
	fclose(fp);
}