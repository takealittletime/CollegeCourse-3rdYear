# include "BTreePage.h"
# include "BufferManager.h"
# include "BTree.h"
extern BufferManager	* bufferManager;
BTreeHeader *bTreeHeader;
void initBTreeHeader(int root, int firstSequence) {
	/*BTree ����� �ʱ�ȭ*/
	bTreeHeader= (BTreeHeader *)malloc(sizeof(BTreeHeader));
	bTreeHeader->rootPage= root;
	bTreeHeader->firstSequencePage= firstSequence;
	bTreeHeader->order=
		(int) ((bufferManager->pageSize-sizeof(int)*4)/(sizeof(int) *2))+1;
	bTreeHeader->minKey=
		bTreeHeader->order / 2-1+bTreeHeader->order % 2;
	bTreeHeader->maxRecord=
		(bufferManager->pageSize-sizeof(int) *3) / (sizeof(BTreeRecord));
	bTreeHeader->minRecord=bTreeHeader->maxRecord /2;
	bTreeHeader->stack= (STACK *)malloc(sizeof(STACK) *5);
	bTreeHeader->stackTop= 4;
	bTreeHeader->stackPtr= 0;
	

}
	void initBTreePage(BTreePagePtr page, int pageNo, BOOL leaf){
		/*BTreePage�� �ʱ�ȭ */
		PAGENO(page)= pageNo;
		NEXT(page)= leaf ? 0 : -1;    //������忡�� ���������� �ε����� ����. �ʱ�ȭ �ÿ��� 0���� �ο�
		KEYCNT(page)=0;
	}
	BOOL readBTreePage(int pageNo, BTreePagePtr page){
		/*pageNo��° �������� page�� �о� ����.
		  ���� : �������� 1������ ����(0���� ��� ������)*/
		BOOL ret;
		ret= readPage(pageNo, page);
		return ret;
	}
	BOOL writeBTreePage(int pageNo, BTreePagePtr page){
		/*page�� ������ (pageNo)��° �������� ��
		  ���� : �������� 1������ ����( 0���� ���������) */
		BOOL ret;
		ret=writePage(pageNo,page);
		return ret;
	}
	int newBTreePage(){
		/* ����ִ� ���ο� B+ - Tree �������� ��ȣ�� ��ȯ�� */
		return newPage();
	}
	void freeBTreePage(BTreePagePtr page){
		/*������ ��ȣ�� victimHashPageNo�� ���� ���� ������ ����Ʈ�� �߰�*/
		if(PAGENO(page)==bTreeHeader->firstSequencePage){
			bTreeHeader->firstSequencePage=NEXT(page);
		}
		freePage(PAGENO(page));
	}

BOOL addKey(BTreePagePtr page, Key key, int rightPage, int index) {
	int j = 0;
	for(j = KEYCNT(page); j>index; j--) {
		KEY(page, j) = KEY(page, j-1);
		CHILD(page, j+1) = CHILD(page, j);
	}
	KEY(page, index) = key;
	CHILD(page, index+1) = rightPage;
	KEYCNT(page)++;
	return TRUE;
}
BOOL addRecord(BTreePagePtr page, BTreeRecord *newRecord, int index) {
	int j = 0;
	for(j = KEYCNT(page); j > index; j--)
		copyRecord(RECORDPTR(page)+j-1, RECORDPTR(page)+j);
	copyRecord(newRecord, RECORDPTR(page)+index);
	KEYCNT(page)++;
	return TRUE;
}
BOOL removeRecord(BTreePagePtr page, int index) {
	int j = 0;
	for(j = index; j < KEYCNT(page)-1; j++)
		copyRecord(RECORDPTR(page)+j+1, RECORDPTR(page)+j);
	KEYCNT(page)--;
	return TRUE;
}
BOOL removeKey(BTreePagePtr page, int index) {
	int j = 0;
	for(j=index; j < KEYCNT(page)-1; j++) {
		KEY(page, j) = KEY(page, j+1);
		CHILD(page, j+1) = CHILD(page, j+2);
	}
	KEYCNT(page)--;

 return TRUE;
}
BOOL isFull(BTreePagePtr targetPage){
 /*�������� ���� á���� �˻�*/
 if(ISLEAF(targetPage)){
  if(KEYCNT(targetPage)==(bTreeHeader->maxRecord)) return TRUE;
  else return FALSE;
 }else{
   if (KEYCNT(targetPage)==(bTreeHeader->order-1)) return TRUE;
   else return FALSE;
  }
}
void copyNode(BTreePagePtr sourcePage, BTreePagePtr targetPage, int from, int count){
/*sourcePage�� from������ ������ targetPage�� ����, ���� targetPage�� ������ ��� ��������.*/
 int i=0;
 KEYCNT(targetPage)=0;
 if(ISLEAF(sourcePage)){
  for(i=0; i<count; i++){
   copyRecord(RECORDPTR(sourcePage)+(from+i),RECORDPTR(targetPage)+i);
   KEYCNT(targetPage)++;
  }
 }else{
  for(i=0; i<count; i++){
   CHILD(targetPage, i)=CHILD(sourcePage, from+i);
   KEY(targetPage, i)=KEY(sourcePage, from+i);
   KEYCNT(targetPage)++;
  }
  CHILD(targetPage, i)=CHILD(sourcePage, from+i);
 }
}
void copyRecord(BTreeRecord *sourceRecord, BTreeRecord *targetRecord){
 /*source���� target���� ���ڵ带 ����*/
 int i=0;
 targetRecord->key=sourceRecord->key;

	for (i= 0; i < sizeof(sourceRecord->value); i++)
		targetRecord->value[i]= sourceRecord->value[i];
}
void copyKey(Key *sourceKey, Key *targetKey) {
	/*source���� target���� Ű�� Ű�� ������ �ڽ� ��� ��ȣ�� ����*/
	int i= 0;
	*targetKey= *sourceKey;
	*(targetKey+1)= *(sourceKey+1);
}
void loadBTreeHeaderPage() {
	  /* ��� �������� ������ ��ũ���� �о���� */
	BYTE *header= (BYTE *)malloc(bufferManager->pageSize);
	loadHeaderPage(header);
	bufferManager->pageSize= *((int *)header);
	bufferManager->maxPageNo= *((int *)header+1);
	bufferManager->lastFreePageNo= *((int *)header+2);
	initBTreeHeader(* ((int *)header+3), *((int *)header+4));
	free(header);
}

void saveBTreeHeaderPage() {
	 /* ��� �������� ������ ��ũ�� ���� */
	BYTE *header= (BYTE *)malloc(bufferManager->pageSize);
	*((int *)header)    = bufferManager->pageSize;
	*((int *)header+1) = bufferManager->maxPageNo;
	*((int *)header+2) = bufferManager->lastFreePageNo;
	*((int *)header+3) = bTreeHeader->rootPage;
	*((int *)header+4) = bTreeHeader->firstSequencePage;
	saveHeaderPage(header);
	free(header);
}
