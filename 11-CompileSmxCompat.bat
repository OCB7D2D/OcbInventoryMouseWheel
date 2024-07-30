@echo off

call MC7D2D SmxCompat\InventoryMouseWheel.dll ^
/reference:"%PATH_7D2D_MANAGED%\NGUI.dll" ^
/reference:"%PATH_7D2D_MANAGED%\Assembly-CSharp.dll" ^
/reference:"SmxCompat\Quartz.lib" ^
Harmony\*.cs SmxCompat\*.cs && ^
echo Successfully compiled SmxCompat\InventoryMouseWheel.dll

SET RV=%ERRORLEVEL%
if "%CI%"=="" pause
exit /B %RV%
