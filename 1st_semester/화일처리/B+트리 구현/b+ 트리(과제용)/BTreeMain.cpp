// BTreeMain.c
/* ��� ����
i : (Ű,��)�� ������ ���ڵ� ����
d : Ű�� ������ ���ڵ� ����
r : Ű�� ������ ���ڵ� �˻�
s : ����Ž��
? : �����������
a : ��� ������ ����(1����������)
*/
#include "BTree.h"
#define PAGE_SIZE 64
int main() {
	FILE *fp, *fp_backup;
	BTreeRecord record;
	char command;
	BOOL file_not_Exist;
	fp = fopen("data.txt", "r");   //Ʈ������ ȭ�� �����ϴ��� üũ
	if (fp == NULL) {
		file_not_Exist = TRUE;     //������ ��
		fp = fopen("data.txt", "w"); //ȭ�� ���� ����
	}
	else
		file_not_Exist =FALSE;
	fclose(fp);
	 	initBTree("data.txt", PAGE_SIZE, file_not_Exist); //���� ��������� �ʱ�ȭ. ����������� ��Ʈ������ ����

	while (1) {
		fscanf(stdin, "%c", &command);
		if (command == 'x' || command == 'X') break;
		switch (command) {
		case 'i':    //(Ű,��)�� ������ ���ڵ� ����
			fscanf(stdin, "%i %s", &record.key, &record.value);
			if (insertRecord(&record)) {
				printf("insert (%d, %s) : success\n", record.key, record.value);
			}
			else {
				printf("Insert (%d, %s) : fail\n", record.key, record.value);
			}
			break;
		case 'd' :     //Ű�� ������ ���ڵ� ����
			fscanf(stdin, "%i", &record.key);
			if (deleteRecord(record.key)) {
				printf("Delete (%d) : success\n", record.key, record.value);
			} else {
				printf("Delete (%d) : fail\n", record.key);
			}
			break;
		case 'r':    //Ű�� ������ ���ڵ� �˻�
			fscanf(stdin, "%i", &record.key);
			if (retrieveRecord(record.key, & record)) {
				printf(
					"Retrive (%d, %s) : success\n",
					record.key,
					record.value);
			} else {
				printf("Retrive (%d) : fail\n", record.key);
			}
			break;
		case 'a':    //��������� �˻�
			retrieveAllPages();
			break;
		case 'h':    //������� ����
			Get_Header();
			break;
		case 's':    //�����˻�
			break;

		case 'o':    //���ȭ�� ��������

			break;
		case 'b':    //�����ý�Ʈȭ��(backup.txt)�� ����ϱ�

			break;
		case 'n':    //������ȭ�� �ʱ�ȭ

			break;

		}
	}
	closeBTree();
	return 0;
}