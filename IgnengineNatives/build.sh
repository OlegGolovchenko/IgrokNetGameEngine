#!/bin/bash
echo "compiling"
g++ -c -Wall -Werror -fpic IgneDisplay.cpp
echo "linking"
g++ -shared -o libIgnEngineUi.so IgneDisplay.o -lX11 -lGL -lGLU