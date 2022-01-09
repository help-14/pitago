#!/bin/bash

dotnet publish .\Pitago.Linux\Pitago.Linux.csproj -c Release --runtime linux-x64 --self-contained true --output .\Pitago.Linux\bin\Release\x64
dotnet publish .\Pitago.Linux\Pitago.Linux.csproj -c Release --runtime linux-x86 --self-contained true --output .\Pitago.Linux\bin\Release\x86
dotnet publish .\Pitago.Linux\Pitago.Linux.csproj -c Release --runtime linux-arm --self-contained true --output .\Pitago.Linux\bin\Release\arm
dotnet publish .\Pitago.Linux\Pitago.Linux.csproj -c Release --runtime linux-arm64 --self-contained true --output .\Pitago.Linux\bin\Release\arm64

#dotnet workload install macos
#dotnet publish .\Pitago.Mac\Pitago.Mac.csproj -c Release --runtime osx-x64 --self-contained true --output .\Pitago.Mac\bin\Release\x64
#dotnet publish .\Pitago.Mac\Pitago.Mac.csproj -c Release --runtime osx.12-arm64 --self-contained true --output .\Pitago.Mac\bin\Release\arm64