//std.db 생성 프로그램

#include <stdio.h>
#include <string.h>
#include<stdlib.h>

#define REC_SIZE	sizeof(struct student)	//레코드 크기= 20
#define BF		2	//버퍼당 레코드 수(Blockin Factor)
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
		while ( (i++) < BF )     //버퍼에 들어가는 레코드 수만큼 채워지면 디스크에 기록한다
		{
				printf("Name : ");
				if (fgets(std.name, sizeof(std.name), stdin) == NULL)  //이름항목 키보드 입력.. 중지하려면 ^z
				{
					flag = 1;
					break;
				}
				std.name[strlen(std.name)-1] = 0;
				printf("Age : ");
				scanf("%d", &std.age);
//				fflush(stdin); 
                     /*엔터키 값이 키보드 버퍼를 지우기 위해 fflush를 쓰는데 이는 잘못
                     fflush는 입력버퍼가 출력버퍼를 지우는데 쓰임. 따라서 다음과 같이*/
				while (getchar() != '\n')        // 읽은 값이 줄바꿈 문자가 아니면
					continue;
				*sp = std;
				sp++;
		}

		if(flag == 1)    // ^Z를 눌러 중단하였을 경우
		{
				if(*block == NULL)
					break;
				fwrite(block, REC_SIZE, (i-1), fp);
				printf("잔여레코드를 처리하였습니다\n");
				break;
		}
		fwrite(block, REC_SIZE, BF, fp);  //버퍼가 가득차면 물리적으로 외부화일에 출력
		printf("버퍼를 디스크에 기록하였습니다\n");
	}
	fclose(fp);
}