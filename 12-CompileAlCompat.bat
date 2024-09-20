@echo off

call MC7D2D AlCompat\InventoryMouseWheel.dll ^
/reference:"%PATH_7D2D_MANAGED%\NGUI.dll" ^
/reference:"%PATH_7D2D_MANAGED%\Assembly-CSharp.dll" ^
/reference:"%PATH_7D2D_MANAGED%\Afterlife.dll" ^
/reference:"%PATH_7D2D_MANAGED%\Quartz.dll" ^
Harmony\*.cs AlCompat\*.cs && ^
echo Successfully compiled AlCompat\InventoryMouseWheel.dll

SET RV=%ERRORLEVEL%
if "%CI%"=="" pause
exit /B %RV%
