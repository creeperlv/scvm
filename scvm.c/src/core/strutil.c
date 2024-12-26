#include "strutil.h"
#include <stdio.h>
#include <string.h>
char CStrEqual(char *L, char *R)
{
	size_t len = strlen(L);
	if (len != strlen(R))
	{
		return 0;
	}
	for (size_t i = 0; i < len; i++)
	{
		if (L[i] != R[i])
		{
			return 0;
		}
	}
	return 1;
}

char IsStartWith(char *str, char *tgt)
{
	size_t len = strlen(tgt);
	for (size_t i = 0; i < len; i++)
	{
		if (str[i] != tgt[i])
			return 0;
	}
	return 1;
}