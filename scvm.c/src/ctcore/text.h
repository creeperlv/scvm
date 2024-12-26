#ifndef __SCVM_CTCORE_TEXT__
#define __SCVM_CTCORE_TEXT__
/**
 * Return:
 * - 2: ReachEnd
 * - 1: Hit
 * - 0: NotHit
 */
char NextSegment(char* input, int startIndex,int MaxLen, char** startPtr,int* strLen);

#endif