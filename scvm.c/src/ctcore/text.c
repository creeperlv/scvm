#include "text.h"

char NextSegment(char *input, int startIndex, int MaxLen, char **startPtr, int *strLen)
{
	int offset = startIndex;
	for (int i = 0; i < MaxLen; i++)
	{
		char c = input[i + startIndex];
		switch (c)
		{
			case ' ':
			case '\t':
			case '\n':
			case '\r':
				offset++;
				break;
			default:
				goto OUTOFLOOP0;
				break;
		}
	}
OUTOFLOOP0:
	int Len = 0;
	for (int i = offset; i < MaxLen; i++)
	{
		char c = input[i];
		switch (c)
		{
			case ' ':
			case '\t':
			case '\n':
			case '\r':
				goto FINALIZE;
				break;
			default:
				break;
		}
		Len++;
	}
FINALIZE:
	startPtr[0] = input + offset;
	strLen[0] = Len;
	return 1;
}