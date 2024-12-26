#include "log.h"
#include "stdio.h"

#define __scvm_core_log_green__ "\e[92m"
#define __scvm_core_log_yellow__ "\e[93m"
#define __scvm_core_log_red__ "\e[91m"
#define __scvm_core_log_reset__ "\e[0m"

#define __scvm_log_normal__ __scvm_core_log_green__
#define __scvm_log_warn__ __scvm_core_log_yellow__
#define __scvm_log_error__ __scvm_core_log_red__
#define __scvm_log_panic__ __scvm_core_log_red__

void Log(int LogLevel, char *str)
{
	printf("[");
	switch (LogLevel)
	{
		case LOG_NORMAL:
			printf(__scvm_log_normal__);
			printf("INFO");
			break;
		case LOG_WARN:
			printf(__scvm_log_warn__);
			printf("WARN");
			break;
		case LOG_ERROR:
			printf(__scvm_log_error__);
			printf("ERROR");
			break;
		case LOG_PANIC:
			printf(__scvm_log_panic__);
			printf("PANIC");
			break;
	}
	printf(__scvm_core_log_reset__);
	printf("]");
	printf("%s\n", str);
}
