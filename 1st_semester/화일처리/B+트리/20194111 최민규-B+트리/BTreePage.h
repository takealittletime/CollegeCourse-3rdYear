#ifndef __HASH_PAGE_
#define __HASH_PAGE_
#include "BaseHeader.h"
typedef int Key;
typedef char Value[12];
typedef struct _STACK STACK;
typedef char *BTreePagePtr;
typedef struct _BTreeRecord BTreeRecord;
typedef struct _BTreeHeader BTreeHeader;
/*Stack ����ü*/
struct _STACK {
	int pageNo;
	int index;
};
/*B+-Ʈ�� ���ڵ� ����ü*/
struct _BTreeRecord {
	Key key;
	Value value;
};
/*B+-Ʈ�� ��� ����ü*/
struct _BTreeHeader {
	int rootPage;
	int firstSequencePage;
	int order;
	int minKey, minRecord;
	int maxRecord;
	STACK *stack;
	int stackTop, stackPtr;
};
/*
	[Internal Node]
		(int) ������ ��ȣ
		(int) ���� ������ ��ȣ = -1
		(int) ����� Ű�� ����
		(int) 0��° �ڽ������� ��ȣ
		(int) 0��° Ű

		(int) order-1��° �ڽ������� ��ȣ
		(int) order-1��° Ű
		(int) order ��° �ڽ������� ��ȣ

	[Leaf Node]
		(int) ������ ��ȣ
		(int) ���� ������ ��ȣ >= 0
		(int) ����� ���ڵ��� ����
		(BTreeRecord) 0��° ���ڵ�
		(BTreeRecord) 1��° ���ڵ�

		(BTreeRecord) maxRecord-1��° ���ڵ�
*/

#define PAGENO(p) (*(int *) (p)) /* �������� ��ȣ */
#define NEXT(p) (*(int *) (p+sizeof(int))) /* ���� ������ ��ȣ */
#define KEYCNT(p) (*(int *) (p+sizeof(int) *2)) /* ����� Ű�� ����=����� ���ڵ��� ���� */
#define CHILD(p, i) (*(CHILDPTR(p, i))) /* i��° �ڽ��� ������ ��ȣ */
#define KEY(p, i) (*(KEYPTR(p, i)))  /* i��° Ű */
#define RECORD(p, i) (*(RECORDPTR(p)+(i))) /* i��° ���ڵ� */
#define ISLEAF(p) ((NEXT(p) != -1) ? TRUE : FALSE) /* ���������� �Ǻ� */
#define MIN(p) (ISLEAF(p) ? bTreeHeader->minRecord : bTreeHeader->minKey) /* �ּ� Ű(���ڵ�)�� ���� ����*/
#define CHILDPTR(p, i) ((int *) (p+sizeof(int) *3) +(i) *2) 
#define KEYPTR(p, i) ((int *) (p+sizeof(int) *4) +(i) *2)
#define RECORDPTR(p) ((BTreeRecord *) (p+sizeof(int) *3))

void initBTreeHeader(int root, int firstSequence); 
	/* B+ Ʈ�� Header�� �ʱ�ȭ */
void initBTreePage(BTreePagePtr page, int pageNo, BOOL leaf);  
	/* B+ Ʈ�� Page�� �ʱ�ȭ*/

BOOL readBTreePage(int pageNo, BTreePagePtr page);
	/*(pageNo)��° �������� page�� �о����.
	���� : �������� 1������ ���� (0���� ��� ������ */
BOOL writeBTreePage(int pageNo,BTreePagePtr page);
	/*page�� ������ (pageNo)��° �������� ��*/
int newBTreePage();
	/* ����ִ� ���ο� �������� ��ȣ�� ��ȯ�� */
void freeBTreePage(BTreePagePtr page);
	/* B+Ʈ�� �������� ��ȯ�� */
BOOL addKey(BTreePagePtr page, Key key, int rightPage, int index);
	/* BTreePage�� index��° �ڸ��� (key, child)�� �߰� */
BOOL addRecord(BTreePagePtr page, BTreeRecord *record, int index);
	/* BTreePage�� index��° �ڸ��� (key, value)�� ���� ���ڵ� �߰� */
BOOL removeRecord(BTreePagePtr page, int index);
	/* index��° ���ڵ带 ���� */
BOOL removeKey(BTreePagePtr page, int index);
	/* index��°�� Ű�� ������ �ڽ� ������ ��ȣ�� ���� */
BOOL isFull(BTreePagePtr targetPage);
	/* targetPage�� �� á���� �˻� */
void copyNode(BTreePagePtr sourcePage, BTreePagePtr targetPage, int from, int count);
	/* node������ ���� */
void copyRecord(BTreeRecord *sourceRecord, BTreeRecord *targetRecord);
	/* sourceRecord�� targetRecord�� ���� */
void copyKey(Key *sourceKey, Key *targetKey);
	/* sourceKey�� Ű�� ������ �ڽ� ������ ��ȣ�� targetKey�� ���� */
void loadBTreeHeaderPage();
	/* ��� �������� �ε� */
void saveBTreeHeaderPage();
	/* ��� �������� ���� */
#endif
