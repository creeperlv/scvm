#include "datac.h"
#include "../core/strutil.h"
#include <stdio.h>
char Char2Hex(char c, char *out)
{
	if (c >= '0' && c <= '9')
	{
		out[0] = c - '0';
		return 1;
	}
	else
	{
		if (c >= 'a')
		{
			c -= ('a' - 'A');
		}
		if (c >= 'A' && c <= 'F')
		{
			out[0] = c - 'A' + 0xA;
			return 1;
		}
	}
	return 0;
}
char Str2UInt8(char *str, int Len, uint8_t *out)
{
	uint8_t data = 0;
	char buf = 0;
	if (IsStartWith(str, "0x"))
	{
		char *ptr = str + 2;
		for (size_t i = 0; i < Len - 2; i++)
		{
			char c = ptr[i];
			if (Char2Hex(c, &buf))
			{
				data *= 0x10;
				data += buf;
			}else{
                return 0;
            }
		}
        out[0]=data;
        return 1;
	}
	return 0;
}