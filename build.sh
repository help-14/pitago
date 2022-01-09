#!/bin/bash

sudo apt install zip -y
dotnet publish ./Pitago.Linux/Pitago.Linux.csproj -c Release --runtime linux-x64 --self-contained true -p:PublishTrimmed=true --output ./Pitago.Linux/bin/Release/Pitago-x64
dotnet publish ./Pitago.Linux/Pitago.Linux.csproj -c Release --runtime linux-arm --self-contained true -p:PublishTrimmed=true --output ./Pitago.Linux/bin/Release/Pitago-arm
dotnet publish ./Pitago.Linux/Pitago.Linux.csproj -c Release --runtime linux-arm64 --self-contained true -p:PublishTrimmed=true --output ./Pitago.Linux/bin/Release/Pitago-arm64

cd ./Pitago.Linux/bin/Release/

zip Pitago-linux-x64.zip -r Pitago-x64
zip Pitago-linux-arm.zip -r Pitago-arm
zip Pitago-linux-arm64.zip -r Pitago-arm64

#dotnet workload install macos
#dotnet publish ./Pitago.Mac/Pitago.Mac.csproj -c Release --runtime osx-x64 --self-contained true --output ./Pitago.Mac/bin/Release/x64
#dotnet publish ./Pitago.Mac/Pitago.Mac.csproj -c Release --runtime osx.12-arm64 --self-contained true --output ./Pitago.Mac/bin/Release/arm64
 
