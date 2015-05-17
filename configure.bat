:: Author Sebastien Maire
:: Contribs

@echo off

set pathDevEnv=
set pathDevEnvFilePath=pathDevEnv.cfg
set workingDir=code_in
set configFile=./code_in/code_in.csproj.user
set CURRENTDIR=%cd%

:setPath
if exist %pathDevEnvFilePath% (
echo Found %pathDevEnvFilePath% that stores devenv.exe path
) else (
echo configure.bat did not found %pathDevEnvFilePath% and cannot configure.
echo Please give the path to the folder that contains devenv.exe  generally "X:/.../Microsoft Visual Studio xx.0/Common7/IDE/"
folderBrowser.exe > %pathDevEnvFilePath%
)

set /p pathDevEnv=< %pathDevEnvFilePath%

if not exist "%pathDevEnv%\devenv.exe" (
echo  "%pathDevEnv%\devenv.exe"
echo Wrong path, you must give a valid path to devenv.exe
del %pathDevEnvFilePath%
goto :setPath
)

echo ^<^?xml version="1.0" encoding="utf-8"?^> > %configFile%
echo ^<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003"^> >> %configFile%
echo   ^<PropertyGroup Condition="'$(Configuration)|$(Platform)' == 'Debug|AnyCPU'"^> >> %configFile%
echo     ^<StartAction^>Program^</StartAction^> >> %configFile%
echo     ^<StartProgram^>%pathDevEnv%\devenv.exe^</StartProgram^> >> %configFile%
echo     ^<StartArguments^>/resetaddin code_in.Connect^</StartArguments^> >> %configFile%
echo     ^<StartWorkingDirectory^>%CURRENTDIR%\%workingDir%^</StartWorkingDirectory^> >> %configFile%
echo   ^</PropertyGroup^> >> %configFile%
echo ^</Project^> >> %configFile%

echo Configuring done

pause