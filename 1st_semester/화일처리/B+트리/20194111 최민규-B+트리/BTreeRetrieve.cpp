#include "BTree.h"
extern BufferManager *bufferManager;
extern BTreeHeader *bTreeHeader;
BOOL retrieveRecord(Key key, BTreeRecord *record) {
	/*�ε��� ������ ���� ���ڵ� �˻�*/
	BTreePagePtr page=(BTreePagePtr)malloc(bufferManager->pageSize);
	int i=0;
	BOOL found=FALSE;
	if(findRecord(key, page)) {
		i=peek()->index;
		copyRecord(RECORDPTR(page)+i, record);
		found=TRUE;
	}
	free(page);
	return found;
}
BOOL findRecord(Key key, BTreePagePtr page) {
	/*Key�� ���� Record�� ã�ư��� ��θ� ���ÿ� push�ϰ�
			������ Leaf�� page�� �����Ѵ�*/

	int i=0, targetPage=bTreeHeader->rootPage;
	bTreeHeader->stackPtr=0;
	readBTreePage(targetPage, page);
	while(ISLEAF(page) == FALSE) {
		for (i = 0; (i < KEYCNT(page)) && (KEY(page, i) < key); i++) {
			;
		}
		push(PAGENO(page), i);
		targetPage=CHILD(page, i);
		readBTreePage(targetPage, page);
	}
	for (i = 0; (i < KEYCNT(page)) && (RECORD(page, i).key < key); i++) {
		;
		}
	

	push(PAGENO(page), i);

	if((i < KEYCNT(page)) && (key == RECORD(page, i).key)) {
		return TRUE;
	}
	else {
		return FALSE;
	}
}
#define SIZE 100
void Sequential_Backup(void) { //����ϱ�

	int buffer[SIZE];
	FILE *fp = NULL;
	FILE* fp1 = NULL;
	int i;
	int count;
	char file1[100];
	printf("������ ���� �̸� �Է�: ");
	scanf("%s", file1);
	char file2[100];
	printf("��� ���� �̸� �Է�: ");
	scanf("%s", file2);
	printf("\n================================================================\n");
	fp = fopen(file1, "rb");
	if( fp == NULL ) {
		fprintf(stderr, "������ �� �� �����ϴ�.");
	}
	fp1 = fopen(file2, "wb");

	if( fp1 == NULL ) {
		fprintf(stderr, "������ �� �� �����ϴ�.");
	}
	while( (count=fread(buffer, sizeof(char), SIZE, fp)) != 0 ) {
		fwrite(buffer, sizeof(char), count, fp1);
	}
	fclose(fp);
	fclose(fp1);
	printf("%s���� -> %s���Ϸ� ��� �Ϸ�.\n",file1,file2);

}
#define PAGE_SIZE 64
void Sequential_Read(void){ //��� ���� �о����

	BOOL file_not_Exist = TRUE;
	initBTree("data.txt", PAGE_SIZE, file_not_Exist);


	int buffer[SIZE];
	FILE* fp = NULL;
	FILE* fp1 = NULL;
	int i;
	int count;
	char file1[100], file2[100];
	printf("================================================================\n");
	printf("��� ���� �̸� �Է�: ");
	scanf("%s", file1);
	printf("������ ���� �̸� �Է�: ");
	scanf("%s", file2);
	printf("\n================================================================\n");
	fp = fopen(file1, "rb");
	if (fp == NULL) {
		fprintf(stderr, "������ �� �� �����ϴ�");
	}
	fp1 = fopen(file2, "wb");

	if (fp1 == NULL) {
		fprintf(stderr, "������ �� �� �����ϴ�.");
	}
	while ((count = fread(buffer, sizeof(char), SIZE, fp)) != 0) {
		fwrite(buffer, sizeof(char), count, fp1);
	}
	fclose(fp);
	fclose(fp1);
	printf("�ҷ����Ⱑ �Ϸ�Ǿ����ϴ�.\n");
}

void DataFileInit(void){ //�ʱ�ȭ
	BOOL file_not_Exist = TRUE;
	FILE* fp, * fp_backup;
	fp = fopen("data.txt", "w");
	fclose(fp);
	initBTree("data.txt", PAGE_SIZE, file_not_Exist); 
	printf("data ������ �ʱ�ȭ�Ǿ����ϴ�.\n");
}

