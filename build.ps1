dotnet publish .\Pitago\Pitago.csproj -c Release --runtime win-x64 --self-contained true --output .\Pitago\bin\Release\win-x64
Compress-Archive -Path .\Pitago\bin\Release\win-x64 -DestinationPath .\Pitago\bin\Release\Pitago-win-x64.zip

dotnet publish .\Pitago\Pitago.csproj -c Release --runtime win-x86 --self-contained true --output .\Pitago\bin\Release\win-x86
Compress-Archive -Path .\Pitago\bin\Release\win-x86 -DestinationPath .\Pitago\bin\Release\Pitago-win-x86.zip

dotnet publish .\Pitago\Pitago.csproj -c Release --runtime win-arm64 --self-contained true --output .\Pitago\bin\Release\win-arm64
Compress-Archive -Path .\Pitago\bin\Release\win-arm64 -DestinationPath .\Pitago\bin\Release\Pitago-win-arm64.zip