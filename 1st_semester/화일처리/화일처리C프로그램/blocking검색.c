//std.db 생성 프로그램

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#define REC_SIZE	sizeof(struct student)	//레코드 크기
#define BF			2	//버퍼당 레코드 수
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
		printf("이름을 입력하세요 (중지하려면 ^Z)  : ");
		if((fgets(name, sizeof(name), stdin)) == NULL)
			break;
		name[strlen(name)-1] = NULL;

		for(flag=0; flag !=1; )
		{
			i=0;
			if(fread(block, REC_SIZE, BF, fp) == 0) // 화일의 내용을 버퍼로 가져옴
				break;

			sp= (struct student *)block;
			for(i=0; i<BF; i++)  //버퍼에 들어있는 레코드 수만큼 반복
			{
				if(strcmp(sp->name, name) == 0)
				{
					flag = 1;
					break;
				}
				sp++;	//한 블록 증가
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
