#include "BTree.h"
extern BufferManager *bufferManager;
extern BTreeHeader *bTreeHeader;
BOOL retrieveRecord(Key key, BTreeRecord *record) {
	/*인덱스 셋으로 부터 레코드 검색*/
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
	/*Key를 가진 Record를 찾아가는 경로를 스택에 push하고
			도달한 Leaf를 page에 저장한다*/

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
void Sequential_Backup(void) { //백업하기

	int buffer[SIZE];
	FILE *fp = NULL;
	FILE* fp1 = NULL;
	int i;
	int count;
	char file1[100];
	printf("데이터 파일 이름 입력: ");
	scanf("%s", file1);
	char file2[100];
	printf("백업 파일 이름 입력: ");
	scanf("%s", file2);
	printf("\n================================================================\n");
	fp = fopen(file1, "rb");
	if( fp == NULL ) {
		fprintf(stderr, "파일을 열 수 없습니다.");
	}
	fp1 = fopen(file2, "wb");

	if( fp1 == NULL ) {
		fprintf(stderr, "파일을 열 수 없습니다.");
	}
	while( (count=fread(buffer, sizeof(char), SIZE, fp)) != 0 ) {
		fwrite(buffer, sizeof(char), count, fp1);
	}
	fclose(fp);
	fclose(fp1);
	printf("%s파일 -> %s파일로 백업 완료.\n",file1,file2);

}
#define PAGE_SIZE 64
void Sequential_Read(void){ //백업 파일 읽어오기

	BOOL file_not_Exist = TRUE;
	initBTree("data.txt", PAGE_SIZE, file_not_Exist);


	int buffer[SIZE];
	FILE* fp = NULL;
	FILE* fp1 = NULL;
	int i;
	int count;
	char file1[100], file2[100];
	printf("================================================================\n");
	printf("백업 파일 이름 입력: ");
	scanf("%s", file1);
	printf("데이터 파일 이름 입력: ");
	scanf("%s", file2);
	printf("\n================================================================\n");
	fp = fopen(file1, "rb");
	if (fp == NULL) {
		fprintf(stderr, "파일을 열 수 없습니다");
	}
	fp1 = fopen(file2, "wb");

	if (fp1 == NULL) {
		fprintf(stderr, "파일을 열 수 없습니다.");
	}
	while ((count = fread(buffer, sizeof(char), SIZE, fp)) != 0) {
		fwrite(buffer, sizeof(char), count, fp1);
	}
	fclose(fp);
	fclose(fp1);
	printf("불러오기가 완료되었습니다.\n");
}

void DataFileInit(void){ //초기화
	BOOL file_not_Exist = TRUE;
	FILE* fp, * fp_backup;
	fp = fopen("data.txt", "w");
	fclose(fp);
	initBTree("data.txt", PAGE_SIZE, file_not_Exist); 
	printf("data 파일이 초기화되었습니다.\n");
}

void Sequential_Search(void) { //순차 검색
	int choice;
	int page_search;
	int key_search;
	int i = 1;
	BTreePagePtr page = (BTreePagePtr)malloc(bufferManager->pageSize);

	printf("순차검색\n");
	printf("1: 순차 접근 검색 / 2: Key 검색: ");
	scanf("%d",&choice);
	switch (choice) {
	case 1:
		while (readBTreePage(i, page)) {
			if (ISLEAF(page) == FALSE) {
				for (int j = 0; (j < KEYCNT(page)); j++) {
				}
			}
			else {
				printf("페이지번호 : %d \n", PAGENO(page));
				printf("레코드개수 : %d\n", KEYCNT(page));
				for (int j = 0; (j < KEYCNT(page)); j++) {
					printf("%d %s\n", RECORD(page, j).key, RECORD(page, j).value);
				}
				printf("다음페이지 : %d\n", NEXT(page));
				printf("--------------------------------------\n");
			}
			i++;

			
		}
		break;
	case 2:
		printf("검색할 Key를 입력하세요 : \n");
		scanf("%d", &key_search);
		while (readBTreePage(i, page)) {
			if (ISLEAF(page) == FALSE) {

				for (int j = 0; (j < KEYCNT(page)); j++) {
				}
			}
			else {
				printf("Key : %d \n", key_search);
				("레코드개수 : %d\n", KEYCNT(page));
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
	/*헤더정보 가져오기*/
	printf("루트노드 페이지번호   : %d\n", bTreeHeader->rootPage);
	printf("리프노드 첫페이지번호 : %d\n", bTreeHeader->firstSequencePage);
	printf("B+ 트리의 차수        : %d\n", bTreeHeader->order); //(int)((pageSize - (정수4바이트) * 4) / (정수4바이트) * 2)) + 1;
	printf("내부노드 최소키 개수  : %d\n", bTreeHeader->minKey); //order / 2 - 1 + order % 2;
	printf("리프의 레코드 최대수  : %d\n", bTreeHeader->maxRecord); //pageSize - (정수4바이트) * 3) / (정수4바이트);
	printf("리프노드 최소키 개수  : %d\n", bTreeHeader->minRecord); // bTreeHeader->maxRecord / 2

}

void retrieveAllPages() {

	BTreePagePtr page = (BTreePagePtr)malloc(bufferManager->pageSize);
	int i = 1;
	while (readBTreePage(i, page)) {
		if (ISLEAF(page) == FALSE) {
			printf("페이지번호   : %d (내부노드)\n", PAGENO(page));
			printf("다음페이지   : %d\n", NEXT(page));
			printf("키의개수     : %d\n", KEYCNT(page));
			printf("키리스트     : ");
			for (int j = 0; (j < KEYCNT(page)); j++) {
				printf("%d, (%d), ", CHILD(page, j), KEY(page, j));
			}
			printf("%d\n", CHILD(page, KEYCNT(page)));
			printf("--------------------------------------\n");

		}
		else {
			printf("페이지번호 : %d (리프노드)\n", PAGENO(page));
			printf("레코드개수 : %d\n", KEYCNT(page));
			for (int j = 0; (j < KEYCNT(page)); j++) {
				printf("%d %s\n", RECORD(page, j).key, RECORD(page, j).value);
			}
			printf("다음페이지 : %d\n", NEXT(page));
			printf("--------------------------------------\n");
		}
		i++;
	}
}

void Command() { //명령어 보기

	printf("B+ 트리\n");
	printf("<명령어 목록>\n");
	printf("h : 헤더 페이지 보기\n");
	printf("a : 모든 페이지 보기\n");
	printf("i : 레코드 추가 ( 형식 : i 1 lee)\n");
	printf("r : 레코드 검색 ( 형식 : r 1)\n");
	printf("d : 레코드 삭제 ( 형식 : d 1)\n");
	printf("s : 순차검색\n");
	printf("b : 순차텍스트화일로 백업하기\n");
	printf("n : 데이터 화일 초기화\n");
	printf("o : 백업 화일 가져오기\n");
	printf("e : 프로그램 종료\n");
	printf("--------------------------------------\n\n");

}