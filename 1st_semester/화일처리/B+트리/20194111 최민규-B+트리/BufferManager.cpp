#include "BufferManager.h"
BufferManager *bufferManager;
//BufferManager�� �ʱ�ȭ
void initBufferManager(char *name, int size){
	bufferManager = new BufferManager();
	bufferManager->fp=fopen(name, "rb+");
	if(bufferManager->fp==NULL){
		creat(name, 0644);
		initBufferManager(name, 0644);
		bufferManager->fp=fopen(name,"rb+");
	}
	bufferManager ->pageSize= size;
	bufferManager ->maxPageNo=0;
	bufferManager ->lastFreePageNo=-1;
}
BOOL readPage(int pageNo, BYTE *buffer){
	//pageNo��° �������� buffer�� �о����
	int ret;
	fseek(bufferManager->fp, pageNo *bufferManager->pageSize, SEEK_SET);
	ret= (int)fread(buffer, sizeof(BYTE),bufferManager->pageSize,bufferManager->fp);
	rewind(bufferManager->fp);
//	printf("�������۷�) : %d", ret);
	if(ret > 0)
		return TRUE;
	else
		return FALSE;
}
BOOL writePage(int pageNo, BYTE *buffer){
	//buffer�� ������ pageNo��° �������� ��
	int ret;
	fseek(bufferManager->fp, pageNo *bufferManager->pageSize, SEEK_SET);
	ret= (int)fwrite(buffer, sizeof(BYTE), bufferManager->pageSize, bufferManager->fp);
	rewind(bufferManager->fp);
	if(ret>0)
		return TRUE;
	else
		return FALSE;
}
int newPage(){
	//����ִ� ���ο� �������� ��ȣ�� ��ȯ�ϰ�, lastFreePageNo�� ������
	int freePageNo, nextPageNo, offset;
	BYTE *buffer= new BYTE(sizeof(bufferManager->pageSize)); ////////////////////////////////////
	//BYTE *buffer= (BYTE*)malloc(sizeof(bufferManager->pageSize));
	//���� ������ ����Ʈ�� �������� ������ �� �������� �Ҵ��ؾ� ��
	if(bufferManager->lastFreePageNo == -1){
		//���� ���� ������ �������� ���� ��ȣ�� �Ҵ�
		freePageNo = ++(bufferManager->maxPageNo);
	}
	else{//���� ������ ����Ʈ�� �������� ������ �� �������� �̿�
		//���� ������ ����Ʈ�� ù ��° ������ ��ȣ�� ����
		freePageNo= bufferManager->lastFreePageNo;
		//�� �������� nextPageNo���� lastFreePageNo������ ����
		offset=freePageNo *bufferManager->pageSize;
		fseek(bufferManager->fp, offset, SEEK_SET);
		fread(& nextPageNo, sizeof(int), 1, bufferManager->fp);
		if(nextPageNo==-1)
			bufferManager->lastFreePageNo=-1;
		else
			bufferManager->lastFreePageNo=nextPageNo+1;
	}
	free(buffer);
	return freePageNo;
}
void freePage(int pageNo){
	//�������� �ʱ�ȭ�� ����, ���� ������ ����Ʈ�� �߰�
	int offset;
	BYTE *buffer= (BYTE*)malloc(bufferManager->pageSize);	
	//BYTE *buffer= new BYTE(bufferManager->pageSize);
	//BYTE *buffer= malloc(bufferManager->pageSize);////////////////////////////////////
	//pageNo��° �������� ���� ������ ������ ����lastFreePageNo�� ���� ��
	offset= pageNo *bufferManager->pageSize;
	fseek(bufferManager->fp, offset, SEEK_SET);
	fwrite(&(bufferManager->lastFreePageNo), sizeof(int), 1, bufferManager->fp);
	//lastFreepageNo�� pageNo ������ ����
	bufferManager->lastFreePageNo = pageNo;
	free(buffer);
}
void loadHeaderPage(BYTE *header){
	//0�� ��ũ ����������header�� �о��
	readPage(0, header);
}
void saveHeaderPage(BYTE *header){
	//0�� ��ũ �������� header�� ����
	writePage(0, header);
}

void creat(char *name,int n){
   name = (char*)malloc(sizeof(char)*n); 
}