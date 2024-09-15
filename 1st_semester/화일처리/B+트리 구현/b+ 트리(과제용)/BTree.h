#ifndef __BTREE_
#define __BTREE_
#include "BaseHeader.h"
#include "BufferManager.h"
#include "BTreePage.h"
void initBTree(char *name, int pageSize, BOOL newStart);
	/*B+ -Ʈ���� �ʱ�ȭ*/
BOOL insertRecord(BTreeRecord* newRecord);
	/*(key, value)�� ���ڵ带 ����*/
BOOL deleteRecord(Key key);
	/*key�� ���� ���ڵ带 ����*/
BOOL retrieveRecord(Key key, BTreeRecord* record);
	/*key�� ���� ���ڵ带 �ε������� ���� �˻��ϰ�, record�� ����� �����Ѵ�*/
BOOL findRecord(Key key, BTreePagePtr page);
	/*key�� ���� ���ڵ尡 ���Ե� ��带 page�� �����Ѵ�*/
void closeBTree();
	/*B+-Ʈ�� ����� ��ũ�� �����ϰ� ȭ���� �ݴ´� */
void Sequential_Search(void);
	/*B+-Ʈ�� ����� ��ũ�� �����ϰ� ȭ���� �ݴ´� */
void Get_Header(void);
	/*B+-Ʈ�� ����� ��ũ�� �����ϰ� ȭ���� �ݴ´� */
void retrieveAllPages();
/*B+-Ʈ�� ��ü ������ �б� */
void Sequential_Backup();
/*B+-Ʈ�� ��ü �ý�Ʈ ��� */
void push(int pageNo, int index);
	/*���ÿ� pageNo�� index�� push*/
STACK* pop(); /*������ top�� pop*/
STACK* peek(); /*������ top�� ��ȯ��*/
#endif

