set pathDevEnv=C:\Program Files %%28x86%%29\Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe
set workingDir=code_in_test
set configFile=./code_in_test/code_in_test.csproj.user
SET CURRENTDIR=%cd%

@echo off

echo ^<^?xml version="1.0" encoding="utf-8"?^> > %configFile%
echo ^<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003"^> >> %configFile%
echo   ^<PropertyGroup Condition="'$(Configuration)|$(Platform)' == 'Debug|AnyCPU'"^> >> %configFile%
echo     ^<StartAction^>Program^</StartAction^> >> %configFile%
echo     ^<StartProgram^>%pathDevEnv%^</StartProgram^> >> %configFile%
echo     ^<StartArguments^>/resetaddin code_in_test.Connect^</StartArguments^> >> %configFile%
echo     ^<StartWorkingDirectory^>%CURRENTDIR%\%workingDir%^</StartWorkingDirectory^> >> %configFile%
echo   ^</PropertyGroup^> >> %configFile%
echo ^</Project^> >> %configFile%

echo Configuring done

pause