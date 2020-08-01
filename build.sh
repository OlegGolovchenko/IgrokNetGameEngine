#!/bin/bash
echo "compiling shared library"
cd ./IgnengineNatives
./build.sh
cd ..
echo "compiling dotnet"
dotnet build