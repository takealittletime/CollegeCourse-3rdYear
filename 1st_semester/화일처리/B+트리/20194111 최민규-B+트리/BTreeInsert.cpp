# include "stdio.h"
# include "string.h"
# include "BTree.h"

extern BufferManager *bufferManager;
extern BTreeHeader *bTreeHeader;
Key splitNode(BTreePagePtr page, Key key, int newPage, int index);
 /*������ �ƴ� ��带 ����*/
Key splitLeaf(BTreePagePtr page, BTreeRecord *record, int index);
 /*������带 ����*/
BOOL insertRecord(BTreeRecord *newRecord){
	/*(key, value)�� ���� ���ڵ� �߰� */
	int index = 0, leftPage = 0, rightPage = 0;
	BOOL finished = FALSE, ret;
	STACK * stack;
	Key key = newRecord -> key;
	BTreePagePtr page = (BTreePagePtr)malloc(bufferManager -> pageSize);
	if(findRecord(key, page)) return FALSE;
	while(finished == FALSE){
		stack = pop();
		if(stack == NULL){
			/*stack �� ����ٸ� ���ο� ��Ʈ ���� : Ʈ�� ���� 1 ���� */
			leftPage = bTreeHeader -> rootPage;
			bTreeHeader -> rootPage = newBTreePage();
			initBTreePage(page, bTreeHeader -> rootPage, FALSE);
			CHILD(page, 0) = leftPage;
			index = 0;
		} else {
			/*������ �Ͼ ��带 �о� �´� */
			index = stack -> index;
			if(rightPage != 0){
				readBTreePage(stack -> pageNo, page);
			}
		}
		if(isFull(page)){
			/*��尡 �� á�ٸ� split�ϰ�, �θ� ��忡 ������ Ű�� ��´�*/
			if(ISLEAF(page)){
				key = splitLeaf(page, newRecord, index);
			}else{
				key = splitNode(page, key, rightPage, index);
			}

		rightPage= PAGENO(page);
		} else {
			/* ��忡 ������ �ִٸ� ���ڵ带 �߰� */
			if (ISLEAF(page)) {
				ret= addRecord(page, newRecord, index);
			} else {
				ret= addKey(page, key, rightPage, index);
			}
			finished= TRUE;
		}
	}
	/* ���ڵ尡 ����� �߰��Ǿ�����, ������ ��ũ�� ��� */
	if (ret) writeBTreePage(PAGENO(page), page);
	free(page);
	return ret;
}
Key splitNode(BTreePagePtr page, Key newKey, int rightPage, int index) {
	/* ������ �ƴ� ��带 ���� */
	BTreePagePtr bigPage=
		(BTreePagePtr)malloc(bufferManager->pageSize+sizeof(Key)+sizeof(int));
	int key= 0, midIndex= 0;
	initBTreePage(bigPage, -2, FALSE);
	/* ���� Ű�� ���ο� Ű�� ���� */
	copyNode(page, bigPage, 0, KEYCNT(page));
	addKey(bigPage, newKey, rightPage, index);
	/* ���� ��忡 ���Ե� Ű�� ���� */
	midIndex= KEYCNT(bigPage) / 2;
	key= KEY(bigPage, midIndex);
	/* Overflow�� �Ͼ ��忡 �й� */
	copyNode(bigPage, page, 0, midIndex);
	index= newBTreePage();
	writeBTreePage(PAGENO(page), page);
	/* ���� �Ҵ�� ��忡 �й� */
	initBTreePage(page, index, FALSE);
	copyNode(bigPage, page, midIndex+1, KEYCNT(bigPage)-midIndex-1);
	writeBTreePage(PAGENO(page), page);
	free(bigPage);

	return key;
}
Key splitLeaf(BTreePagePtr page, BTreeRecord * newRecord, int index) {
		/*������带 ����*/
	BTreePagePtr bigPage=
		(BTreePagePtr)malloc(bufferManager->pageSize+sizeof(BTreeRecord));
	int key= 0, midIndex= 0;
		/*���� ���ڵ�� ���ο� ���ڵ带 ����*/
	initBTreePage(bigPage, -2, TRUE);
	copyNode(page, bigPage, 0, KEYCNT(page));
	addRecord(bigPage, newRecord, index);
		/*������忡 ���Ե� Ű�� ����, ceiling(bigNode�� key����/2)+1*/
	midIndex= KEYCNT(bigPage) / 2-1+KEYCNT(bigPage) % 2;
	key= RECORD(bigPage, midIndex).key;
		/*Overflow�� �Ͼ ��忡 �й�*/
	copyNode(bigPage, page, 0, midIndex+1);
	index= NEXT(page);
	NEXT(page)= newBTreePage();
	writeBTreePage(PAGENO(page), page);
		/*���� �Ҵ�� ��忡 �й�*/
	initBTreePage(page, NEXT(page), TRUE);
	NEXT(page)= index;
	copyNode(bigPage, page, midIndex+1, KEYCNT(bigPage)-midIndex-1);
	writeBTreePage(PAGENO(page), page);
	free(bigPage);
	return key;
}
