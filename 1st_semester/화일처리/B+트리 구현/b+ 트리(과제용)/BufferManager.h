#ifndef _BUFFER_MANAGER_
#define _BUFFER_MANAGER_
#include "BaseHeader.h"
typedef struct _BufferManager BufferManager;
struct _BufferManager{
	FILE* fp;
	int pageSize;
	int maxPageNo;
	int lastFreePageNo;
};
void initBufferManager(char *FILEName,int pageSize);
//BufferManager�� �ʱ�ȭ
BOOL readPage(int pageNo, BYTE *buffer);
//pageNo��° �������� buffer�� �о� ����
BOOL writePage(int pageNo, BYTE *buffer);
//buffer�� ������ pageNo��° �������� ��
int newPage();
//����ִ� ���ο� �������� ��ȣ�� ��ȯ��
void freePage(int pageNo);
//�������� �ʱ�ȭ�� ����, ���� ������ ����Ʈ�� �߰�
int newHashPage();
//����ִ� ���ο� �������� ��ȣ�� ��ȯ�ϰ�, lastFreePageNo�� ������
void freeHashPage(int pageNo);
//�ؽ� �������� ��ȯ��
void saveHeaderPage(BYTE*);
//bufferManager�� ������ ��� �������� ��(����)
void loadHeaderPage(BYTE*);
//��� �������� ������ bufferManager�� �о� ����(����)
void creat(char*, int);
//�߰�
#endif
