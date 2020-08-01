# IgrokNetGameEngine
This repository is for Game Engine created using .net core xlib and opengl

This product is fully compatible and tested to run on following environment:
* Windows 10
  * Debian on WSL2
  * VcXSrv
## Prerequisites
In order to build this repo you'll need
* gcc and g++
* .NET Core 3.1
* linux libraries:
  * Xlib (with dev libraries)
  * Opengl (via mesa package or driver specific package)
  * GLU (with dev dependencies)

## How to build
Run `./build.sh` from root of the repo
Run `./install.sh` from root of the repo to copy IgnengineNatives library into /usr/lib

## How to run
You can use vscode to debug this program or just call `dotnet run --project IgnengineRunner/IgnengineRunner.csproj`.
