 #include "BTree.h"
 extern BufferManager *bufferManager;
 extern BTreeHeader *bTreeHeader;
 
 void initBTree(char *name, int pageSize, BOOL newStart) {
		/*B+-Ʈ���� �ʱ�ȭ*/
	if (newStart) {
		int rootPage;
		//bufferManager = (BufferManager*)malloc(sizeof(BufferManager));
		BTreePagePtr page= (char*)malloc(pageSize);///////////////////////////////////////
		initBufferManager(name, pageSize);
		rootPage= newBTreePage();
		initBTreePage(page, rootPage, TRUE);
		writeBTreePage(rootPage, page);
		initBTreeHeader(rootPage, rootPage);
		free(page);
	} else {
		initBufferManager(name, pageSize);
		loadBTreeHeaderPage();
	}
}
void closeBTree() {
		/*b+-Ʈ�� ����� ��ũ�� �����ϰ� ȭ���� �ݴ´� */
	saveBTreeHeaderPage();
	fclose(bufferManager->fp);
	free(bTreeHeader->stack);
	free(bTreeHeader);
	free(bufferManager);
}
void push(int pageNo, int index) {
		/*���ÿ� pageNo�� index�� push*/
	if (bTreeHeader->stackPtr < bTreeHeader->stackTop+1) {
		bTreeHeader->stack[bTreeHeader->stackPtr].pageNo= pageNo;
		bTreeHeader->stack[bTreeHeader->stackPtr].index= index;
		bTreeHeader->stackPtr++;
	} else {
		/*2�� ũ���� �������� �ٽ� �Ҵ��� �ش�.*/
		bTreeHeader->stack= (STACK *)realloc(bTreeHeader->stack,
			sizeof(STACK) *(bTreeHeader->stackTop+1) *2);
		bTreeHeader->stackTop= (bTreeHeader->stackTop+1) *2-1;

		/*push*/
		bTreeHeader->stack[bTreeHeader->stackPtr].pageNo= pageNo;
		bTreeHeader->stack[bTreeHeader->stackPtr].index= index;
		bTreeHeader->stackPtr++;
	}
}

STACK *pop() {
	/*������ top�� pop*/
	if (bTreeHeader->stackPtr == 0) {
		return NULL;
	} else {
		bTreeHeader->stackPtr--;
		return (bTreeHeader->stack+bTreeHeader->stackPtr);
	}
}
STACK *peek() {
	/*������ top�� ��ȯ��*/
	if (bTreeHeader->stackPtr == 0) return NULL;
	else return (bTreeHeader->stack+bTreeHeader->stackPtr-1);
}
