@echo off

call 10-CompileVanilla.bat %*
call 11-CompileSmxCompat.bat %*
call 12-CompileAlCompat.bat %*
