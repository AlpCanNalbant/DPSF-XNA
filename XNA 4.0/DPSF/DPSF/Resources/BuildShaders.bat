@echo off
setlocal

cd %~dp0

@echo Building for the DirectX 11 ...
@for /f %%f IN ('dir /b *.fx') do (
    call mgfxc %%~nf.fx %%~nf.dx11.mgfxo /Profile:DirectX_11
)
@REM @echo Building for the OpenGL ...
@REM @for /f %%f IN ('dir /b *.fx') do (
@REM     call mgfxc %%~nf.fx ..\Resources\%%~nf.ogl.mgfxo
@REM )

endlocal
@pause
