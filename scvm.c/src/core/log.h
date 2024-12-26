#ifndef __SCVM_CORE_LOG__
#define __SCVM_CORE_LOG__

#define LOG_NORMAL 0x00
#define LOG_WARN 0x01
#define LOG_ERROR 0x02
#define LOG_PANIC 0x03

void Log(int LogLevel, char *str);

#endif
