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

void Sequential_Backup(void) {

}
void Sequential_Search(void) {

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

}

