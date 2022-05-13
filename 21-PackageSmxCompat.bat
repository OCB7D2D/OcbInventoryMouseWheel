@echo off

SET NAME=ZMXuiCPOCBIMW

if not exist build\ (
  mkdir build
)

if exist build\%NAME%\ (
  echo remove existing directory
  rmdir build\%NAME% /S /Q
)

mkdir build\%NAME%

SET VERSION=snapshot

if not "%1"=="" (
  SET VERSION=%1
)

echo create %VERSION%

xcopy *.xml build\%NAME%\
xcopy *.md build\%NAME%\
xcopy SmxCompat\*.dll build\%NAME%\
xcopy SmxCompat\Config build\%NAME%\Config\ /S
xcopy Resources build\%NAME%\Resources\ /S
xcopy UIAtlases build\%NAME%\UIAtlases\ /S

powershell -Command "(gc -encoding UTF8 build\%NAME%\ModInfo.xml) -replace '\"OcbInventoryMouseWheel\"', '\"OcbInventoryMouseWheelSMX\"' | Out-File -encoding UTF8 build\%NAME%\ModInfo.xml"

cd build
echo Packaging %NAME%-%VERSION%.zip
powershell Compress-Archive %NAME% %NAME%-%VERSION%.zip -Force
cd ..

SET RV=%ERRORLEVEL%
if "%CI%"=="" pause
exit /B %RV%
