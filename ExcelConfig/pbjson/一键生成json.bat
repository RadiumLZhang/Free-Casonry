set "outputDir=..\..\Assets\Resources\ConfigAssets\PbJson"
set "protoDir=proto"
set "excelDir=excel"

for %%i in (proto\*.proto) do (
    java -Dfile.encoding=utf-8 -jar bin\pbjson.jar convert -I %protoDir% -i %excelDir% --enum_style number -o %outputDir% -p %%~nxi
)

pause