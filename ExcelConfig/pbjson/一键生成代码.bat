set "outputDir=..\..\Assets\Scripts\Config"
set "protoDir=proto"

setlocal enabledelayedexpansion
for %%i in (%protoDir%\*.proto) do (
    java -jar bin\pbjson.jar codegen -I %protoDir% -p %%~nxi -i bin --templates template_client.pj.cs --language cs -o %outputDir%
    start bin\protoc-3.11.2-win64\bin\protoc.exe --csharp_out=%outputdir% %%i
    if !ERRORLEVEL! neq 0 goto ERROREXIT
)

:ERROREXIT
pause