@echo off

call 20-PackageVanilla.bat %*
call 21-PackageSmxCompat.bat %*
call 22-PackageAlCompat.bat %*
