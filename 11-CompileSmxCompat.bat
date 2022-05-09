@echo off

call MC7D2D SmxCompat\InventoryMouseWheelSMX.dll ^
/reference:"%PATH_7D2D_MANAGED%\Assembly-CSharp.dll" ^
/reference:"SmxCompat\Quartz.lib" ^
Harmony\*.cs SmxCompat\*.cs && ^
echo Successfully compiled SmxCompat\InventoryMouseWheelSMX.dll

SET RV=%ERRORLEVEL%
if "%CI%"=="" pause
exit /B %RV%
