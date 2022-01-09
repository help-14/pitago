dotnet publish .\Pitago.Win\Pitago.Win.csproj -c Release --runtime win-x64 --self-contained true -p:PublishTrimmed=true --output .\Pitago.Win\bin\Release\Pitago-x64
Compress-Archive -Path .\Pitago.Win\bin\Release\Pitago-x64 -DestinationPath .\Pitago.Win\bin\Release\Pitago-win-x64.zip

dotnet publish .\Pitago.Win\Pitago.Win.csproj -c Release --runtime win-x86 --self-contained true -p:PublishTrimmed=true --output .\Pitago.Win\bin\Release\Pitago-x86
Compress-Archive -Path .\Pitago.Win\bin\Release\Pitago-x86 -DestinationPath .\Pitago.Win\bin\Release\Pitago-win-x86.zip

dotnet publish .\Pitago.Win\Pitago.Win.csproj -c Release --runtime win-arm64 --self-contained true -p:PublishTrimmed=true --output .\Pitago.Win\bin\Release\Pitago-arm64
Compress-Archive -Path .\Pitago.Win\bin\Release\Pitago-arm64 -DestinationPath .\Pitago.Win\bin\Release\Pitago-win-arm64.zip