#!/bin/sh
if [ -z "$CC" ];
then
    CC="cc"
fi

if [ -z "$BINDIR" ];
then
    BINDIR="./bin"
fi

if [ -z "$CORESRC" ];
then
    CORESRC=./src/core/*.c
fi


if [ -z "$SKIP_SIMPLEC" ];
then
    if [ -z "$EXENAME" ];
    then
        EXENAME="scvmsc"
    fi

    if [ -z "$SIMPLECSRC" ];
    then
        SIMPLECSRC=./src/sc/*.c
    fi

    OUTPUT="$BINDIR/$EXENAME"
    COMPILE="$CC $CORESRC $SIMPLECSRC -o $OUTPUT"
    echo "$COMPILE"
    $COMPILE
fi


if [ -z "$SKIP_SIMPLEOFL" ];
then
    if [ -z "$SOFL_EXENAME" ];
    then
        SOFL_EXENAME="scvmsofl"
    fi

    if [ -z "$SIMPLEOFLSRC" ];
    then
        SIMPLEOFLSRC=./src/sofl/*.c
    fi

    OUTPUT="$BINDIR/$SOFL_EXENAME"
    COMPILE="$CC $CORESRC $SIMPLEOFLSRC -o $OUTPUT"
    echo "$COMPILE"
    $COMPILE
fi
