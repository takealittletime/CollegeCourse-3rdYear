//단일 트랜잭션 순차화일 갱신 프로그램
// 두개의 화일 즉, oldmaster.txt 와 transaction.txt 두개를 읽어
//갱신된 새로운 마스터화일 newmaster.txt 와 report.txt 를 생성한다

#include <stdio.h>
#include < string.h >
#define SENTINEL 100   //화일의 끝을 알리는 특수 키값

//물품 레코드 구조체
struct product_rec {
	int id;
	char name[20];
	int inventory;
}; 

//갱신레코드 구조체(갱신코드가 하나 추가됨)
struct update_rec {
	char update_code;
	int id;
	char name[20];
	int inventory;
};

//전역변수 선언

FILE *old_master, *transaction, *new_master, *report;
struct product_rec old_rec;   //구마스터 레코드 선언
struct update_rec trans_rec;  //트랜잭션 레코드 선언
int add_cnt=0, change_cnt=0, delete_cnt=0, error_cnt=0;

//함수 정의 

void Get_Old()  //Old Master 화일에서 읽어오기
{
	fscanf(old_master, "\n%d %s %d", &old_rec.id, old_rec.name, &old_rec.inventory);
}

void Get_Transaction()  //Transaction 화일에서 읽어오기
{
	fscanf(transaction, "\n%c %d %s %d", &trans_rec.update_code, &trans_rec.id, trans_rec.name, &trans_rec.inventory);
}

void Put_Old_To_New()  //Old Master 레코드를 그대로 New Master 에 출력
{
	fprintf(new_master, "%d %s %d\n", old_rec.id, old_rec.name, old_rec.inventory);
}

void Put_Transaction_To_New()  //Old Master 레코드를 그대로 New Master 에 출력
{
	fprintf(new_master, "%d %s %d\n", trans_rec.id, trans_rec.name, trans_rec.inventory);
}



void Error_Print(char err_code)  //오류내용 출력 
{
	switch(err_code)
	{
	case 'A':
			fprintf(report, "%c %d %s %d : 추가오류\n", trans_rec.update_code, trans_rec.id, trans_rec.name, trans_rec.inventory);
			break;
	case 'C':
			break;
			fprintf(report, "%c %d %s %d : 해당 레코드 없음\n", trans_rec.update_code, trans_rec.id, trans_rec.name, trans_rec.inventory);
	case 'E':
			fprintf(report, "%c %d %s %d : 갱신코드오류\n", trans_rec.update_code, trans_rec.id, trans_rec.name, trans_rec.inventory);
			break;
	}

}


//메인함수 시작

int main(void)
{

old_master = fopen("oldmaster.txt", "r");
transaction = fopen("transaction.txt", "r");
new_master = fopen("newmaster.txt", "w");
report = fopen("report.txt", "w");

// 첫 레코드 읽기
Get_Old();
Get_Transaction();

//센티널을 만날 때까지 반복 수행
while(!(old_rec.id==SENTINEL || trans_rec.id==SENTINEL))
{
	if (old_rec.id < trans_rec.id) 
	{
		Put_Old_To_New();
		Get_Old();
	}

	else if (old_rec.id == trans_rec.id) 
	{
		switch(trans_rec.update_code)
		{
			case 'A':
				Error_Print('A');  //추가 오류
				Put_Old_To_New();
				Get_Old();
				Get_Transaction();
				error_cnt++;
				break;
			case 'C':
				old_rec.inventory += trans_rec.inventory; //마스터레코드 변경 해줌
				Put_Old_To_New();   //변경된 레코드를 바로 새로운 마스터에 출력. 다중트랜잭션과 다른점
				Get_Old();
				Get_Transaction();
				change_cnt++;
				break;
			case 'D':
				Get_Old();
				Get_Transaction();
				delete_cnt++;
				break;
			default:
				Error_Print('E');
				error_cnt++;
				break;
		}
	}
	else		// old_rec.id > trans_rec.id 의 경우를 의미함. 삽입('A') 아니면 모두 에러처리;
	{
		switch(trans_rec.update_code)
		{
			case 'A':
				Put_Transaction_To_New();
				Get_Transaction();
				add_cnt++;
				break;
			case 'C':
			case 'D':
				Error_Print('C');
				error_cnt++;
				Get_Transaction();
				break;
			default:
				Error_Print('E');
				error_cnt++;
				Get_Transaction();
				break;
		}
	}
}

//===================================
//센티널을 만난 경우 잔여레코드 처리
//====================================

if(trans_rec.id == SENTINEL)  // Transaction에서 먼저 센티널을 만난 경우 구마스터 내용 복사
	{
		while(old_rec.id != SENTINEL)
		{
			Put_Old_To_New();
			Get_Old();
		}

	}
else      // Old Master에서 먼저 센티널을 만난 경우 트랜잭션 레코드 처리함. 삽입('A') 아니면 모두 에러처리;
	{  
		while(trans_rec.id != SENTINEL)
		{
		  switch(trans_rec.update_code)
		  {
			case 'A':
				Put_Transaction_To_New();
				add_cnt++;
				Get_Transaction();
				break;
			case 'C':
			case 'D':
				Error_Print('C');
				error_cnt++;
				Get_Transaction();
				break;
			default:
				Error_Print('E');
				error_cnt++;
				Get_Transaction();
				break;
		  }
		}
	}

fprintf(new_master, "%d %s %d\n", SENTINEL, "*", 0);

//마무리
printf("추가 횟수 : %d\n", add_cnt);
printf("수정 횟수 : %d\n", change_cnt);
printf("삭제 횟수 : %d\n", delete_cnt);
printf("오류 횟수 : %d\n", error_cnt);
fprintf(report, "==========\n");
fprintf(report, "갱신 요약 \n");
fprintf(report, "==========\n");

fprintf(report, "추가 횟수 : %d\n", add_cnt);
fprintf(report, "수정 횟수 : %d\n", change_cnt);
fprintf(report, "삭제 횟수 : %d\n", delete_cnt);
fprintf(report, "오류 횟수 : %d\n", error_cnt);

fclose(old_master);
fclose(transaction);
fclose(new_master);
fclose(report);

// 요약 내용출력


}
