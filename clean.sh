#!/bin/bash
echo "cleaning"
cd ./IgnengineNatives
./clean.sh
cd ..
echo "cleaning dotnet"
dotnet clean