void Sequential_Search(void) { //���� �˻�
	int choice;
	int page_search;
	int key_search;
	int i = 1;
	BTreePagePtr page = (BTreePagePtr)malloc(bufferManager->pageSize);

	printf("�����˻�\n");
	printf("1: ���� ���� �˻� / 2: Key �˻�: ");
	scanf("%d",&choice);
	switch (choice) {
	case 1:
		while (readBTreePage(i, page)) {
			if (ISLEAF(page) == FALSE) {
				for (int j = 0; (j < KEYCNT(page)); j++) {
				}
			}
			else {
				printf("��������ȣ : %d \n", PAGENO(page));
				printf("���ڵ尳�� : %d\n", KEYCNT(page));
				for (int j = 0; (j < KEYCNT(page)); j++) {
					printf("%d %s\n", RECORD(page, j).key, RECORD(page, j).value);
				}
				printf("���������� : %d\n", NEXT(page));
				printf("--------------------------------------\n");
			}
			i++;

			
		}
		break;
	case 2:
		printf("�˻��� Key�� �Է��ϼ��� : \n");
		scanf("%d", &key_search);
		while (readBTreePage(i, page)) {
			if (ISLEAF(page) == FALSE) {

				for (int j = 0; (j < KEYCNT(page)); j++) {
				}
			}
			else {
				printf("Key : %d \n", key_search);
				("���ڵ尳�� : %d\n", KEYCNT(page));
				for (int j = 0; (j < KEYCNT(page)); j++) {
					if (RECORD(page, j).key == key_search) {
						printf("Key : %d = %s\n", key_search, RECORD(page, j).value);
					}
				}
				printf("--------------------------------------\n");

			}
			i++;
		}
		break;

	}

}
void Get_Header(void) {
	/*������� ��������*/
	printf("��Ʈ��� ��������ȣ   : %d\n", bTreeHeader->rootPage);
	printf("������� ù��������ȣ : %d\n", bTreeHeader->firstSequencePage);
	printf("B+ Ʈ���� ����        : %d\n", bTreeHeader->order); //(int)((pageSize - (����4����Ʈ) * 4) / (����4����Ʈ) * 2)) + 1;
	printf("���γ�� �ּ�Ű ����  : %d\n", bTreeHeader->minKey); //order / 2 - 1 + order % 2;
	printf("������ ���ڵ� �ִ��  : %d\n", bTreeHeader->maxRecord); //pageSize - (����4����Ʈ) * 3) / (����4����Ʈ);
	printf("������� �ּ�Ű ����  : %d\n", bTreeHeader->minRecord); // bTreeHeader->maxRecord / 2

}

void retrieveAllPages() {

	BTreePagePtr page = (BTreePagePtr)malloc(bufferManager->pageSize);
	int i = 1;
	while (readBTreePage(i, page)) {
		if (ISLEAF(page) == FALSE) {
			printf("��������ȣ   : %d (���γ��)\n", PAGENO(page));
			printf("����������   : %d\n", NEXT(page));
			printf("Ű�ǰ���     : %d\n", KEYCNT(page));
			printf("Ű����Ʈ     : ");
			for (int j = 0; (j < KEYCNT(page)); j++) {
				printf("%d, (%d), ", CHILD(page, j), KEY(page, j));
			}
			printf("%d\n", CHILD(page, KEYCNT(page)));
			printf("--------------------------------------\n");

		}
		else {
			printf("��������ȣ : %d (�������)\n", PAGENO(page));
			printf("���ڵ尳�� : %d\n", KEYCNT(page));
			for (int j = 0; (j < KEYCNT(page)); j++) {
				printf("%d %s\n", RECORD(page, j).key, RECORD(page, j).value);
			}
			printf("���������� : %d\n", NEXT(page));
			printf("--------------------------------------\n");
		}
		i++;
	}
}

void Command() { //��ɾ� ����

	printf("B+ Ʈ��\n");
	printf("<��ɾ� ���>\n");
	printf("h : ��� ������ ����\n");
	printf("a : ��� ������ ����\n");
	printf("i : ���ڵ� �߰� ( ���� : i 1 lee)\n");
	printf("r : ���ڵ� �˻� ( ���� : r 1)\n");
	printf("d : ���ڵ� ���� ( ���� : d 1)\n");
	printf("s : �����˻�\n");
	printf("b : �����ؽ�Ʈȭ�Ϸ� ����ϱ�\n");
	printf("n : ������ ȭ�� �ʱ�ȭ\n");
	printf("o : ��� ȭ�� ��������\n");
	printf("e : ���α׷� ����\n");
	printf("--------------------------------------\n\n");

}