#include "../core/log.h"
#include "../core/strutil.h"
#include "../ctcore/cobj.h"
#include <stdio.h>
/* only output object file */
int main(int ac, char **av)
{
	char *output = "a.o";
	char *input = NULL;
	for (int i = 1; i < ac; i++)
	{
		char *item = av[i];
		if (CStrEqual(item, "-o"))
		{
			i++;
			output = av[i];
		}
		else
		{
			input = item;
		}
	}
	printf("input:%s\n", input);
	printf("output:%s\n", output);
	FILE *f = fopen(input, "rb");
	if (f == NULL)
	{
		Log(LOG_ERROR, "Unable to open input file!\n");
	}
	return 0;
}
