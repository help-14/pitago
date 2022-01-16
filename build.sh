#!/bin/bash

cd ./linux
./publish-appimage -r linux-x64 --skip-yes
./publish-appimage -r linux-arm --skip-yes
./publish-appimage -r linux-arm64 --skip-yes

#sudo apt install zip -y
#dotnet publish ./Pitago/Pitago.csproj -c Release --runtime linux-x64 --self-contained true --output ./Pitago/bin/Release/linux-x64
#dotnet publish ./Pitago/Pitago.csproj -c Release --runtime linux-arm --self-contained true --output ./Pitago/bin/Release/linux-arm
#dotnet publish ./Pitago/Pitago.csproj -c Release --runtime linux-arm64 --self-contained true --output ./Pitago/bin/Release/linux-arm64

#cd ./Pitago/bin/Release/

#zip Pitago-linux-x64.zip -r linux-x64
#zip Pitago-linux-arm.zip -r linux-arm
#zip Pitago-linux-arm64.zip -r linux-arm64

#dotnet workload install macos
#dotnet publish ./Pitago.Mac/Pitago.Mac.csproj -c Release --runtime osx-x64 --self-contained true --output ./Pitago.Mac/bin/Release/x64
#dotnet publish ./Pitago.Mac/Pitago.Mac.csproj -c Release --runtime osx.12-arm64 --self-contained true --output ./Pitago.Mac/bin/Release/arm64
 
