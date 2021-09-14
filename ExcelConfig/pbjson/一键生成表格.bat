cd excel

setlocal enabledelayedexpansion
for %%i in (..\proto\*.proto) do (
    echo %%~nxi
    java -Dfile.encoding=utf-8 -jar ..\bin\pbjson.jar update -I "..\proto" -p %%~nxi --init_if_not_existed
    if !ERRORLEVEL! neq 0 goto ERROREXIT
)

:ERROREXIT
pause