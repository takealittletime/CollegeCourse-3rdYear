//���� Ʈ����� ����ȭ�� ���� ���α׷�
// �ΰ��� ȭ�� ��, oldmaster.txt �� transaction.txt �ΰ��� �о�
//���ŵ� ���ο� ������ȭ�� newmaster.txt �� report.txt �� �����Ѵ�

#include <stdio.h>
#include < string.h >
#define SENTINEL 100   //ȭ���� ���� �˸��� Ư�� Ű��

//��ǰ ���ڵ� ����ü
struct product_rec {
	int id;
	char name[20];
	int inventory;
}; 

//���ŷ��ڵ� ����ü(�����ڵ尡 �ϳ� �߰���)
struct update_rec {
	char update_code;
	int id;
	char name[20];
	int inventory;
};

//�������� ����

FILE *old_master, *transaction, *new_master, *report;
struct product_rec old_rec;   //�������� ���ڵ� ����
struct update_rec trans_rec;  //Ʈ����� ���ڵ� ����
int add_cnt=0, change_cnt=0, delete_cnt=0, error_cnt=0;

//�Լ� ���� 

void Get_Old()  //Old Master ȭ�Ͽ��� �о����
{
	fscanf(old_master, "\n%d %s %d", &old_rec.id, old_rec.name, &old_rec.inventory);
}

void Get_Transaction()  //Transaction ȭ�Ͽ��� �о����
{
	fscanf(transaction, "\n%c %d %s %d", &trans_rec.update_code, &trans_rec.id, trans_rec.name, &trans_rec.inventory);
}

void Put_Old_To_New()  //Old Master ���ڵ带 �״�� New Master �� ���
{
	fprintf(new_master, "%d %s %d\n", old_rec.id, old_rec.name, old_rec.inventory);
}

void Put_Transaction_To_New()  //Old Master ���ڵ带 �״�� New Master �� ���
{
	fprintf(new_master, "%d %s %d\n", trans_rec.id, trans_rec.name, trans_rec.inventory);
}



void Error_Print(char err_code)  //�������� ��� 
{
	switch(err_code)
	{
	case 'A':
			fprintf(report, "%c %d %s %d : �߰�����\n", trans_rec.update_code, trans_rec.id, trans_rec.name, trans_rec.inventory);
			break;
	case 'C':
			break;
			fprintf(report, "%c %d %s %d : �ش� ���ڵ� ����\n", trans_rec.update_code, trans_rec.id, trans_rec.name, trans_rec.inventory);
	case 'E':
			fprintf(report, "%c %d %s %d : �����ڵ����\n", trans_rec.update_code, trans_rec.id, trans_rec.name, trans_rec.inventory);
			break;
	}

}


//�����Լ� ����

int main(void)
{

old_master = fopen("oldmaster.txt", "r");
transaction = fopen("transaction.txt", "r");
new_master = fopen("newmaster.txt", "w");
report = fopen("report.txt", "w");

// ù ���ڵ� �б�
Get_Old();
Get_Transaction();

//��Ƽ���� ���� ������ �ݺ� ����
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
				Error_Print('A');  //�߰� ����
				Put_Old_To_New();
				Get_Old();
				Get_Transaction();
				error_cnt++;
				break;
			case 'C':
				old_rec.inventory += trans_rec.inventory; //�����ͷ��ڵ� ���� ����
				Put_Old_To_New();   //����� ���ڵ带 �ٷ� ���ο� �����Ϳ� ���. ����Ʈ����ǰ� �ٸ���
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
	else		// old_rec.id > trans_rec.id �� ��츦 �ǹ���. ����('A') �ƴϸ� ��� ����ó��;
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
//��Ƽ���� ���� ��� �ܿ����ڵ� ó��
//====================================

if(trans_rec.id == SENTINEL)  // Transaction���� ���� ��Ƽ���� ���� ��� �������� ���� ����
	{
		while(old_rec.id != SENTINEL)
		{
			Put_Old_To_New();
			Get_Old();
		}

	}
else      // Old Master���� ���� ��Ƽ���� ���� ��� Ʈ����� ���ڵ� ó����. ����('A') �ƴϸ� ��� ����ó��;
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

//������
printf("�߰� Ƚ�� : %d\n", add_cnt);
printf("���� Ƚ�� : %d\n", change_cnt);
printf("���� Ƚ�� : %d\n", delete_cnt);
printf("���� Ƚ�� : %d\n", error_cnt);
fprintf(report, "==========\n");
fprintf(report, "���� ��� \n");
fprintf(report, "==========\n");

fprintf(report, "�߰� Ƚ�� : %d\n", add_cnt);
fprintf(report, "���� Ƚ�� : %d\n", change_cnt);
fprintf(report, "���� Ƚ�� : %d\n", delete_cnt);
fprintf(report, "���� Ƚ�� : %d\n", error_cnt);

fclose(old_master);
fclose(transaction);
fclose(new_master);
fclose(report);

// ��� �������


}
