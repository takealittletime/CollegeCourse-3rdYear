// BTreeMain.c
/* ��� ����
i : (Ű,��)�� ������ ���ڵ� ����
d : Ű�� ������ ���ڵ� ����
r : Ű�� ������ ���ڵ� �˻�
*/
#include "BTree.h"
int main(int argc, char **argv) {
	FILE *fp;
	BTreeRecord record;
	char command;
	
	if((argc < 2) || !(fp = fopen(argv[1], "r"))) {
		printf("FILE open error\n");
		exit(1);
	}
	initBTree("name.txt", 4096, TRUE);//swap -> name.txt
	while(fscanf(fp, "%c %i", &command, &record.key) != EOF) {
		switch(command) {
		case 'i' : 
			fscanf(fp, "%s", &record.value);
			if(insertRecord(&record)){
				printf("insert (%d, %s) : success\n", record.key, record.value);
			} else {
				printf("Insert (%d, %s) : fail\n", record.key, record.value);
			}
			break;
		case 'd' :
			if (deleteRecord(record.key)) {
				printf("Delete (%d) : success\n", record.key, record.value);
			} else {
				printf("Delete (%d) : fail\n", record.key);
			}
			break;
		case 'r':
			if (retrieveRecord(record.key, & record)) {
				printf(
					"Retrive (%d, %s) : success\n",
					record.key,
					record.value);
			} else {
				printf("Retrive (%d) : fail\n", record.key);
			}
			break;
		}
	}
	closeBTree();
	return 0;
}